using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        #region отображения страницы резерва кадров
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
        #endregion
        #region отображения регистрации в резерв
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

            var historyOfAppointments = await _context.TableHistoryOfAppointments
                .Include(i =>i.EmployeeRegistrationLog)
                .Include(i =>i.EmployeeRegistrationLog.Worker)
                .Include(i =>i.EmployeeRegistrationLog.Worker.positon)
                .Include(i =>i.Position)
                .Where(i => i.DateOfDismissal == null)
                .Where(i => i.EmployeeRegistrationLog.TableOrganizationsId == TableOrganizations)
                .ToListAsync();

            ReserveOfPersonnelViewModel model = new ReserveOfPersonnelViewModel 
            {
                employeeRegistrationLog = employees,
                positions = TablePositions,
                HistoryOfAppointments = historyOfAppointments,
            };

            return View(model);
        }
        #endregion
        #region регистрация в резерв
        public async Task<IActionResult> Create(ReserveOfPersonnelViewModel model)
        {
            try
            {

                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                     (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
                TableHistoryOfAppointments historyOfAppointments = await _context.TableHistoryOfAppointments
                    .Where(p => p.TablePositionId == model.TablePositionId)
                    .Where(p => p.DateOfDismissal == null)
                    .FirstOrDefaultAsync(p => p.EmployeeRegistrationLogId == model.EmployeeRegistrationLogId);
                if (historyOfAppointments == null)
                {
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
                }
                else
                {
                    return RedirectToAction("Create");
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }
        #endregion
        #region отображения вывыдо из резерва
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
        #endregion
        #region вывод из резерва и назначения на должность
        [HttpPost]
        public async Task<IActionResult> Edit(ReserveOfPersonnelViewModel model)
        {
            ReserveOfPersonnel reserve =  await _context.reserveOfPersonnels.FirstOrDefaultAsync(p => p.ReserveId == model.ReserveId);
            var TablePosition = _context.TablePosition.Find(reserve.TablePositionId);
            reserve.StatusReserve = "Назначен на должность";
            reserve.EndDateReserve = DateTime.Now;
            TableHistoryOfAppointments his = await _context.TableHistoryOfAppointments
                .Where(p => p.DateOfDismissal == null)
                .FirstOrDefaultAsync(p => p.TablePositionId == model.TablePositionId);
            if (TablePosition.CountPosition == 0 && his == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TableHistoryOfAppointments historyofappointments = new TableHistoryOfAppointments
                {
                    DateOfAppointment = DateTime.Now,
                    TablePositionId = reserve.TablePositionId,
                    EmployeeRegistrationLogId = reserve.EmployeeRegistrationLogId,
                };
                TablePosition.CountPosition = TablePosition.CountPosition - 1;

                _context.TableHistoryOfAppointments.Add(historyofappointments);
                _context.reserveOfPersonnels.Update(reserve);
                _context.TablePosition.Update(TablePosition);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");      
            }
        }
        #endregion
        #region Вывод из резерва
        public async Task<IActionResult> Remove(int? id)
        {
            if (id != null)
            {
                ReserveOfPersonnel reserve = await _context.reserveOfPersonnels.FirstOrDefaultAsync(p => p.ReserveId == id);
                if (reserve != null)
                {
                    reserve.StatusReserve = "Выведен из резерва";
                    reserve.EndDateReserve = DateTime.Now;
                    _context.reserveOfPersonnels.Update(reserve);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        #endregion
        #region удаления записи резерва
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                ReserveOfPersonnel reserve = await _context.reserveOfPersonnels.FirstOrDefaultAsync(p => p.ReserveId == id);
                if (reserve != null)
                {
                    _context.reserveOfPersonnels.Remove(reserve);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        #endregion
    }
}
