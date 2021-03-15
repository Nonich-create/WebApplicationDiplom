using System.Collections.Generic;
using WebApplicationDiplom.Models;
namespace WebApplicationDiplom.ViewModels
{
    public class AdminWindowsViewModel
    {
        public int? OrganizationId { get; set; }
        public List<TableOrganizations> tableOrganizations { get; set; }
        public List<TableHistoryOfAppointments>? historyOfAppointments { get; set; }
    }
}
