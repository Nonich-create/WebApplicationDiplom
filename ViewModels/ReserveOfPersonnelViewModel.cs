using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationDiplom.Models;

namespace WebApplicationDiplom.ViewModels
{
    public class ReserveOfPersonnelViewModel
    {

        
        [DataType(DataType.Text)]
        [Display(Name = "Состояние в резерве")]
        public string StatusReserve { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата зачисления резерв")]
        public DateTime StartDateReserve { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата исключения из резерва")]
        public DateTime? EndDateReserve { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public int TablePositionId { get; set; }

        [Required(ErrorMessage = "Не указан работник")]
        [DataType(DataType.Text)]
        [Display(Name = "Работник")]
        public int WorkerId { get; set; }


        public Worker Worker { get; set; }
        public Position Position { get; set; }

        public List<Worker> workers { get; set; }
        public List<TablePosition> positions { get; set; }

        public ReserveOfPersonnelViewModel()
        {
            workers = new List<Worker>();
            positions = new List<TablePosition>();
        }
    }
}
