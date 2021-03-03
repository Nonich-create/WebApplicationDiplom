using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;
using WebApplicationDiplom.ViewModels;
 
namespace WebApplicationDiplom.Controllers
{
    public class PositionController : Controller
    {
        public readonly ApplicationContext _context;

        public PositionController(ApplicationContext context)
        {
            _context = context;
        }
        #region отображения должностей в организации
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
              (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            var positiones = _context.TablePosition.Include(p => p.Position).Where(p => p.TableOrganizationsId == TableOrganizations);
            return View(await positiones.ToListAsync());
        }
        #endregion
        #region отображения регистрации должности в организации
        public async Task<IActionResult> CreateAsync() 
        {
            var positions = await _context.Position.ToListAsync();
          //  ViewBag.PositionId = new SelectList(_context.Position, "PositionId", "JobTitle");
            PositionViewModel model = new PositionViewModel
            {
                positions = positions,
            };
            return View(model);
        }
        #endregion
        #region регистрация должности в организации
        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel model)
        {
            if (ModelState.IsValid)
            {
                int TableOrganizations = _context.TableOrganizations.Include(i => i.users).FirstOrDefault
                 (i => User.Identity.Name == i.users.UserName).TableOrganizationsId;
            
                TablePosition position = new TablePosition
                {
                    DateOfJobRegistration = DateTime.Now,
                    CountPosition = model.CountPosition,
                    JobResponsibilities = model.JobResponsibilities,
                    PositionId = model.PositionId,
                    TableOrganizationsId = TableOrganizations,
                };
                    
                _context.TablePosition.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var positions = await _context.Position.ToListAsync();
                model.positions = positions;
                return View(model);
            }
           
        }
        #endregion
        #region отображения добавления должности в справочник
        [HttpGet]
        public IActionResult CreateJob()
        {
            return View();
        }
        #endregion
        #region добавления должности в справочник
        [HttpPost]
        public async Task<IActionResult> CreateJob(JobDirectoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Position position = await _context.Position.FirstOrDefaultAsync
                    (i => i.JobTitle == model.JobTitle);

                Position positionResult = new Position
                {
                        JobTitle = model.JobTitle
                };
                if (position == null)
                {
                    await _context.Position.AddAsync(positionResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
           
              
            }
            return View(model);
        }
        #endregion
    }
}
    