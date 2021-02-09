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

        [Required(ErrorMessage = "Не указан работник")]
        [DataType(DataType.Text)]
        [Display(Name = "Тип организации")]
        public int WorkerId { get; set; }

        [Required(ErrorMessage = "Не указана должность ")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Не указана организация ")]
        [DataType(DataType.Text)]
        [Display(Name = "Организация")]
        public int TableOrganizationsId { get; set; }
       
        [Required(ErrorMessage = "Не указан тип аттестации ")]
        [DataType(DataType.Text)]
        [Display(Name = "Тип аттестации")]
        public int verificationOfTypesId { get; set; }

        public RecommendationsAfterCertification Recommendations { get; set; }
        public List<Worker> workers { get; set; }
        public List<Position> positions { get; set; }
        public List<TableOrganizations> organizations { get; set; }
        public List<VerificationOfType> verificationOfTypes { get; set; }
        public VerificationOfEducationViewModel()
        {
            verificationOfTypes = new List<VerificationOfType>();
            workers = new List<Worker>();
            positions = new List<Position>();
            organizations = new List<TableOrganizations>();
        }
        public VerificationOfType verificationOfType { get; set; }
        public Worker Worker { get; set; }
        public TableOrganizations Organizations { get; set; }
        public Position Position { get; set; }
    }
}
