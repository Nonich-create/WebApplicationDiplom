using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;

namespace WebApplicationDiplom.Controllers
{
    public class ReportsController : Controller
    {

      
        public readonly ApplicationContext _context;

        public ReportsController(ApplicationContext context)
        {
            
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
         

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> TheListOfPersonnelReserve()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
           (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;


            var position = await _context.TablePosition.Include(i => i.Position)
              .Where(i => i.TableOrganizationsId == TableOrganizations)
              .Select(i => new { TablePositionId = i.TablePositionId, JobTitle = i.Position.JobTitle }).ToListAsync();
            ViewBag.TablePositionId = new SelectList(position, "TablePositionId", "JobTitle");


            var historyOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.Position.TableOrganizationsId == TableOrganizations).ToListAsync();

            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.position)
                .Include(i =>i.Worker)
                .Where(i =>i.Worker.TableOrganizationsId == TableOrganizations).ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Where(i => i.employeeRegistrationLog.TableOrganizationsId == TableOrganizations).ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .Where(i => i.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations).ToListAsync();


            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            { 
              tableHistoryOfAppointments = historyOfAppointments,
              tableEducationals = educationals,
              reserveOfPersonnels = reserveOfPersonnels,
              advancedTrainingViewModels = advancedTrainings
            };

            return View(model);
        }

 


    }
}
