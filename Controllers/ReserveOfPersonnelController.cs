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
                .Include(p => p.Worker.positon)
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

           
            var Workers = await _context.Worker.ToListAsync();

            var workers = await _context.employeeRegistrationLogs.Include(
            i => i.Worker).Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            workers.ForEach(i => reserveOfPersonnelViewModel.workers.Add(i.Worker));
 
            var TablePositions = await _context.TablePosition
                .Include(i => i.Position)
                .Include(i => i.Organizations)
                .ThenInclude(i => i.users)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
              
            ReserveOfPersonnelViewModel model = new ReserveOfPersonnelViewModel { workers = reserveOfPersonnelViewModel.workers, positions = TablePositions };

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
                    WorkerId = model.WorkerId,
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
                    .Include(p => p.Worker)
                    .Include(p => p.tablePosition)
                    .FirstOrDefaultAsync(p => p.ReserveId == id);
                ReserveOfPersonnelViewModel reserveOfPersonnelViewModel = new ReserveOfPersonnelViewModel
                {
                    ReserveId = reserve.ReserveId,
                    TablePositionId = reserve.TablePositionId,
                    WorkerId = reserve.WorkerId,
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
                    WorkerId = reserve.WorkerId,
                };
                _context.TableHistoryOfAppointments.Add(historyofappointments);
              
            
    
            _context.reserveOfPersonnels.Update(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
