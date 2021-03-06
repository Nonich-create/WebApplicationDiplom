﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplicationDiplom.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public string Identifier { get; set; }
        public List<TableOrganizations> tableOrganizations { get; set; }
        public User()
        {
            tableOrganizations = new List<TableOrganizations>();
        }
    }
}
