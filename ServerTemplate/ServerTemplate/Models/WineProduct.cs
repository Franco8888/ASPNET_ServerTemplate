using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTemplate.Models
{
    public class WineProduct
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Cultivar { get; set; }

        public double Price { get; set; }
    }
}
