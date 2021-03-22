using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableHistoryOfAppointments
    {
        [Key]
        public int HistoryOfAppointmentsId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAppointment { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfDismissal { get; set; }
        public string TheReasonForTheDismissal { get; set; }
        public int? TablePositionId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public EmployeeRegistrationLog EmployeeRegistrationLog { get; set; }
        public TablePosition Position { get; set; }
    }
}
