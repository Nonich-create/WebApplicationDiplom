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
        public List<TableDistrict>  District { get; set; }
        public TableArea()
        {
            District = new List<TableDistrict>();
        }
    }
}
