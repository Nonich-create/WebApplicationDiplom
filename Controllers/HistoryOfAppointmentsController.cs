using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        #region отображения принятых на должность работников
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
            (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
          
            var historyofappointments = _context.TableHistoryOfAppointments
                .Include(p => p.Position)
                .Include(p => p.EmployeeRegistrationLog)
                .Include(p => p.EmployeeRegistrationLog.Worker)
                .Include(p => p.Position.Position)
                .Where(p => p.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations);
          
            return View(await historyofappointments.ToListAsync());
        }
        #endregion
        #region отображения принятия на должность работника
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
           int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;

            var employees = await _context.employeeRegistrationLogs
               .Include(i => i.Worker)
               .OrderBy(i => i.Worker.Surname)
               .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var position = await _context.TablePosition
                .Include(i => i.Position)
                .OrderBy(i => i.Position.JobTitle)
                .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();

            var historyofappointments = _context.TableHistoryOfAppointments
              .Include(p => p.Position)
              .Include(p => p.EmployeeRegistrationLog)
              .Include(p => p.EmployeeRegistrationLog.Worker)
              .Include(p => p.Position.Position)
              .Where(p => p.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations)
              .Where(p => p.DateOfDismissal == null);

             foreach (var item in historyofappointments)
             {
                foreach(var itememployees in employees)
                {
                    if (item.EmployeeRegistrationLogId == itememployees.EmployeeRegistrationId)
                    {
                        employees.Remove(itememployees);
                        break;
                    }
                }
             }
            HistoryOfAppointmentsViewModel model = new HistoryOfAppointmentsViewModel
            { 
               employeeRegistrationLogs = employees,
               positions = position,  
            }
            ;
            return View(model);
        }
        #endregion
        #region назначения работника на должность в организации
        [HttpPost]
        public async Task<IActionResult> Create(HistoryOfAppointmentsViewModel model)
        {
            try
            {
                var tableposition = await _context.TablePosition.FirstOrDefaultAsync(p => p.TablePositionId == model.TablePositionId);
                if (tableposition.CountPosition > 0)
                {
                    if (ModelState.IsValid)
                    {
                        TableHistoryOfAppointments historyofappointments = new TableHistoryOfAppointments
                        {
                            DateOfAppointment = DateTime.Now,
                            TablePositionId = model.TablePositionId,
                            EmployeeRegistrationLogId = model.EmployeeRegistrationLogId
                        };
                        tableposition.CountPosition = tableposition.CountPosition - 1;
                        _context.TablePosition.Update(tableposition);
                        _context.TableHistoryOfAppointments.Add(historyofappointments);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                         (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
                        var position = await _context.TablePosition
                          .Include(i => i.Position)
                       .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
                        var employees = await _context.employeeRegistrationLogs
               .Include(i => i.Worker)
               .Include(i => i.Organizations)
               .Where(i => i.TableOrganizationsId == TableOrganizations).ToListAsync();
                        HistoryOfAppointmentsViewModel modelResult = new HistoryOfAppointmentsViewModel
                        {

                            employeeRegistrationLogs = employees,
                            positions = position,
                        };
                        return View(modelResult);
                    }
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }
        #endregion
        #region отображения увольнения работника с должности
        [HttpGet]
        public async Task<IActionResult> Layoffs(int? id)
        { 
            if (id != null)
            {
                TableHistoryOfAppointments historyOfAppointments = await _context.TableHistoryOfAppointments.FirstOrDefaultAsync(p => p.HistoryOfAppointmentsId == id);
                if (historyOfAppointments.DateOfDismissal == null)
                {
                    HistoryOfAppointmentsViewModel model = new HistoryOfAppointmentsViewModel
                    {
                        HistoryOfAppointmentsId = historyOfAppointments.HistoryOfAppointmentsId,
                        DateOfAppointment = historyOfAppointments.DateOfAppointment,
                        DateOfDismissal = historyOfAppointments.DateOfDismissal,
                        TablePositionId = historyOfAppointments.TablePositionId,
                        EmployeeRegistrationLogId = historyOfAppointments.EmployeeRegistrationLogId,
                        TheReasonForTheDismissal = historyOfAppointments.TheReasonForTheDismissal
                    };
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        #endregion
        #region увольнения работника
        [HttpPost]
        public async Task<IActionResult> Layoffs(HistoryOfAppointmentsViewModel model)
        {
          
                TableHistoryOfAppointments historyOfAppointments = await _context.TableHistoryOfAppointments.FirstOrDefaultAsync(p => p.HistoryOfAppointmentsId == model.HistoryOfAppointmentsId);
                var tableposition = await _context.TablePosition.FirstOrDefaultAsync(p => p.TablePositionId == historyOfAppointments.TablePositionId);
                tableposition.CountPosition = tableposition.CountPosition + 1;
                historyOfAppointments.TheReasonForTheDismissal = model.GroundsForDismissal.ToString();
                historyOfAppointments.DateOfDismissal = DateTime.Now;
                _context.TableHistoryOfAppointments.Update(historyOfAppointments);
                _context.TablePosition.Update(tableposition);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }
        #endregion
    }
}
