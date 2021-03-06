﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableOrganizations
    {
        [Key]
        public int TableOrganizationsId { get; set; }
        public string NameOfOrganization { get; set; }
        public string TypeOrganization { get; set; }
        public string Email { get; set; }
        public int? SubordinationId { get; set; }
        public string UserId { get; set; }
        public User? users { get; set; }
        public List<TablePosition> TablePosition { get; set; }
        public List<EmployeeRegistrationLog>  employeeRegistrationLogs{ get; set; }
        public List<TableAddress> tableAddresses { get; set; }
        public TableOrganizations()
        {
            tableAddresses = new List<TableAddress>();
            employeeRegistrationLogs = new List<EmployeeRegistrationLog>();
            TablePosition = new List<TablePosition>();
        }
    }
}
    