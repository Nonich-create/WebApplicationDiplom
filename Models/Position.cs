﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationDiplom.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }  
        public string JobTitle { get; set; }
        public List<TableVerificationOfEducation> VerificationOfEducation { get; set; }
        public List<TablePosition> TablePosition { get; set; }
        public Position()
        {
            VerificationOfEducation = new List<TableVerificationOfEducation>();
            TablePosition = new List<TablePosition>();
        }
    }
}
