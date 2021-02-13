using System.Collections.Generic;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class AddressViewModel
    {
        public IEnumerable<TableArea> areas { get; set; }
        public IEnumerable<TableDistrict> districts { get; set; }
        public IEnumerable<Tablelocality> localities { get; set; }
        public IEnumerable<TableAddress> addresses { get; set; }
    }
}
