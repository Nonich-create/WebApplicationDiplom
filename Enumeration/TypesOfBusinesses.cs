using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.Enumeration
{
   public enum TypesOfBusinesses
   {

        [Display(Name = "Райпо")]
        Райпо,
        [Display(Name = "Учреждения образования")]
        Учреждения_образования,
        [Display(Name = "Унитарная предприятие")]
        Унитарная_предприятия,
        [Display(Name = "Филиал")]
        Филиал,
        [Display(Name = "Заготторг")]
        Заготторг,
        [Display(Name = "Облпотребсоюз")]
        облпотребсоюз,
        [Display(Name = "Облпотребобщество")]
        облпотребобщество,
        [Display(Name = "Белкоопсоюз")]
        Белкоопсоюз,
        [Display(Name = "Cельскохозяйственное отделение")]
        Cельскохозяйственное_отделение,
        [Display(Name = "Торговое унитарное предприятие")]
        Торговое_унитарное_предприятие,
        [Display(Name = "Межрайонная торговая база")]
        Межрайонная_торговая_база,
        [Display(Name = "Кооптранс")]
        Кооптранс,
        [Display(Name = "Вычислительный центр")]
        Вычислительный_центр,
        [Display(Name = "Городская заготовительная контора")]
        Городская_заготовительная_контора

    }
}
