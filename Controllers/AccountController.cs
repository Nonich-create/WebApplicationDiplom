using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;

namespace WebApplicationDiplom.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationContext _context;
     
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
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
                        
                 //   }
                    await _signInManager.SignInAsync(user, false);
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
    }
}
