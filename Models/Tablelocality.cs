﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class Tablelocality
    {
        [Key]
        public int localityId { get; set; }
        public string Typelocality { get; set; }
        public string Namelocality { get; set; }
        public int DistrictId { get; set; }
        public TableDistrict District { get; set; }
        public List<TableAddress> Address { get; set; }
        public Tablelocality()
        {
            Address = new List<TableAddress>();
        }
    }
}
