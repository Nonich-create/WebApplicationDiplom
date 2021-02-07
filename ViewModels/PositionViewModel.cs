﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class PositionViewModel
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрации вакансии")]
        public DateTime DateOfJobRegistration { get; set; }

        [Required(ErrorMessage = "Не указана количество мест")]
        [DataType(DataType.Text)]
        [Display(Name = "Количество мест")]
        public int CountPosition { get; set; }  


        [DataType(DataType.Text)]
        [Display(Name = "Должностные обязанности")]
        public string JobResponsibilities { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int PositionId { get; set; }


        [Required(ErrorMessage = "Не указана организация")]
        [DataType(DataType.Text)]
        [Display(Name = "Организация")]
        public int TableOrganizationsId { get; set; }


        public IEnumerable<TableOrganizations> organizations { get; set; }
        public IEnumerable<Position> positions { get; set; }
 
    
    }
}
