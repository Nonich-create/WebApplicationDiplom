using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TablePosition
    {
        [Key]
        public int TablePositionId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfJobRegistration { get; set; }
        public int CountPosition { get; set; }
        public string JobResponsibilities { get; set; }
        public int PositionId { get; set; }
        public int TableOrganizationsId { get; set; }
        public TableOrganizations Organizations { get; set; }
        public Position Position { get; set; }
        public List<TableHistoryOfAppointments> HistoryOfAppointments { get; set; }
        public List<ReserveOfPersonnel> reserveOfPersonnels { get; set; }
        public TablePosition()
        {
            reserveOfPersonnels = new List<ReserveOfPersonnel>();
            HistoryOfAppointments = new List<TableHistoryOfAppointments>();
             
        }
    }
}
