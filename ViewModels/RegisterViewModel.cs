using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class RegisterViewModel
    {




        //[Required]
        //[Display(Name = "Идентификатор")]
        public string Identifier { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        //[Required(ErrorMessage = "Не указана название организации")]
        //[DataType(DataType.Text)]
        //[Display(Name = "Название организации")]
        public string OrganizationName { get; set; }

        //[Required(ErrorMessage = "Не указана тип организации")]
        //[DataType(DataType.Text)]
        //[Display(Name = "Тип организации")]
      //  public string TypeOrganization { get; set; }

        //[Required(ErrorMessage = "Не указана электронный адрес организации")]
        //[DataType(DataTyp
        //[Display(Name = "Электронный адрес организации")]e.Text)]
        public string Email { get; set; }

        //[DataType(DataType.Text)]
        //[Display(Name = "Подченность организации")]
        public string Subordination { get; set; }
        public TypesOfBusinesses Businesses { get; set; }
        //[Required(ErrorMessage = "Не указана Адрес организации")]
        //[DataType(DataType.Text)]
        //[Display(Name = "Адрес организации")]
        // public int? AddressId { get; set; }
    }
}