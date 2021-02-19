using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class EducationalViewModel
    {
        public TypeOfEducation EducationType { get; set; }
        public QualificationEducation QualificationEducation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала обучения")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата окончания обучения")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Не указана место образования")]
        [DataType(DataType.Text)]
        [Display(Name = "Учерждения образования")]
        public int EducationalInstitutionsId { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Не указан работник ")]
        [DataType(DataType.Text)]
        [Display(Name = "Работник")]
        public int EmployeeRegistrationLogId { get; set; }

        [Required(ErrorMessage = "Не указана квалификация")]
        [DataType(DataType.Text)]
        [Display(Name = "Квалификация")]
        public int QualificationId { get; set; }
        public List<EmployeeRegistrationLog> workers { get; set; }
        public IEnumerable<EducationalInstitutions> educationals { get; set; }
        public IEnumerable<TableQualification> qualifications { get; set; }
      
        public IEnumerable<Position> positions { get; set; }

    }
}
