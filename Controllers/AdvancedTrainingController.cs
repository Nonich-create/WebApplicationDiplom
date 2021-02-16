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
    public class AdvancedTrainingController : Controller
    {
        public readonly ApplicationContext _context;

        public AdvancedTrainingController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
             (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;



            var advancedTraining =  _context.advancedTrainings
                .Include(p => p.EmployeeRegistrationLog)
                .Include(p => p.EducationalInstitutions)
                .Include(p =>p.EmployeeRegistrationLog.Worker)
                .Where(p => p.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations)
                ;

            return View(await advancedTraining.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var employees = await _context.employeeRegistrationLogs
                .Include(i => i.Worker)
                .Include(i => i.Organizations)
                .ThenInclude(i => i.users)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var educationals = await _context.EducationalInstitutions.ToListAsync();

            AdvancedTrainingViewModel model = new AdvancedTrainingViewModel
            {
              employees = employees,
              educationals = educationals
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvancedTrainingViewModel model)
        {
            if (ModelState.IsValid)
            {
                AdvancedTraining advanced = new AdvancedTraining
                {
                 EducationalInstitutionsId = model.educationalsId,
                 EmployeeRegistrationLogId = model.employeesId,
                 Start = model.Start,
                 End = model.End,
                };

                _context.advancedTrainings.Add(advanced);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
