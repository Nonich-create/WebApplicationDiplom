using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.ViewModels
{
    public class LoginViewModel
    {
               [Required]
            [Display(Name = "Идентификатор")]
            public string Identifier { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
                      [Display(Name = "Запомнить?")]
            public bool RememberMe { get; set; }
                    public string ReturnUrl { get; set; }
    }
}
