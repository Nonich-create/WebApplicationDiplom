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
                    // проверяем, принадлежит ли URL приложению
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
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
 
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
                // добавляем пользователя

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
                    // установка куки
               //     var UserIdentifier = await _context.Users.FirstOrDefaultAsync
                 //   (i => i.Identifier == model.Identifier);
        
                 //   if (UserIdentifier == null)
                 //   {
                        _context.TableOrganizations.Add(organizations);
                        await _context.SaveChangesAsync();
                         await _userManager.AddToRoleAsync(user, "user");
                    //   }
                    //   await _signInManager.SignInAsync(user, false);
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
            var users = _userManager.Users.ToList();

            var organizations = await _context.TableOrganizations
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();


            InformationAboutTheAccountViewModel model = new InformationAboutTheAccountViewModel
            {
                TableOrganizations = organizations,
                users = users,
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
                    return View(organizations);
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
                        return RedirectToAction("Index");
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

    }
}
