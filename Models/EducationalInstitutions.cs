using System.Collections.Generic;
namespace WebApplicationDiplom.Models
{
    public class EducationalInstitutions
    {
        public int EducationalInstitutionsId { get; set; }
        public string NameEducationalInstitutions { get; set; }
        public string Abbreviation { get; set; }
        public List<TableEducational> Educational { get; set; }
        public List<AdvancedTraining> advancedTrainings { get; set; }
        public EducationalInstitutions()
        {
            advancedTrainings = new List<AdvancedTraining>();
            Educational = new List<TableEducational>();
        }
    }
}
