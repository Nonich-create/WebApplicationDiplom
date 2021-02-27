using System.Collections.Generic;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class InformationAboutTheAccountViewModel
    {
        public List<TableArea> areas { get; set; }
        public List<TableDistrict> districts { get; set; }
        public List<Tablelocality> localities { get; set; }
        public List<TableAddress> addresses { get; set; }
        public List<TableOrganizations> TableOrganizations { get; set; }
        
    }
}
