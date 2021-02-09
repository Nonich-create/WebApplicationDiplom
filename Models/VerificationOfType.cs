using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class VerificationOfType
    {
        public int VerificationOfTypeId { get; set; }
        public string VerificationOfTypeName { get; set; }
        public List<TableVerificationOfEducation> VerificationOfEducation { get; set; }
        public VerificationOfType()
        {
            VerificationOfEducation = new List<TableVerificationOfEducation>();
        }
    }
}
