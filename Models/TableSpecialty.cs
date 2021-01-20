using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
