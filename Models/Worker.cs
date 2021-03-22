using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string DoubleName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        public List<EmployeeRegistrationLog> employeeRegistrationLogs { get; set; }
        public Worker()
        {
            employeeRegistrationLogs = new List<EmployeeRegistrationLog>();
        }
    }
}
