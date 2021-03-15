using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class AdvancedTraining
    {
        [Key]
        public int AdvancedTrainingId { get; set; }
        public int EmployeeRegistrationLogId { get; set; }
        public int EducationalInstitutionsId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public EducationalInstitutions EducationalInstitutions { get; set; }
        public EmployeeRegistrationLog EmployeeRegistrationLog { get; set;}
    }
}
