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
        public int TableOrganizationsId { get; set; }

        
        public string NameOfOrganization { get; set; }

        public string TypeOrganization { get; set; }

        public string Email { get; set; }

        public string Subordination { get; set; }

        public int? AddressId { get; set; }
        
        public string UserId { get; set; }
        public User? users { get; set; }
        public TableAddress? Address { get; set; }


        public List<TableVerificationOfEducation> VerificationOfEducation { get; set; }
        public List<TablePosition> TablePosition { get; set; }
        public List<EmployeeRegistrationLog>  employeeRegistrationLogs{ get; set; }
 
       
        public TableOrganizations()
        {
            employeeRegistrationLogs = new List<EmployeeRegistrationLog>();
            VerificationOfEducation = new List<TableVerificationOfEducation>();
            TablePosition = new List<TablePosition>();
        }
    }
}
    