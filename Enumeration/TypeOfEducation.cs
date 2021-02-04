using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.Enumeration
{
    public enum TypeOfEducation
    {
        [Display(Name = "Общее среднее образование")]
        Общее_среднее_образование,
        [Display(Name = "Специальное образование")]
        Специальное_образование, 
        [Display(Name = "Профессионально техническое образование")]
        Профессионально_техническое_образование,
        [Display(Name = "Cреднее специальное образование")]
        Cреднее_специальное_образование,
        [Display(Name = "Высшее образование")]
        Высшее_образование
    }
}
