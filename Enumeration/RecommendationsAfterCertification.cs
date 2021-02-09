using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Enumeration
{
    public enum RecommendationsAfterCertification
    {
        [Display(Name = "Не требует рекомендацией")]
        Не_требует_рекомендацией,
        [Display(Name = "Для зачисления врезерв")]
        Для_зачисления_врезерв,
        [Display(Name = "Для занятия вышестоящей должности")]
        Для_занятия_вышестоящей_должности,
        [Display(Name = "Повысить квалификационную категорию")]
        Повысить_квалификационную_категорию,
        [Display(Name = "Повысить квалификационную категорию")]
        Пройти_переподготовку,
        [Display(Name = "Пройти переподготовку")]
        Получить_профильное_образование





    }
}
