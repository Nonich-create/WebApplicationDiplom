﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            //return View(await _context.Worker.ToListAsync());
            var employee = _context.employeeRegistrationLogs 
           .Include(p => p.Worker).Include(p =>p.Organizations).Where(p => p.TableOrganizationsId == TableOrganizations)
           .Include(p => p.Worker.positon)

           ;
            return View(await employee.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.TablePositionId = new SelectList(_context.Position, "PositionId", "JobTitle");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {

                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
               (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
                Worker worker = new Worker
                {
                    Surname = model.Surname,
                    Name = model.Name,
                    DoubleName = model.DoubleName,
                    DateOfBirth = model.DateOfBirth,
                    PositionId = model.PositionId
                };
                 _context.Worker.Add(worker);
                await _context.SaveChangesAsync();
                EmployeeRegistrationLog employee = new EmployeeRegistrationLog
                {
                    EmployeeRegistrationDate = DateTime.Now,
                    WorkerId = worker.WorkerId,
                    TableOrganizationsId = TableOrganizations,
                };
                _context.employeeRegistrationLogs.Add(employee);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
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