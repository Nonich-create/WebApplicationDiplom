using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.Controllers
{
    public class HomeController : Controller
    {
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
         public IActionResult Index()
         {
             return View();
        
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
