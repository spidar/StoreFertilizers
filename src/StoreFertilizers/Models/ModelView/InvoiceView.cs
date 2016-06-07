using System;

namespace StoreFertilizers.Models.ModelView
{
    public class InvoiceView
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string CustomerName { get; set; }
        public bool? Paid { get; set; }
        public decimal NetTotal { get; set; }
        public bool IsTax { get; set; }
        public string Notes { get; set; }
    }
}
