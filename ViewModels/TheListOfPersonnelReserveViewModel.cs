using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class TheListOfPersonnelReserveViewModel
    {
         
        public List<TableHistoryOfAppointments> tableHistoryOfAppointments { get; set; }
        public List<ReserveOfPersonnel> reserveOfPersonnels { get; set; }
        public List<TableEducational> tableEducationals { get; set; }
        public List<AdvancedTraining> advancedTrainingViewModels { get; set; }
       
       
    }
}
