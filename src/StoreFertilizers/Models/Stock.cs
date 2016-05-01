using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFertilizers.Models
{
    public class Stock
    {
        public int StockID { get; set; }

        [Display(Name = "สินค้า")]
        [Required(ErrorMessage = "กรุณาระบุสินค้า")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "สถานที่จัดเก็บ")]
        public string Location { get; set; }

        [Display(Name = "จำนวนเงิน")]
        [Range(0.00, 999999999, ErrorMessage = "ค่าต้องอยู่ระหว่าง 0.00 ถึง 999999999")]
        public decimal Amount { get; set; }

        [Display(Name = "รูปสินค้า")]
        public Byte[] ProductImage { get; set; }

        [Display(Name = "ปรับปรุงล่าสุด")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "หมายเหตุ")]
        public string Notes { get; set; }
    }
}