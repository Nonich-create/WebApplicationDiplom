using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.ViewModels
{
    public class RegisterWorkerViewModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не указана имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана отчёство")]
        [Display(Name = "Отчёство")]
        public string DoubleName { get; set; }
        [Required(ErrorMessage = "Не указана дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
    }
}
