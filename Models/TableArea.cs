using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableArea
    {
        [Key]
        public int AreaId { get; set; }
        public string NameArea { get; set; }
        public List<Tablelocality> locality { get; set; }
        public TableArea()
        {
            locality = new List<Tablelocality>();
        }
    }
}
