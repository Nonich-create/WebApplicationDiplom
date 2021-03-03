using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;

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


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Не указана должность")]
        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public List<Position> positions = new List<Position>();
        public Position Position { get; set; }

    }
}
