using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StoreFertilizers.Models
{
    public class PaymentType
    {
        public int PaymentTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Descr { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}