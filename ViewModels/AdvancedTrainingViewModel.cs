using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class AdvancedTrainingViewModel
    {
        public int AdvancedTrainingId { get; set; }
        public int educationalsId { get; set; }
        public int employeesId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime Start { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата окончания")]
        public DateTime? End { get; set; }
        public EmployeeRegistrationLog Employee { get; set; }
        public EducationalInstitutions Educational { get; set; }
        public List<EmployeeRegistrationLog> employees { get; set; }
        public List<EducationalInstitutions> educationals { get; set; }
    }
}
