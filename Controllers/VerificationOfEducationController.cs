using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        #region отображения на странице
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
     (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;


            var verificationofeducation = _context.TableVerificationOfEducation
                .Include(p => p.Position)
                .Include(p => p.employeeRegistrationLog)
                .Include(p => p.employeeRegistrationLog.Organizations)
                .Include(p => p.employeeRegistrationLog.Worker)
                .Where(p => p.employeeRegistrationLog.TableOrganizationsId == TableOrganizations);
            
            return View(await verificationofeducation.ToListAsync());
        }
        #endregion
        #region  отображения регистрации аттестации
        [HttpGet]
       public async Task<IActionResult> Create()
       {
           int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var employees = await _context.employeeRegistrationLogs
                .Include(i => i.Worker)
                .Include(i => i.Organizations)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var position = await _context.Position.ToListAsync();


            VerificationOfEducationViewModel model = new VerificationOfEducationViewModel
            {
                positions = position,
                employeeRegistrationLogs = employees,
            };
            return View(model);
       }
        #endregion
        #region регистрация аттестации
        [HttpPost]
        public async Task<IActionResult> Create(VerificationOfEducationViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                TableVerificationOfEducation verificationOfEducation = new TableVerificationOfEducation
                {

                    Recommendations = "",
                    VerificationStatus = "Не аттестован",
                    DateOfVerification = DateTime.Now,
                    DateOfCertificationCompletion = model.DateOfCertificationCompletion,
                    EmployeeRegistrationLogId = model.EmployeeRegistrationLogId,
                    PositionId = model.PositionId,
                };

                _context.TableVerificationOfEducation.Add(verificationOfEducation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
        #region отображения утверждения аттестации
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            VerificationOfEducationViewModel verificationOfEducationViewModel = new VerificationOfEducationViewModel();

            if (id != null)
            {
                TableVerificationOfEducation verificationOfEducation = await _context.TableVerificationOfEducation 
                    .FirstOrDefaultAsync(p => p.VerificationOfEducationId == id);

                VerificationOfEducationViewModel verificationOfEducationView = new VerificationOfEducationViewModel
                {
                    Id = verificationOfEducation.VerificationOfEducationId,
                    VerificationStatus = verificationOfEducation.VerificationStatus,
                    DateOfVerification = verificationOfEducation.DateOfVerification,
                    EmployeeRegistrationLogId = verificationOfEducation.EmployeeRegistrationLogId,
                    PositionId  = verificationOfEducation.PositionId,
                    Recommendations = verificationOfEducation.Recommendations
                };
                if (verificationOfEducation != null)
                {

                    return View(verificationOfEducationView);
                }
            }
            return NotFound();
        }
        #endregion
        #region утверждения аттестации
        [HttpPost]
        public async Task<IActionResult> Edit(VerificationOfEducationViewModel model )
        {
        TableVerificationOfEducation tableVerificationOfEducation = _context.TableVerificationOfEducation.Find(model.Id);
         tableVerificationOfEducation.Recommendations = model.EnumerationRecommendations.ToString();
         tableVerificationOfEducation.VerificationStatus = model.EnumerationStatus.ToString();

            _context.TableVerificationOfEducation.Update(tableVerificationOfEducation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
