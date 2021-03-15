using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;
namespace WebApplicationDiplom.ViewModels
{
    public class HistoryOfAppointmentsViewModel
    {
        public int HistoryOfAppointmentsId { get; set; }
              [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата приема на работу")]
        public DateTime DateOfAppointment { get; set; }
                [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата увольнения работника")]
        public DateTime? DateOfDismissal { get; set; }
                 [DataType(DataType.Text)]
        [Display(Name = "Причина увольнения")]
        public string TheReasonForTheDismissal { get; set; }
                [Required(ErrorMessage = "Не указана должность")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int? TablePositionId { get; set; }
                [Required(ErrorMessage = "Не указана работник")] 
        [DataType(DataType.Text)]
        [Display(Name = "Работник")]
        public int EmployeeRegistrationLogId { get; set; }
              public GroundsForDismissal GroundsForDismissal { get; set; }
        public List<EmployeeRegistrationLog> employeeRegistrationLogs { get; set; }
        public List<TablePosition> positions { get; set; }
              }
}
