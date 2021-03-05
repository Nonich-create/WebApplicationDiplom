using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
 
        //[HttpGet]
        //public async Task<IActionResult> DownloadFile(int id)
        //{
        //    Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/SavingReport/{id}");
        //    using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
        //    {
        //        //   client.DownloadFile(location, @"S:\sample.doc");
        //        //@"S:\sample.doc"
        //        //Response.Headers.Add("Content-Disposition", "inline;filename=somefile.ext");

        //        client.DownloadFile(location, @"S:\sample.doc"); ;//("content-disposition", attachment));

        //        // Or you can get the file content without saving it
        //    //  string htmlCode =   client.DownloadString(location);

        //    }
        //    return RedirectToAction("Index");
        //}

        //#region метод для скачки файла
        //[HttpGet]
        //private async Task<FileResult> DownloadFile(int id)
        //{
        //    Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/SavingReport/{id}");
        //    WebRequest request = WebRequest.Create(location);
        //       using (WebResponse response = await request.GetResponseAsync())
        //       {
        //        using (Stream stream = response.GetResponseStream())
        //        {

        //            byte[] buffer = new byte[16 * 12024];

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int read;
        //                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //                {
        //                    ms.Write(buffer, 0, read);
        //                }

        //                string file_type = "text/doc";
        //                string file_name = "sample.doc";
        //                return File(ms.ToArray(), file_type, file_name);
        //            }
        //        }
        //       }
        //}
        //#endregion

        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/SavingReport/{id}");
            using (WebClient client = new WebClient())
            {
                string file_type = "text/doc";
                string file_name = $"Председатели райпо - резерв {DateTime.Now.ToString()}.doc";
                return File(client.DownloadData(location), file_type, file_name);
            }
        }

        [HttpGet]
        public FileResult DownloadFileChairmenOfTheManagementBoards(int id)
        {
            Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/SavingChairmenOfTheManagementBoards/{id}");
            using (WebClient client = new WebClient())
            {
                string file_type = "text/doc";
                string file_name = $"Председатели райпо - резерв {DateTime.Now.ToString().Remove(10).Remove(0, 6)}.doc";
                return File(client.DownloadData(location), file_type, file_name);
            }
        }

        public async Task<IActionResult> SavingChairmenOfTheManagementBoards(int id)
        {
            return View(await TheModelIsFilledWithAllTheData(id));
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
        #region Формирования резерва на должности председателей правлений облпотребсоюзов, облпотребобществ на сохранения
        public async Task<IActionResult> SavingApplicationReport(int id)
        {
            return View(await CreateApplicationReport(id));
        }
        #endregion
        #region Загрузка  списка резерва на должности председателей правлений облпотребсоюзов, облпотребобществ
        [HttpGet]
        public FileResult DownloadFileApplicationReport(int id)
        {
            Uri location = new Uri($"{Request.Scheme}://{Request.Host}/Reports/SavingApplicationReport/{id}");
            using (WebClient client = new WebClient())
            {
                string file_type = "text/doc";
                string file_name = $"Резерв на пред. правлений ОПС на 2020 (приложение 2) {DateTime.Now.ToString().Remove(10).Remove(0, 6)}.doc";
                return File(client.DownloadData(location), file_type, file_name);
            }
        }
        #endregion
    }
}   