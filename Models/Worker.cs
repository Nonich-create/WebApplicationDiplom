using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class Worker
    {

        [Key]
        public int WorkerId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string DoubleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<EmployeeRegistrationLog> employeeRegistrationLogs { get; set; }
        public Worker()
        {
            employeeRegistrationLogs = new List<EmployeeRegistrationLog>();
        }
     
      
    }
}
