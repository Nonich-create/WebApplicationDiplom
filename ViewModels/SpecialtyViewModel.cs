using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.ViewModels
{
    public class SpecialtyViewModel
    {
        [Required(ErrorMessage = "Не указана специальность")]
        [DataType(DataType.Text)]
        [Display(Name = "Специальность")]
        public string Specialty { get; set; }
    }
}
