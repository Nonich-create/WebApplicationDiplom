using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.ViewModels
{
    public class QualificationViewModel
    {
        [Required(ErrorMessage = "Не указана квалификация")]
        [DataType(DataType.Text)]
        [Display(Name = "Квалификация")]
        public string Qualification { get; set; }
    }
}
