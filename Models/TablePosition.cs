using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TablePosition
    {
        [Key]
        public int TablePositionId { get; set; }
        public DateTime DateOfJobRegistration { get; set; }
        public int CountPosition { get; set; }
        public string JobResponsibilities { get; set; }
        public int PositionId { get; set; }
        public int TableOrganizationsId { get; set; }
        public TableOrganizations Organizations { get; set; }
        public Position Position { get; set; }
        public List<TableHistoryOfAppointments> HistoryOfAppointments { get; set; }
        public TablePosition()
        {
            HistoryOfAppointments = new List<TableHistoryOfAppointments>();
        }
    }
}
