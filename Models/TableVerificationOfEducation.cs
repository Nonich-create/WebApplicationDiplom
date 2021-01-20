using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableVerificationOfEducation
    {
        [Key]
        public int VerificationOfEducationId { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime DateOfVerification { get; set; }
        public int WorkerId { get; set; }
        public int PositionId { get; set; }
        public int TableOrganizationsId { get; set; }
        public Worker Worker { get; set; }
        public TableOrganizations Organizations { get; set; }
        public Position Position { get; set; }
    }
}
