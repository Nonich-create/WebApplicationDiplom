using System.Collections.Generic;
using WebApplicationDiplom.Enumeration;
using WebApplicationDiplom.Models;
namespace WebApplicationDiplom.ViewModels
{
    public class InformationAboutTheAccountViewModel
    {
        public List<TableArea> areas { get; set; }
        public List<TableDistrict> districts { get; set; }
        public List<Tablelocality> localities { get; set; }
        public List<TableAddress> addresses { get; set; }
        public int? TableOrganizationsId { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public int areaId {get; set;}
        public TypesOfLocalities TypesOfLocalities { get; set; }
        public string Namelocality { get; set; }
        public List<TableOrganizations> TableOrganizations { get; set; }
        public List<User> users { get; set; }
        public TableAddress address { get; set; }
        public TableOrganizations Organizations { get; set; }
        public string NameOfOrganization { get; set; }
        public string TypeOrganization { get; set; }
        public string Email { get; set; }
        public int? SubordinationId { get; set; }
        public string UserId { get; set; }
     }
}
