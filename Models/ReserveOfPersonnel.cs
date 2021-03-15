using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class ReserveOfPersonnel
    {
        [Key]
        public int ReserveId { get; set; }
        public string StatusReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime? EndDateReserve { get; set; }
        public int? TablePositionId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public EmployeeRegistrationLog employeeRegistrationLog { get; set; }
        public TablePosition tablePosition { get; set; }
    }
}
