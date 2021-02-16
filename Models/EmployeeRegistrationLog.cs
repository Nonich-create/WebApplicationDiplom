﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class EmployeeRegistrationLog
    {
        [Key]
        public int EmployeeRegistrationId { get; set; }
        public DateTime EmployeeRegistrationDate { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int TableOrganizationsId { get; set; }
        public TableOrganizations Organizations { get; set; }
        public List<TableEducational> tableEducationals { get; set; }
        public List<AdvancedTraining> advancedTrainings { get; set; }
        public EmployeeRegistrationLog()
        {
            advancedTrainings = new List<AdvancedTraining>();
            tableEducationals = new List<TableEducational>();
        }
    }
}
