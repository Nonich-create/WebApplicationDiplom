using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        #region отображения повышения квалификации работника
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
        #endregion
        #region отображения регистрации повышения квалификации работника
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var employees = await _context.employeeRegistrationLogs
                .Include(i => i.Worker)
                .Include(i => i.Organizations)
                .ThenInclude(i => i.users)
                .OrderBy(i => i.Worker.Surname)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var educationals = await _context.EducationalInstitutions
                .OrderBy(i => i.NameEducationalInstitutions)
                .ToListAsync();

            AdvancedTrainingViewModel model = new AdvancedTrainingViewModel
            {
              employees = employees,
              educationals = educationals
            };
            return View(model);
        }
        #endregion
        #region регистрация квалификации работника
        [HttpPost]
        public async Task<IActionResult> Create(AdvancedTrainingViewModel model)
        {
            try
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
                else
                {
                    int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
               (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

                    var employees = await _context.employeeRegistrationLogs
                        .Include(i => i.Worker)
                        .Include(i => i.Organizations)
                        .ThenInclude(i => i.users)
                        .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

                    var educationals = await _context.EducationalInstitutions.ToListAsync();
                    AdvancedTrainingViewModel modelResult = new AdvancedTrainingViewModel
                    {
                        employees = employees,
                        educationals = educationals
                    };
                    return View(modelResult);
                }
            }
            catch {
                return RedirectToAction("Create");
            }
        }
        #endregion
    }
}
