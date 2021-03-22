using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;
namespace WebApplicationDiplom.ViewModels
{
    public class RegisterViewModel
    {
        public int TableOrganizationsId { get; set; }
        [Required]
        [Display(Name = "Идентификатор")]
        public string Identifier { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Праввильный формат пароля dY2-AJX-mJd-zuL")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
         [Required]
         [Compare("Password", ErrorMessage = "Пароли не совпадают")]
         [DataType(DataType.Password)]
         [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Не указана название организации")]
        [DataType(DataType.Text)]
        [Display(Name = "Название организации")]
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public int? SubordinationId { get; set; }
        public TypesOfBusinesses Businesses { get; set; }
        public List<TableOrganizations> organizations = new List<TableOrganizations>(); 
    }
}