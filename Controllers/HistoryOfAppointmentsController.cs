using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;

namespace WebApplicationDiplom.Controllers
{
    public class HistoryOfAppointmentsController : Controller
    {
        public readonly ApplicationContext _context;

        public HistoryOfAppointmentsController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
     (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            var historyofappointments = _context.TableHistoryOfAppointments
                .Include(p => p.Position)
                .Include(p => p.Worker)
                .Include(p => p.Position.Position).Where(p => p.Position.TableOrganizationsId == TableOrganizations);
            return View(await historyofappointments.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create( )
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;


            ViewBag.WorkerId = new SelectList(_context.employeeRegistrationLogs.Include(
                i => i.Worker).Where(i => i.TableOrganizationsId == TableOrganizations), "WorkerId", "Surname");
            ViewBag.EducationalInstitutions = new SelectList(_context.employeeRegistrationLogs);
             ViewBag.TablePositionId = new SelectList(_context.TablePosition, "TablePositionId", "PositionId");
            ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HistoryOfAppointmentsViewModel model)
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            if (ModelState.IsValid)
            {
                TableHistoryOfAppointments historyofappointments = new TableHistoryOfAppointments
                {
                    DateOfAppointment = DateTime.Now,
                    TablePositionId = model.TablePositionId,
                    WorkerId = model.WorkerId,
                };

                _context.TableHistoryOfAppointments.Add(historyofappointments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}
