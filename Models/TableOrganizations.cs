using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{

    public class TableOrganizations
    {
        [Key]
        public int? TableOrganizationsId { get; set; }

        [Required(ErrorMessage = "Не указана название организации")]
        [DataType(DataType.Text)]
        [Display(Name = "Название организации")]
        public string? NameOfOrganization { get; set; }

        [Required(ErrorMessage = "Не указана тип организации")]
        [DataType(DataType.Text)]
        [Display(Name = "Тип организации")]
        public string? TypeOrganization { get; set; }

        [Required(ErrorMessage = "Не указана электронный адрес организации")]
        [DataType(DataType.Text)]
        [Display(Name = "Электронный адрес организации")]
        public string? Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Подченность организации")]
        public string? Subordination { get; set; }

        [Required(ErrorMessage = "Не указана Адрес организации")]
        [DataType(DataType.Text)]
        [Display(Name = "Адрес организации")]
        public int? AddressId { get; set; }


        public TableAddress? Address { get; set; }
        public List<TableVerificationOfEducation> VerificationOfEducation { get; set; }
        public List<TablePosition> TablePosition { get; set; }
        public List<EmployeeRegistrationLog>  employeeRegistrationLogs{ get; set; }
        public List<User> users { get; set; }
       
        public TableOrganizations()
        {
            users = new List<User>();
            employeeRegistrationLogs = new List<EmployeeRegistrationLog>();
            VerificationOfEducation = new List<TableVerificationOfEducation>();
            TablePosition = new List<TablePosition>();
        }
    }
}
