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
    public class EducationalController : Controller
    {
        public readonly ApplicationContext _context;

        public EducationalController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var educationales = _context.TableEducational
                .Include(p => p.Worker)
                .Include(p => p.position)
                .Include(p => p.Qualification)
                .Include(p => p.EducationalInstitutions) 
                ;
            return View( await educationales.ToListAsync());  
        }
        public IActionResult Create()
        {
            ViewBag.WorkerId = new SelectList(_context.Worker, "WorkerId", "Surname");

            ViewBag.EducationalInstitutionsId = new SelectList(_context.EducationalInstitutions, "EducationalInstitutionsId", "NameEducationalInstitutions");

            ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle");

            ViewBag.QualificationId = new SelectList(_context.TableQualification, "QualificationId", "Qualification");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EducationalViewModel model)
        {
            if (ModelState.IsValid)
            {
                TableEducational educational = new TableEducational
                {
                    EducationType = model.EducationType.ToString(),
                    QualificationEducation = model.QualificationEducation.ToString(),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EducationalInstitutionsId = model.EducationalInstitutionsId,
                    PositionId = model.PositionId,
                    WorkerId = model.WorkerId,
                    QualificationId = model.QualificationId
                };
             
                _context.TableEducational.Add(educational);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                }
                //ViewBag.WorkerId = new SelectList(_context.Worker, "WorkerId", "Surname",model.WorkerId);

                //ViewBag.EducationalInstitutionsId = new SelectList(_context.EducationalInstitutions, "EducationalInstitutionsId", "NameEducationalInstitutions",model.EducationalInstitutionsId);

                //ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle",model.PositionId);

                //ViewBag.QualificationId = new SelectList(_context.TableQualification, "QualificationId", "Qualification",model.QualificationId);

                return View();
            }
        }
}
