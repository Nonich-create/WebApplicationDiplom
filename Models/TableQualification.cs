using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableQualification
    {
        [Key]
        public int QualificationId { get; set; }
        public string Qualification { get; set; }
        public List<TableEducational> Educational { get; set; }
        public TableQualification()
        {
            Educational = new List<TableEducational>();
        }
    }
}
