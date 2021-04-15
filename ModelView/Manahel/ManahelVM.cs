using ManhaleAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.ModelView.Manahel
{
    public class ManahelVM
    {
        public int Id { get; set; }

        [Display(Name="Manhal Code")]
        public string Ssn { get; set; }

        [Display(Name = "Manhal Name")]
        public string NickName { get; set; }

        [Display(Name = "Manhal Locattion")]
        public string LocationName { get; set; }

        [Display(Name = "Flower Name")]
        public string FlowerName { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Last Changes")]
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Manhal Images")]
        [NotMapped]
        public List<Images> ImageManhals { get; set; }
    }
}
