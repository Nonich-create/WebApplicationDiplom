using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
