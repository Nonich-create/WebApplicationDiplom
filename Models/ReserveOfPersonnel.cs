using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class ReserveOfPersonnel
    {

        [Key]
        public int ReserveId { get; set; }
        public string StatusReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime? EndDateReserve { get; set; }
        public int TablePositionId { get; set; }
       
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public TablePosition tablePosition { get; set; }
    }
}
