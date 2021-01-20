﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDiplom.Models
{
    public class TableAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public int localityId { get; set; }
        public Tablelocality locality { get; set; }
        public List<TableOrganizations> Organizations { get; set; }
        public TableAddress()
        {
            Organizations = new List<TableOrganizations>();
        }
    }
}