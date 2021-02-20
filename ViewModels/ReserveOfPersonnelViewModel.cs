using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class ReserveOfPersonnelViewModel
    {
        public List<EmployeeRegistrationLog> employeeRegistrationLog { get; set; }
        public List<TablePosition> positions { get; set; }
        public List<TableHistoryOfAppointments> HistoryOfAppointments { get; set; }
        public int ReserveId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public int? TablePositionId { get; set; }
        public string StatusReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime? EndDateReserve { get; set; }
 
        public TablePosition tablePosition { get; set; }
    }
}
