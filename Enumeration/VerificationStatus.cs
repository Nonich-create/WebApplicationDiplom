using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.Enumeration
{
    public enum VerificationStatus
    {
        [Display(Name = "Не аттестован")]
        Не_аттестован,
        [Display(Name = "Аттестован")]
        Аттестован,
        [Display(Name = "Условно аттестован")]
        Условно_аттестован
    }
}
