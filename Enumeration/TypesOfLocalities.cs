using System.ComponentModel.DataAnnotations;


namespace WebApplicationDiplom.Enumeration
{
    public enum TypesOfLocalities
    {

        Сельсовет,
        Агрогородок,
        Деревня,
        Посёлок,
        Хутор,
        [Display(Name = "Городской посёлок")]
        Городской_посёлок,
        [Display(Name = "Рабочий посёлок")]
        Рабочий_посёлок,
        Город,
        [Display(Name = "Курортный посёлок")]
        Курортный_посёлок
    }
}
