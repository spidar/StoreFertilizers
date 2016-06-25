using System;

namespace StoreFertilizers.Models.ModelView
{
    public class InvoiceDetailsView
    {
        public int InvoiceID { get; set; }
        public bool IsTax { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductUnitTypeName { get; set; }
        public decimal Qty { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public decimal Amount { get; set; }
    }
}
