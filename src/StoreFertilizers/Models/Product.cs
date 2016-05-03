using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StoreFertilizers.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public string Descr { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal Price { get; set; }

        public int? ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }

        public int? UnitTypeID { get; set; }
        public virtual UnitType UnitType { get; set; }

        //public virtual ICollection<InvoiceDetails> InvoiceDetials { get; set; }
    }
}