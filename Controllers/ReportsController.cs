using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;
namespace WebApplicationDiplom.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationContext _context;
        public ReportsController(ApplicationContext context)
        {
            _context = context;
        }
        #region Отображения вариантов отчёта
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region страница для отображения отчёта
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TheListOfPersonnelReserve()
        {
            int TableOrganizations =  _context.TableOrganizations.Include(i => i.users).FirstOrDefault
           (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            return View(await FillingInTheModel(TableOrganizations));
        }
        #endregion
        #region страница для сохранения отчёта
        [HttpGet]
        public async Task<IActionResult> SavingReport(int id)
        {
            return View(await FillingInTheModel(id));
        }
        #endregion
        #region метод для заполнения модели
        private async Task<TheListOfPersonnelReserveViewModel> FillingInTheModel(int TableOrganizations)

        {
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
            .Where(i => i.DateOfDismissal == null)
            .Where(i => i.Position.TableOrganizationsId == TableOrganizations)
            .ToListAsync();

            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.tableSpecialty)
                .Include(i => i.Worker)
                .Where(i => i.Worker.TableOrganizationsId == TableOrganizations).ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Where(i => i.EndDateReserve == null)
                .Where(i => i.employeeRegistrationLog.TableOrganizationsId == TableOrganizations)
                .ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .Where(i => i.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations).ToListAsync();
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            {
                Id = TableOrganizations,
                tableHistoryOfAppointments = historyOfAppointments,
                tableEducationals = educationals,
                reserveOfPersonnels = reserveOfPersonnels,
                advancedTrainingViewModels = advancedTrainings,

            };
            return model;
        }
        #endregion
        public async Task<IActionResult> SavingChairmenOfTheManagementBoards(int id)
        {
            return View(await TheModelIsFilledWithAllTheData(id));
        }
        public FileResult DownloadReport(int id,string Url,string FileType,string FileName)
        {
            Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/{Url}/{id}");
            using (WebClient client = new WebClient())
            {
                return File(client.DownloadData(location), FileType, FileName);
            }
        }
        [Authorize]
        public async Task<IActionResult> ChairmenOfTheManagementBoards()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
           (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            return View(await TheModelIsFilledWithAllTheData(TableOrganizations));
        }
        #region список резерва
        private async Task<TheListOfPersonnelReserveViewModel> TheModelIsFilledWithAllTheData(int TableOrganizations)
        {
            var historyOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .Where(i => i.Position.Position.JobTitle == "Председатель правления"
             || i.Position.Position.JobTitle == "Председатель ревизионной комиссии"
             || i.Position.Position.JobTitle == "Директор филиала"
             || i.Position.Position.JobTitle == "И.о.председателя правления"
             || i.Position.Position.JobTitle == "И.о.директора филиала"
             || i.Position.Position.JobTitle == "И.о.директора"
             || i.Position.Position.JobTitle == "И.о.председателя ревизионной комиссии"
             || i.Position.Position.JobTitle == "Директор")
            .ToListAsync();

            var tablePositions = await _context.TablePosition
                .Include(i => i.Organizations)
                .Include(i => i.Position)
                 .Where(i => i.Position.JobTitle == "Председатель правления"
             || i.Position.JobTitle == "Председатель ревизионной комиссии"
             || i.Position.JobTitle == "Директор филиала"
             || i.Position.JobTitle == "И.о.председателя правления"
             || i.Position.JobTitle == "И.о.директора филиала"
             || i.Position.JobTitle == "И.о.директора"
             || i.Position.JobTitle == "И.о.председателя ревизионной комиссии"
             || i.Position.JobTitle == "Директор")
            .ToListAsync();

            var AllhistoryOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .ToListAsync();


            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.tableSpecialty)
                .Include(i => i.Worker)
                .Include(i => i.Qualification)
                .ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Include(i => i.tablePosition.Position)
                .Include(i => i.employeeRegistrationLog.Worker)
                .Where(i => i.EndDateReserve == null)
                .ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .ToListAsync();

            var organizations = await _context.TableOrganizations
                .ToListAsync();


            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            {
                Id = TableOrganizations,
                tableHistoryOfAppointments = historyOfAppointments,
                tableEducationals = educationals,
                reserveOfPersonnels = reserveOfPersonnels,
                advancedTrainingViewModels = advancedTrainings,
                Organizations = organizations,
                AllHistoryOfAppointments = AllhistoryOfAppointments,
                tablePositions = tablePositions
                
            };
            return model;
        }
        #endregion
        #region список резерва на должности председателей правлений облпотребсоюзов, облпотребобществ

        private async Task<TheListOfPersonnelReserveViewModel> CreateApplicationReport(int TableOrganizations)
        {
            var historyOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .Where(i => i.Position.Position.JobTitle == "Председатель правления облпотребсоюза"
             || i.Position.Position.JobTitle == "Председатель правления облпотребобщества")
            .ToListAsync();

            var tablePositions = await _context.TablePosition
              .Include(i => i.Organizations)
              .Include(i => i.Position)
   .Where(i => i.Position.JobTitle == "Председатель правления облпотребсоюза"
             || i.Position.JobTitle == "Председатель правления облпотребобщества")
          .ToListAsync();

            var AllhistoryOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .ToListAsync();


            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.tableSpecialty)
                .Include(i => i.Worker)
                .Include(i => i.Qualification)
                .ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Include(i => i.tablePosition.Position)
                .Include(i => i.employeeRegistrationLog.Worker)
                .Where(i => i.EndDateReserve == null)
                .ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .ToListAsync();

            var organizations = await _context.TableOrganizations
                .ToListAsync();
            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            {
                Id = TableOrganizations,
                tableHistoryOfAppointments = historyOfAppointments,
                tableEducationals = educationals,
                reserveOfPersonnels = reserveOfPersonnels,
                advancedTrainingViewModels = advancedTrainings,
                Organizations = organizations,
                AllHistoryOfAppointments = AllhistoryOfAppointments,
                tablePositions = tablePositions
            };
            return model;
        }
        #endregion
        #region Формирования резерва на должности председателей правлений облпотребсоюзов, облпотребобществ
        [Authorize]
        public async Task<IActionResult> ApplicationReport()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
           (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            return View(await CreateApplicationReport(TableOrganizations));
        }
        #endregion
        #region на сохранения формирования резерва на должности председателей правлений облпотребсоюзов, облпотребобществ на сохранения
        public async Task<IActionResult> SavingApplicationReport(int id)
        {
            return View(await CreateApplicationReport(id));
        }
        #endregion 
        #region список лиц, включенных в кадровый резерв на должности руководителей предприятий и учреждений Белкоопсоюза 
        private async Task<TheListOfPersonnelReserveViewModel> Reports4(int TableOrganizations)
        {
            var historyOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .Where(i => i.Position.Position.JobTitle.Contains("Директор"))
            .ToListAsync();

            var tablePositions = await _context.TablePosition
              .Include(i => i.Organizations)
              .Include(i => i.Position)
            .Where(i => i.Position.JobTitle.Contains("Директор"))
          .ToListAsync();

            var AllhistoryOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .ToListAsync();


            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.tableSpecialty)
                .Include(i => i.Worker)
                .Include(i => i.Qualification)
                .ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Include(i => i.tablePosition.Position)
                .Include(i => i.employeeRegistrationLog.Worker)
                .Where(i => i.EndDateReserve == null)
                .ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .ToListAsync();

            var organizations = await _context.TableOrganizations
                .ToListAsync();
            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            {
                Id = TableOrganizations,
                tableHistoryOfAppointments = historyOfAppointments,
                tableEducationals = educationals,
                reserveOfPersonnels = reserveOfPersonnels,
                advancedTrainingViewModels = advancedTrainings,
                Organizations = organizations,
                AllHistoryOfAppointments = AllhistoryOfAppointments,
                tablePositions = tablePositions
            };
            return model;
        }
        #endregion
        #region Формирования резерва на должности председателей правлений облпотребсоюзов, облпотребобществ
        [Authorize]
        public async Task<IActionResult> ApplicationReport4()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            return View(await Reports4(TableOrganizations));
        }
        #endregion
        #region на сохранения формирования резерва на должности председателей правлений облпотребсоюзов, облпотребобществ на сохранения
        public async Task<IActionResult> SavingApplicationReport4(int id)
        {
            return View(await Reports4(id));
        }
        #endregion
        #region список лиц, включенных в кадровый резерв руководящих кадров по аппарату управления Белкоопсоюза
        private async Task<TheListOfPersonnelReserveViewModel> Reports5(int TableOrganizations)
        {
            var historyOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .Where(i => i.Position.Position.JobTitle.Contains("Начальник"))
            .ToListAsync();

            var tablePositions = await _context.TablePosition
              .Include(i => i.Organizations)
              .Include(i => i.Position)
            .Where(i => i.Position.JobTitle.Contains("Начальник"))
          .ToListAsync();

            var AllhistoryOfAppointments = await _context.TableHistoryOfAppointments
            .Include(i => i.Position)
            .Include(i => i.Position.Position)
            .Include(i => i.EmployeeRegistrationLog)
            .Include(i => i.EmployeeRegistrationLog.Worker)
            .Include(i => i.Position.Organizations)
            .Where(i => i.DateOfDismissal == null)
            .ToListAsync();


            var educationals = await _context.TableEducational
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.tableSpecialty)
                .Include(i => i.Worker)
                .Include(i => i.Qualification)
                .ToListAsync();

            var reserveOfPersonnels = await _context.reserveOfPersonnels
                .Include(i => i.tablePosition)
                .Include(i => i.employeeRegistrationLog)
                .Include(i => i.tablePosition.Position)
                .Include(i => i.employeeRegistrationLog.Worker)
                .Where(i => i.EndDateReserve == null)
                .ToListAsync();

            var advancedTrainings = await _context.advancedTrainings
                .Include(i => i.EducationalInstitutions)
                .Include(i => i.EmployeeRegistrationLog)
                .ToListAsync();

            var organizations = await _context.TableOrganizations
                .ToListAsync();
            TheListOfPersonnelReserveViewModel model = new TheListOfPersonnelReserveViewModel
            {
                Id = TableOrganizations,
                tableHistoryOfAppointments = historyOfAppointments,
                tableEducationals = educationals,
                reserveOfPersonnels = reserveOfPersonnels,
                advancedTrainingViewModels = advancedTrainings,
                Organizations = organizations,
                AllHistoryOfAppointments = AllhistoryOfAppointments,
                tablePositions = tablePositions
            };
            return model;
        }
        #endregion
        #region Формирования резерва лиц, включенных в кадровый резерв руководящих кадров по аппарату управления Белкоопсоюза
        [Authorize]
        public async Task<IActionResult> ApplicationReport5()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            return View(await Reports5(TableOrganizations));
        }
        #endregion
        #region на сохранения формирования резерва лиц, включенных в кадровый резерв руководящих кадров по аппарату управления Белкоопсоюза
        public async Task<IActionResult> SavingApplicationReport5(int id)
        {
            return View(await Reports5(id));
        }
        #endregion
        #region метод для заполнения модели
        private async Task<CertificationoOfEmployeesViewModel> Report6(int TableOrganizations)
        {
            var Organizations = await _context.TableOrganizations
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var historyOfAppointments = await _context.TableHistoryOfAppointments
                .Include(i =>i.Position)
                .Include(i =>i.Position.Position)
                .Include(i =>i.Position.Organizations)
                .ToListAsync();
            var verificationOfEducations = await _context.TableVerificationOfEducation
                .ToListAsync();
            var SubjectToCertification = await _context.TableHistoryOfAppointments
               .Include(i => i.Position)
               .Include(i => i.Position.Position)
               .Include(i => i.Position.Organizations)
               .ToListAsync();
            for (int i = 0;i < SubjectToCertification.Count;i++)
            {
                foreach (var item in verificationOfEducations)
                {
                    SubjectToCertification[i].EmployeeRegistrationLogId = 1;
                }
            }
            

            CertificationoOfEmployeesViewModel model = new CertificationoOfEmployeesViewModel
            {
                Id = TableOrganizations,
                Organizations = Organizations,
                historyOfAppointments = historyOfAppointments,
                verificationOfEducations = verificationOfEducations
            };
            return model;
        }
        #endregion
        #region Аттестация
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ApplicationReport6()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
           (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            return View(await Report6(TableOrganizations));
        }
        #endregion


    }
}   