using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
  
    public class Item
    {
        public int Id { get; set; }

        public int ProductID { get; set; }

        public virtual Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}


