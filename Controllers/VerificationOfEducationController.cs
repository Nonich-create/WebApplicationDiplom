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
    public class VerificationOfEducationController : Controller
    {
        public readonly ApplicationContext _context;

        public VerificationOfEducationController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
     (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
          
            
            var verificationofeducation = _context.TableVerificationOfEducation
                .Include(p => p.Position)
                .Include(p => p.Worker)
                .Include(p => p.Organizations)
                .Include(p => p.VerificationOfType).Where(p => p.Organizations.TableOrganizationsId == TableOrganizations);
            return View(await verificationofeducation.ToListAsync());
        }
       [HttpGet]
       public IActionResult Create()
       {
            PositionViewModel tablePositionViewModel = new PositionViewModel();
            VerificationOfEducationViewModel verificationOfEducationViewModel = new VerificationOfEducationViewModel();
           int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
     
           var workers = _context.employeeRegistrationLogs.Include(
            i => i.Worker).Where(i => i.TableOrganizationsId == TableOrganizations).ToList();



            var position = _context.TablePosition.Include(
                i => i.Position).Where(i => i.TableOrganizationsId == TableOrganizations).ToList();
              //.Select(i => new { TablePositionId = i.TablePositionId, JobTitle = i.Position.JobTitle }).ToList();
           
           workers.ForEach(i => verificationOfEducationViewModel.workers.Add(i.Worker));
            position.ForEach(i => tablePositionViewModel.positions.Add(i.Position));


            ViewBag.verificationOfTypesId = new SelectList(_context.verificationOfTypes, "VerificationOfTypeId", "VerificationOfTypeName");


           ViewBag.WorkerId = new SelectList(verificationOfEducationViewModel.workers, "WorkerId", "Surname");
           ViewBag.TablePositionId = new SelectList(tablePositionViewModel.positions, "PositionId", "JobTitle");
           ViewBag.TableOrganizationsId = new SelectList(_context.TableOrganizations.Where(p => 
           p.TableOrganizationsId == TableOrganizations), "TableOrganizationsId", "NameOfOrganization");
            return View(verificationOfEducationViewModel);
       }
       [HttpPost]
       public async Task<IActionResult> Create(VerificationOfEducationViewModel model)
       {
           int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
           if (ModelState.IsValid)
           {
                TableVerificationOfEducation verificationOfEducation = new TableVerificationOfEducation
                {
                   
                   Recommendations = "",
                   VerificationStatus = "Не прошёл",
                   DateOfVerification = DateTime.Now,
                   WorkerId = model.WorkerId,
                   PositionId = model.PositionId,
                   TableOrganizationsId = model.TableOrganizationsId,
 
                   VerificationOfTypeId = model.verificationOfTypesId,
                };
       
               _context.TableVerificationOfEducation.Add(verificationOfEducation);
               await _context.SaveChangesAsync();
               return RedirectToAction("Index");
           }
       
       
           return View();
       }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                TableVerificationOfEducation verificationOfEducation = await _context.TableVerificationOfEducation.FirstOrDefaultAsync(p => p.VerificationOfEducationId == id);
                if (verificationOfEducation != null)
                    return View(verificationOfEducation);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TableVerificationOfEducation verificationOfEducation)
        {
            _context.TableVerificationOfEducation.Update(verificationOfEducation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
