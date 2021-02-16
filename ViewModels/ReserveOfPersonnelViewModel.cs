using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class ReserveOfPersonnelViewModel
    {
        public List<Worker> workers { get; set; }
        public List<TablePosition> positions { get; set; }

        public int ReserveId { get; set; }
        public int WorkerId { get; set; }
        public int TablePositionId { get; set; }
        public string StatusReserve { get; set; }
        public DateTime StartDateReserve { get; set; }
        public DateTime? EndDateReserve { get; set; }
        public ReserveOfPersonnelViewModel()
        {
            workers = new List<Worker>();
        }
        public Worker Worker { get; set; }
        public TablePosition tablePosition { get; set; }
    }
}
