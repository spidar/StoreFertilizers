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

        public virtual ICollection<InvoiceDetails> InvoiceDetials { get; set; }
    }
}