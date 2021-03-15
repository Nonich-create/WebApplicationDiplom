﻿using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableVerificationOfEducation
    {
        [Key]
        public int VerificationOfEducationId { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime DateOfVerification { get; set; }
        public DateTime DateOfCertificationCompletion { get; set; }
        public string Recommendations { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public int PositionId { get; set; }
        public EmployeeRegistrationLog employeeRegistrationLog { get; set; }
        public Position Position { get; set; }
    }
}
