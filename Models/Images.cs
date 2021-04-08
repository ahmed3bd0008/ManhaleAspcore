using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string ImagesString { get; set; }
        public int TabelId { get; set; }
        public string TabelName { get; set; }
    }
}
