using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StoreFertilizers.Models
{
    public class UnitType
    {
        public int UnitTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Descr { get; set; }

        //public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}