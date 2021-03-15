using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;
namespace WebApplicationDiplom.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Identifier, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var organizations = await _context.TableOrganizations.ToListAsync();
            RegisterViewModel model = new RegisterViewModel
            {
                organizations = organizations,
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Identifier, Identifier = model.Identifier, };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TableOrganizations organizations = new TableOrganizations
                    {
                        NameOfOrganization = model.OrganizationName,
                        UserId = user.Id,
                        TypeOrganization = model.Businesses.ToString(),  
                        Email = model.Email,
                        SubordinationId = model.TableOrganizationsId, 
                    };
                        _context.TableOrganizations.Add(organizations);
                        await _context.SaveChangesAsync();
                         await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return RedirectToAction("Register", "Account");
                    }
                }
            }
            return View(model);
        }
        #region отображения информации о организации
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> OrganizationInformation()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var organizations = await _context.TableOrganizations
                .Include(i => i.users)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
            var adreess = await _context.PersTableAddresson
                .Include(i => i.locality)
                .Include(i => i.locality.District)
                .Include(i => i.locality.District.Area)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
            TableAddress tableAddress;
            if (adreess.Count == 0)
            {
                 tableAddress = null;
            }
            else
            {
                 tableAddress = adreess[0];
            }
            InformationAboutTheAccountViewModel model = new InformationAboutTheAccountViewModel
            {
                TableOrganizations = organizations,
                addresses = adreess,
                address = tableAddress
            };  
            return View(model);
        }
        #endregion
        #region отображения редактирования информации о организации
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                TableOrganizations organizations = await _context.TableOrganizations.FirstOrDefaultAsync(p => p.TableOrganizationsId == id);
                if (organizations != null)
                {
                    return View(organizations);
                }
            }
            return NotFound();
        }
        #endregion
        #region редактирования организации
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(TableOrganizations organizations)
        {
            _context.TableOrganizations.Update(organizations);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrganizationInformation");
        }
        #endregion
        #region Отображения смены пароля 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Identifier = user.Identifier };
            return View(model);
        }
        #endregion
        #region смена пароля
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("OrganizationInformation");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        #endregion
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddAdreess(int? id)
        {
            var area = await _context.TableArea.ToListAsync();

            InformationAboutTheAccountViewModel model = new InformationAboutTheAccountViewModel
            {
                TableOrganizationsId = id,
                areas = area,
            };
            return View(model); ;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAdreess(InformationAboutTheAccountViewModel model)
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
              (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

                TableDistrict district = new TableDistrict
                {
                    NameDistrict = model.District,
                    AreaId = model.areaId
                };
                _context.TableDistrict.Add(district);
                await _context.SaveChangesAsync();
                Tablelocality tablelocality = new Tablelocality
                {
                    Typelocality = model.TypesOfLocalities.ToString(),
                    DistrictId = district.DistrictId,
                    Namelocality = model.Namelocality
                };
                _context.Tablelocality.Add(tablelocality);
                await _context.SaveChangesAsync();
                TableAddress address = new TableAddress
                {
                    Adress = model.Adress,
                    PostalCode = model.PostalCode,
                    TableOrganizationsId = TableOrganizations,
                                     localityId = tablelocality.localityId,
                };
                _context.PersTableAddresson.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction("OrganizationInformation");
    
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                TableAddress address = await _context.PersTableAddresson.FirstOrDefaultAsync(p => p.AddressId == id);
                Tablelocality tablelocality = await _context.Tablelocality.FirstOrDefaultAsync(p => p.localityId == address.localityId);
                TableDistrict tableDistrict = await _context.TableDistrict.FirstOrDefaultAsync(p => p.DistrictId == tablelocality.DistrictId);
                if (address != null)
                {
                    _context.PersTableAddresson.Remove(address);
                    await _context.SaveChangesAsync();
                    _context.Tablelocality.Remove(tablelocality);
                    await _context.SaveChangesAsync();
                    _context.TableDistrict.Remove(tableDistrict);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("OrganizationInformation");
                }
            }
            return NotFound();
        }
 
    }
}
