using System.ComponentModel.DataAnnotations;


namespace WebApplicationDiplom.ViewModels
{
    public class EducationalInstitutionsViewModel
    {
        [Required(ErrorMessage = "Не указана название")]
        [DataType(DataType.Text)]
        [Display(Name = "Название")]
        public string NameEducationalInstitutions { get; set; }

        [Required(ErrorMessage = "Не указана аббревиатура")]
        [DataType(DataType.Text)]
        [Display(Name = "Аббревиатура")]
        public string Abbreviation { get; set; }
    }
}
