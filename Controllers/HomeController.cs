using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        List<TableArea> area = new List<TableArea>();
        List<TableDistrict> districts = new List<TableDistrict>();
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
            area = _context.TableArea.ToList();
            districts = _context.TableDistrict.ToList();
            locations = _context.Tablelocality.ToList();
            addresses = _context.PersTableAddresson.ToList();
            AddressViewModel avm = new AddressViewModel { areas = area, districts = districts, localities = locations, addresses = addresses };
            return View(avm);  
         }

    
        //  [HttpPost]
        //  public async Task<IActionResult> Index(Person[] people)
        //  {
        //       return View(await _context.Person.ToListAsync());
        //  }
        //
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
