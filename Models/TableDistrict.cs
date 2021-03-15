using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableDistrict
    {
        [Key]
        public int DistrictId { get; set; }
        public string NameDistrict { get; set; }
        public int? AreaId { get; set; }
        public TableArea Area { get; set; }
        public List<Tablelocality> locality { get; set; }
        public TableDistrict()
        {
            locality = new List<Tablelocality>();
        }
    }
}
