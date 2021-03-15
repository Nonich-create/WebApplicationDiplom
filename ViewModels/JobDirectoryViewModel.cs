using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.ViewModels
{
    public class JobDirectoryViewModel
    {
        [Required(ErrorMessage = "Не указана должность")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public string JobTitle { get; set; }
     }
}
