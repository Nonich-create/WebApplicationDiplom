using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationDiplom.Models
{
    public class TableSpecialty
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string Specialty { get; set; }
        public List<TableEducational> Educational { get; set; }
        public TableSpecialty()
        {
            Educational = new List<TableEducational>();
        }
    }
}
