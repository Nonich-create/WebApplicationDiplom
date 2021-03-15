using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class TableAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public int localityId { get; set; }
        public int TableOrganizationsId { get; set; }
        public Tablelocality locality { get; set; }
        public TableOrganizations Organizations {get;set;}

    }
}
