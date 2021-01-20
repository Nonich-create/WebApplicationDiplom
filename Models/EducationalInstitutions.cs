using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class EducationalInstitutions
    {
        public int EducationalInstitutionsId { get; set; }
        public string NameEducationalInstitutions { get; set; }
        public List<TableEducational> Educational { get; set; }
        public EducationalInstitutions()
        {
            Educational = new List<TableEducational>();
        }
    }
}
