﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;

namespace WebApplicationDiplom.Controllers
{
    public class PositionController : Controller
    {
        public readonly ApplicationContext _context;

        public PositionController(ApplicationContext context)
        {
            _context = context;
        }

 
        [HttpGet]
        public async Task<IActionResult> Index()
        {
        
            var positiones = _context.TablePosition.Include(p => p.Position);
            return View(await positiones.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel model)
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            if (ModelState.IsValid)
            {
                TablePosition position = new TablePosition
                {
                    DateOfJobRegistration = DateTime.Now,
                    CountPosition = model.CountPosition,
                    JobResponsibilities = model.JobResponsibilities,
                    PositionId = model.PositionId,
                    TableOrganizationsId = TableOrganizations,
                };
                    
                _context.TablePosition.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
 

            return View();
            }
}
}
    