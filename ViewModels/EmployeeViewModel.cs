using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class EmployeeViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрации работника")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Не указан работник ")]
        [DataType(DataType.Text)]
        [Display(Name = "Работник")]
        public int WorkerId { get; set; }

        [Required(ErrorMessage = "Не указана организация")]
        [DataType(DataType.Text)]
        [Display(Name = "Организация")]
        public int TableOrganizationsId { get; set; }

        public IEnumerable<TableOrganizations> organizations { get; set; }
        public IEnumerable<Worker> workers { get; set; }
    }
}
