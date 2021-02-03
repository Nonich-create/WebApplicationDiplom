using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;

namespace WebApplicationDiplom.Controllers
{
   //  [Authorize] 
    public class HomeController : Controller
    {
        ObservableCollection<TableArea> area = new ObservableCollection<TableArea>();
        ObservableCollection<TableDistrict> districts = new ObservableCollection<TableDistrict>();
        List<Tablelocality> locations = new List<Tablelocality>();
        List<TableAddress> addresses = new List<TableAddress>();
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        // [HttpGet]
        // public async Task<IActionResult> Index()
        // {
        //    // return View(await _context.Person.Include(i => i.Company).ToListAsync());
        //
        // }
        
         [HttpGet]
         public async Task<IActionResult> Index()
         {
            int selectedIndex = 1;
            foreach (var item in _context.TableArea.ToList())
                area.Add(item);
       //     districts = _context.TableDistrict.ToList();
        
            locations = _context.Tablelocality.ToList();
            addresses = _context.PersTableAddresson.ToList();
            //area.CollectionChanged += Users_CollectionChanged;
            SelectList areaes = new SelectList(_context.TableArea, "AreaId", "NameArea", selectedIndex);
            ViewBag.TableArea = areaes;
            SelectList districtes = new SelectList(_context.TableDistrict.Where(c => c.AreaId==selectedIndex), "DistrictId", "NameDistrict");
            ViewBag.TableDistrict = districtes;

            ViewBag.AreaId = new SelectList(_context.TableDistrict, "AreaId", "NameArea");

            ViewBag.DistrictId = new SelectList(_context.TableDistrict, "DistrictId", "NameDistrict");


            AddressViewModel avm = new AddressViewModel { areas = area, districts = districts, localities = locations, addresses = addresses };
            return View(avm);  
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
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
