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
                .Where(p =>p.tablePosition.TableOrganizationsId == TableOrganizations)
                ;
            return View(await reverveofpersonnel.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PositionViewModel tablePositionViewModel = new PositionViewModel();
            ReserveOfPersonnelViewModel reserveOfPersonnelViewModel = new ReserveOfPersonnelViewModel();
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var workers =  await _context.employeeRegistrationLogs.Include(
             i => i.Worker).Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var tablepositionId = _context.TablePosition
               .FirstOrDefault(i => TableOrganizations == i.TableOrganizationsId).TablePositionId;

            var pp = await _context.TablePosition
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
            // var position =  _context.TablePosition
            //   .Include(i => i.Position).Where(i => i.TablePositionId == tableposition).;

            var position = _context.Position
                .Where(i => i.PositionId == tablepositionId);

            pp.ForEach(i => tablePositionViewModel.positions.Add(i.Position));
            workers.ForEach(i => reserveOfPersonnelViewModel.workers.Add(i.Worker));
             
            //position.ForEach(i => tablePositionViewModel.positions.Add(i.Position));
          //  tablePositionViewModel.positions
           
            ViewBag.WorkerId = new SelectList(reserveOfPersonnelViewModel.workers, "WorkerId", "Surname");
            ViewBag.TablePositionId = new SelectList(position, "PositionId", "JobTitle");
   
            return View();
        }
        [HttpPost]
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
    }
}
