using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableOrganizations
    {
        [Key]
        public int TableOrganizationsId { get; set; }
        public string? NameOfOrganization { get; set; }
        public string? TypeOrganization { get; set; }
        public string? WebSite { get; set; }
        public string? Email { get; set; }
        public string? Identifier { get; set; }
        public int? AddressId { get; set; }
        public TableAddress Address { get; set; }
        public List<TableVerificationOfEducation> VerificationOfEducation { get; set; }
        public List<TablePosition> TablePosition { get; set; }
        public TableOrganizations()
        {
            VerificationOfEducation = new List<TableVerificationOfEducation>();
            TablePosition = new List<TablePosition>();
        }
    }
}
