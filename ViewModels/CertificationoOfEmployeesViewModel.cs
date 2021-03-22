using System.Collections.Generic;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class CertificationoOfEmployeesViewModel
    {
        public int Id { get; set; }
        public List<TableOrganizations> Organizations { get; set; }
        public List<TableHistoryOfAppointments> historyOfAppointments = new List<TableHistoryOfAppointments>();
        public List<TableVerificationOfEducation> verificationOfEducations = new List<TableVerificationOfEducation>();
        public List<TableHistoryOfAppointments> SubjectToCertification = new List<TableHistoryOfAppointments>();
         
    }
}
