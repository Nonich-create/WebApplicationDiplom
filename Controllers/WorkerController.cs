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
    public class WorkerController : Controller
    {
        public readonly ApplicationContext _context;
       
        public WorkerController(ApplicationContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Worker.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Worker worker = new Worker
                {
                    Surname = model.Surname,
                    Name = model.Name,
                    DoubleName = model.DoubleName,
                    DateOfBirth = model.DateOfBirth,
                };
                 _context.Worker.Add(worker);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Worker worker = await _context.Worker.FirstOrDefaultAsync(p => p.WorkerId == id);
                if (worker != null)
                    return View(worker);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Worker worker)
        {
            _context.Worker.Update(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
