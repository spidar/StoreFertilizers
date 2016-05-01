using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace StoreFertilizers.Models
{
    public class ProductType
    {
        public int ProductTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Descr { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}