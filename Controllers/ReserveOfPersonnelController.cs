using Microsoft.AspNetCore.Mvc;
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
    public class ReserveOfPersonnelController : Controller
    {
        public readonly ApplicationContext _context;

        public ReserveOfPersonnelController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
              (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var reverveofpersonnel = _context.reserveOfPersonnels
                .Include(p => p.employeeRegistrationLog)
                .Include(p => p.employeeRegistrationLog.Worker)
                .Include(p => p.employeeRegistrationLog.Worker.positon)
                .Include(p => p.tablePosition.Position)
                .Where(p => p.tablePosition.TableOrganizationsId == TableOrganizations)
                ;
            return View(await reverveofpersonnel.ToListAsync());
        }
 
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ReserveOfPersonnelViewModel reserveOfPersonnelViewModel = new ReserveOfPersonnelViewModel();
    
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
        (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var employees = await _context.employeeRegistrationLogs
                 .Include(i => i.Worker)
                 .Include(i => i.Organizations)
                 .Include(i => i.Worker.positon)
                 .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var TablePositions = await _context.TablePosition
                .Include(i => i.Position)
                .Include(i => i.Organizations)
                .ThenInclude(i => i.users)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
              
            ReserveOfPersonnelViewModel model = new ReserveOfPersonnelViewModel 
            { employeeRegistrationLog = employees, positions = TablePositions};

            return View(model);
        }
 
        public async Task<IActionResult> Create(ReserveOfPersonnelViewModel model)
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            if (ModelState.IsValid)
            {
                ReserveOfPersonnel reserveOfPersonnel = new ReserveOfPersonnel
                {

                    StatusReserve = "В резерве",
                    TablePositionId = model.TablePositionId,
                    StartDateReserve = DateTime.Now,
                    EmployeeRegistrationLogId = model.EmployeeRegistrationLogId
                };

                await _context.reserveOfPersonnels.AddAsync(reserveOfPersonnel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
   (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            VerificationOfEducationViewModel verificationOfEducationViewModel = new VerificationOfEducationViewModel();

            if (id != null)
            {
                ReserveOfPersonnel reserve = await _context.reserveOfPersonnels
                    .Include(p => p.employeeRegistrationLog)
                    .Include(p => p.tablePosition)
                    .Include(p => p.employeeRegistrationLog.Worker)
                    .FirstOrDefaultAsync(p => p.ReserveId == id);
                ReserveOfPersonnelViewModel reserveOfPersonnelViewModel = new ReserveOfPersonnelViewModel
                {
                    ReserveId = reserve.ReserveId,
                    TablePositionId = reserve.TablePositionId,
                    EmployeeRegistrationLogId = reserve.EmployeeRegistrationLogId
                };
        
                if (reserve != null)
                {

                    return View(reserveOfPersonnelViewModel);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ReserveOfPersonnelViewModel model)
        {
            ReserveOfPersonnel reserve = _context.reserveOfPersonnels.Find(model.ReserveId);
            reserve.StatusReserve = "Выведен из резерва";
            reserve.EndDateReserve = DateTime.Now;

                TableHistoryOfAppointments historyofappointments = new TableHistoryOfAppointments
                {
                    DateOfAppointment = DateTime.Now,
                    TablePositionId = reserve.TablePositionId,
                    EmployeeRegistrationLogId = reserve.EmployeeRegistrationLogId,
                };
                _context.TableHistoryOfAppointments.Add(historyofappointments);
              
            
    
            _context.reserveOfPersonnels.Update(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
