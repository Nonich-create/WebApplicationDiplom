using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableHistoryOfAppointments
    {
        [Key]
        public int HistoryOfAppointmentsId { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public DateTime? DateOfDismissal { get; set; }
        public string TheReasonForTheDismissal { get; set; }
        public int? TablePositionId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public EmployeeRegistrationLog EmployeeRegistrationLog { get; set; }
        public TablePosition Position { get; set; }
    }
}
