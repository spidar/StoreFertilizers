using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFertilizers.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }

        //[Display(Name = "ใบสั่งซื้อเลขที่")]
        public string PurchaseNumber { get; set; }

        //[Display(Name = "วันที่ซื่อเข้า")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

        //[Display(Name = "เลขที่บิล")]
        public string BillNumber { get; set; }

        //[Display(Name = "สินค้า")]
        //[Required(ErrorMessage = "กรุณาระบุสินค้า")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        //[Display(Name = "จำนวน")]
        //[Range(0, 999999999, ErrorMessage = "ค่าต้องอยู่ระหว่าง 0 ถึง 999999999")]
        public decimal Qty { get; set; }
        //[Display(Name = "ใช้ไปแล้วจำนวน")]
        public decimal QtyRemain { get; set; }

        //[Display(Name = "หน่วย")]
        //public int? UnitTypeID { get; set; }
        //public virtual UnitType UnitType { get; set; }

        //[Range(0.1, 999999999, ErrorMessage = "PurchasePrice must be between 1 and 999999999")]
        public decimal PurchasePricePerUnit { get; set; }

        //[Range(0.1, 999999999, ErrorMessage = "Price must be between 1 and 999999999")]
        public decimal SalePrice { get; set; }

        public decimal Amount { get; set; }

        //[Display(Name = "สินค้าภาษี")]
        public bool IsTax { get; set; }

        //[Range(0.00, 100.0, ErrorMessage = "VAT must be a % between 0 and 100")]
        public decimal VAT { get; set; }

        public int? ProviderID { get; set; }
        public virtual Provider Provider { get; set; }
        public string ProviderName { get; set; }

        public string Notes { get; set; }

        //public int PurchaseTypeID { get; set; }
        //[DisplayName("Expense category")]
        //public virtual PurchaseType PurchaseType { get; set; }

        /*
        #region Calculated fields
        [NotMapped]
        public decimal SubTotal
        {
            get
            {
                return Price;
            }
        }

        [NotMapped]
        public decimal TotalWithVAT
        {

            get
            {
                return Price + (Price * VAT / 100);
            }
        }

        [NotMapped]
        public decimal VATAmount
        {
            get
            {
                return TotalWithVAT - SubTotal;
            }
        }
        #endregion
        */
    }
}