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
                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

                 var educationales = _context.TableEducational
                .Include(p => p.Worker)
                .Include(p => p.position)
                .Include(p => p.Qualification)
                .Include(p => p.EducationalInstitutions)
                .Include(p => p.Worker.Worker)
                .Where(p => p.Worker.TableOrganizationsId == TableOrganizations);
             return View(await educationales.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var Worker = await _context.employeeRegistrationLogs
                .Include(i => i.Worker)
                .Include(i => i.Organizations)
                .ThenInclude(i => i.users)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            ViewBag.EducationalInstitutionsId = new SelectList(_context.EducationalInstitutions, "EducationalInstitutionsId", "NameEducationalInstitutions");
            ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle");
            ViewBag.QualificationId = new SelectList(_context.TableQualification, "QualificationId", "Qualification");
            EducationalViewModel model = new EducationalViewModel
            {
                workers = Worker
            };
            return View(model);
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
                    QualificationId = model.QualificationId,
                    WorkerEmployeeRegistrationId = model.EmployeeRegistrationLogId
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
        [HttpGet]
        public IActionResult CreateEducationalInstitutions()
        {
 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEducationalInstitutions(EducationalInstitutions model)
        {
            if (ModelState.IsValid)
            {
                EducationalInstitutions educational = await _context.EducationalInstitutions.FirstOrDefaultAsync
                    (i => i.NameEducationalInstitutions == model.NameEducationalInstitutions);
                if (educational == null)
                {
                    await _context.EducationalInstitutions.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");
                    }
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateQualification()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateQualification(TableQualification model)
        {
            if (ModelState.IsValid)
            {
                TableQualification qualification = await _context.TableQualification.FirstOrDefaultAsync
                    (i => i.Qualification == model.Qualification);
                if (qualification == null)
                {
                    await _context.TableQualification.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
            }
            return View();
        }
    }

}
