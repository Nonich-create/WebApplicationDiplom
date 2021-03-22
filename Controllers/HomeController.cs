using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;
namespace WebApplicationDiplom.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context, UserManager<User> userManager)
        {
 
             _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<TableHistoryOfAppointments> historyofappointments = new List<TableHistoryOfAppointments>();
            List<ReserveOfPersonnel> reserve = new List<ReserveOfPersonnel>();
            var organizations = await _context.TableOrganizations
                .Include(i => i.users)
                .ToListAsync();
            if(User.IsInRole("user"))
            {
                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
         (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

                    historyofappointments = await _context.TableHistoryOfAppointments
                    .Include(p => p.Position)
                    .Include(p => p.EmployeeRegistrationLog)
                    .Include(p => p.EmployeeRegistrationLog.Worker)
                    .Include(p => p.Position.Position)
                    .Where(p => p.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations && p.DateOfDismissal == null).ToListAsync();
                     reserve = await _context.reserveOfPersonnels
                    .Include(i => i.employeeRegistrationLog)
                    .Where(i => i.employeeRegistrationLog.TableOrganizationsId == TableOrganizations).ToListAsync();
            }
         
            AdminWindowsViewModel model = new AdminWindowsViewModel
            {
                tableOrganizations = organizations,
                historyOfAppointments = historyofappointments,  
                reserveOfPersonnels = reserve,
            };
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region Отображения смены пароля 
        [HttpGet]
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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                AdminWindowsViewModel model = new AdminWindowsViewModel
                {
                    OrganizationId = id,
                };
                if (model != null)
                {

                    return View(model);
                }
            }
            return NotFound();
        }
        #region удаления записи о организации
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                TableOrganizations organizations = await _context.TableOrganizations.FirstOrDefaultAsync(p => p.TableOrganizationsId == id);
                User user = await _context.users.FirstOrDefaultAsync(p => p.Id == organizations.UserId);
                if (organizations != null && user != null)
                {

                    _context.TableOrganizations.Remove(organizations);
                    _context.users.Remove(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
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
            return RedirectToAction("Index");
        }
        #endregion
    }
}
