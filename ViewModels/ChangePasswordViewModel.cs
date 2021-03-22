using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string NewPassword { get; set; }
    }
}
    