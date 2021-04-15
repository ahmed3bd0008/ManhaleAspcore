using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ManhaleAspNetCore.Models;

namespace ManhaleAspNetCore.ModelView.Khalia
{
    public class KhaliaVM
    {
        public int Id { get; set; }

        [Display(Name = "Khalia Number"),Required]
        public int Ssn { get; set; }

        [Display(Name = "Khalia Level")]
        public string KhaliaLevel { get; set; }

        [Display(Name = "Khalia Type")]
        public string KhaliaType { get; set; }

        [Display(Name = "Wood Type")]
        public string Wood { get; set; }

        [Display(Name = "Prwaz Count"),Required]
        public int PraweezCount { get; set; }
        public string Notes { get; set; }

        public int ManhalId { get; set; }
        public virtual ManahelContext Manhal { get; set; }
        public virtual System.Collections.Queue Queues { get; set; }
    }
}
