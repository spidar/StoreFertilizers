using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFertilizers.Models
{
    public class InvoiceDetails
    {
        public int InvoiceDetailsID { get; set; }

        [Display(Name = "ใบส่งสินค้าเลขที่")]
        public int InvoiceID { get; set; }
        //public virtual Invoice Invoice { get; set; }

        [Display(Name = "สินค้า")]
        //[Required(ErrorMessage = "กรุณาระบุสินค้า")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "จำนวน")]
        [Range(0, 999999999, ErrorMessage = "ค่าต้องอยู่ระหว่าง 0 ถึง 999999999")]
        public decimal Qty { get; set; }

        [Display(Name = "หน่วย")]
        public int? UnitTypeID { get; set; }
        public virtual UnitType UnitType { get; set; }

        [Display(Name = "ราคาขายต่อหน่วย")]
        [Range(0.00, 999999999, ErrorMessage = "ค่าต้องอยู่ระหว่าง 0.00 ถึง 999999999")]
        public decimal? PricePerUnit { get; set; }

        [Display(Name = "ส่วนลด")]
        [Range(0.00, 100, ErrorMessage = "ต้องเป็นค่า % ระหว่าง 0 ถึง 100")]
        public decimal? Discount { get; set; }

        [Display(Name = "เปอร์เซ็นต์กำไร")]
        [Range(0.00, 100, ErrorMessage = "ต้องเป็นค่า % ระหว่าง 0 ถึง 100")]
        public decimal? ExpectedProfit { get; set; }

        [Display(Name = "จำนวนเงิน")]
        [Range(0.00, 999999999, ErrorMessage = "ค่าต้องอยู่ระหว่าง 0.00 ถึง 999999999")]
        public decimal Amount { get; set; }

        public bool? IsDeleted { get; set; }
        /*
        #region Calculated fields
        [NotMapped]
        public decimal Total
        {
            get
            {
                return Qty * PricePerUnit;
            }
        }

        [NotMapped]
        public decimal VATAmount
        {
            get
            {
                return TotalPlusVAT - Total;
            }
        }

        [NotMapped]
        public decimal TotalPlusVAT
        {
            get
            {
                return Total * (1 + VAT / 100);
            }
        }        
        #endregion
        */
    }
}