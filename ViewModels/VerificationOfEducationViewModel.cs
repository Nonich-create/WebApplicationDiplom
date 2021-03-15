using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;
namespace WebApplicationDiplom.ViewModels
{
    public class VerificationOfEducationViewModel
    {
                public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Статус прохождения аттестации")]
        public string VerificationStatus { get; set; }
                [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрирование аттестации")]
        public DateTime DateOfVerification { get; set; }
                [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата прохождение аттестации")]
        public DateTime DateOfCertificationCompletion { get; set; }
                [Required(ErrorMessage = "Не указана должность ")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int PositionId { get; set; }
                [Required(ErrorMessage = "Не указан работник ")]
        [DataType(DataType.Text)]
        [Display(Name = "Работник")]
        public int EmployeeRegistrationLogId { get; set; }
               [DataType(DataType.Text)]
        [Display(Name = "Рекомендации")]
        public string Recommendations { get; set; }
               public RecommendationsAfterCertification EnumerationRecommendations { get; set; }
        public VerificationStatus EnumerationStatus { get; set; }
        public List<EmployeeRegistrationLog> employeeRegistrationLogs { get; set; }
        public List<Position> positions { get; set; }
    }
}
