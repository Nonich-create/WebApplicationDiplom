using System.Collections.Generic;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class TheListOfPersonnelReserveViewModel
    {
        public int Id { get; set; }
        public List<TableHistoryOfAppointments> tableHistoryOfAppointments { get; set; }
        public List<TableHistoryOfAppointments> AllHistoryOfAppointments { get; set; }
        public List<ReserveOfPersonnel> reserveOfPersonnels { get; set; }
        public List<TableEducational> tableEducationals { get; set; }
        public List<AdvancedTraining> advancedTrainingViewModels { get; set; }
        public List<EmployeeRegistrationLog> employeeRegistrationLogs { get; set; }
        public List<TableOrganizations> Organizations { get; set; }
        public List<TablePosition> tablePositions { get; set; }

    }
}
