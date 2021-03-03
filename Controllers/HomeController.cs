using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
        
            return View( );
        }

        public ActionResult GetItems(int id)
         {
            return PartialView(_context.TableDistrict.Where(c => c.AreaId == id).ToList());
         }
 
    //     [HttpPost]
    //     public async Task<IActionResult> Organizations(TableOrganizations model)
    //     {
     
    //          var user = _context.Users.Include(i => i.tableOrganizations).FirstOrDefault(i => i.UserName == User.Identity.Name);
    //        if (user.tableOrganizations != null)
    //        {
    //            return RedirectToAction("Index", "Home");
    //        }
    //        _context.TableOrganizations.Add(model);
    //        await _context.SaveChangesAsync();

    //        model.Users = model.;

    //        _context.Update(user);

    //        await _context.SaveChangesAsync();

    //        return RedirectToAction("Index", "Home");
    //    }
    //        return View(model);

    //}
       
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
