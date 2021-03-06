using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        #region Отображения образований работников
        [HttpGet]
        public async Task<IActionResult> Index()
        {
                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

                 var educationales = _context.TableEducational
                .Include(p => p.Worker)
                .Include(p => p.tableSpecialty)
                .Include(p => p.Qualification)
                .Include(p => p.EducationalInstitutions)
                .Include(p => p.Worker.Worker)
                .Where(p => p.Worker.TableOrganizationsId == TableOrganizations);
             return View(await educationales.ToListAsync());
        }
        #endregion
        #region Отображения регистрации образования
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var Worker = await _context.employeeRegistrationLogs
            .Include(i => i.Worker)
            .Include(i => i.Organizations)
            .ThenInclude(i => i.users)
            .OrderBy(i => i.Worker.Surname)
            .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var Qualification = await _context.TableQualification
                .OrderBy(i => i.Qualification)
                .ToListAsync();
            var Educational = await _context.EducationalInstitutions
                  .OrderBy(i => i.NameEducationalInstitutions)
                .ToListAsync();
            var Specialties = await _context.tableSpecialties
                    .OrderBy(i => i.Specialty)
                .ToListAsync();


  
            EducationalViewModel model = new EducationalViewModel
            {
                workers = Worker,
                educationals = Educational,
                tableSpecialties = Specialties,
                qualifications = Qualification
            };
            return View(model);
        }
        #endregion
        #region регистрация нового образования работника
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
                    tableSpecialtySpecialtyId = model.tableSpecialtySpecialtyId,
                    QualificationId = model.QualificationId,
                    WorkerEmployeeRegistrationId = model.EmployeeRegistrationLogId
                };
                _context.TableEducational.Add(educational);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
        #region отображения добавления место образоавния в справочник 
        [HttpGet]
        public IActionResult CreateEducationalInstitutions()
        {
            return View();
        }
        #endregion
        #region добавления образования в справочник
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
        #endregion
        #region отображения добавления квалификации 
        [HttpGet]
        public IActionResult CreateQualification()
        {
            return View();
        }
        #endregion
        #region добавления квалификации в справчоник
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
        #endregion
        #region отображения добавления специальности
        [HttpGet]
        public IActionResult CreateSpecialty()
        {
            return View();
        }
        #endregion
        #region добавления специальности в справочник
        [HttpPost]
        public async Task<IActionResult> CreateSpecialty(TableSpecialty model)
        {
            if (ModelState.IsValid)
            {
                TableSpecialty position = await _context.tableSpecialties.FirstOrDefaultAsync
                    (i => i.Specialty == model.Specialty);
                if (position == null)
                {
                    await _context.tableSpecialties.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }
        #endregion
        #region удаления образования работника
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                TableEducational educational = await _context.TableEducational.FirstOrDefaultAsync(p => p.EducationalId == id);
                if (educational != null)
                {
                    _context.TableEducational.Remove(educational);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        #endregion
    }

}
