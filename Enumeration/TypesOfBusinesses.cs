using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.Enumeration
{
   public enum TypesOfBusinesses
   {

        [Display(Name = "Райпо")]
        Райпо,
        [Display(Name = "Учреждения образования")]
        Учреждения_образования,
        [Display(Name = "Унитарная предприятия")]
        Унитарная_предприятия,
        [Display(Name = "Филиал")]
        Филиал,
        [Display(Name = "Заготторг")]
        Заготторг,
        [Display(Name = "Облпотребсоюз")]
        облпотребсоюз,
            [Display(Name = "Облпотребобщество")]
        облпотребобщество


    }
}
