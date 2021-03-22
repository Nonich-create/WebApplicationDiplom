using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class ReserveOfPersonnel
    {
        [Key]
        public int ReserveId { get; set; }
        public string StatusReserve { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateReserve { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDateReserve { get; set; }
        public int? TablePositionId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public EmployeeRegistrationLog employeeRegistrationLog { get; set; }
        public TablePosition tablePosition { get; set; }
    }
}
