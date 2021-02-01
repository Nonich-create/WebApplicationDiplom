using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public string Identifier { get; set; }
        public int? TableOrganizationsId { get; set; }
        public virtual TableOrganizations TableOrganizations { get; set; }
    }
}
