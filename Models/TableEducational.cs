using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableEducational
    {
        [Key]
        public int EducationalId { get; set; }
        public string EducationType { get; set; }
        public string QualificationEducation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
      
        public int PositionId { get; set; }
        public int WorkerEmployeeRegistrationId { get; set; }
        public int QualificationId { get; set; }
        public TableQualification Qualification { get; set; }
        public int EducationalInstitutionsId  { get; set; }
        public EmployeeRegistrationLog Worker { get; set; }
        public Position position  { get; set; }
        public EducationalInstitutions EducationalInstitutions { get; set; }
   
    }
}
