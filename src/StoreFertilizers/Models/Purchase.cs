using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFertilizers.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }

        public string PurchaseNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string BillNumber { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public decimal Qty { get; set; }
        public decimal QtyRemain { get; set; }
        public decimal PurchasePricePerUnit { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Amount { get; set; }
        public bool IsTax { get; set; }
        public decimal VAT { get; set; }

        public int? ProviderID { get; set; }
        public virtual Provider Provider { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public int OrgProductID { get; set; }
        [NotMapped]
        public decimal OrgQty { get; set; }
    }
}