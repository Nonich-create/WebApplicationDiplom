using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class Tablelocality
    {
        [Key]
        public int localityId { get; set; }
        public string Typelocality { get; set; }
        public string Namelocality { get; set; }
        public int AreaId { get; set; }
        public TableArea Area { get; set; }
        public List<TableAddress> Address { get; set; }
        public Tablelocality()
        {
            Address = new List<TableAddress>();
        }
    }
}
