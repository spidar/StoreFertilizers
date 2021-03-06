﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreFertilizers.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetails>();
        }

        public int InvoiceID { get; set; }

        public bool IsTax { get; set; }

        public bool? IsTicket { get; set; }

        [Display(Name = "ใบกำกับสินค้าเลขที่")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "วันที่")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "วันที่ครบกำหนดชำระ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "เงื่อนไขการชำระเงิน")]
        public string TermOfPayment { get; set; }

        [Display(Name = "วันที่ส่งสินค้า")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "เลขที่อ้างอิงใบสั่งซื้อ")]
        public string ReferencePONumber { get; set; }

        [Display(Name = "พนักงานขาย")]
        //public int? EmployeeID { get; set; }
        //public virtual Employee Employee { get; set; }
        public string EmployeeName { get; set; }

        [Display(Name = "ลูกค้า")]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Display(Name = "ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        [Display(Name = "สถานที่ส่งของ")]
        public string ShipTo { get; set; }

        [Display(Name = "ขนส่งโดย")]
        public string ShipBy { get; set; }

        [Display(Name = "เลขที่ใบขนส่ง")]
        public string DeliveryRefNumber { get; set; }

        [Display(Name = "เลขทะเบียนรถขนส่ง")]
        public string DeliveryByCarNumber { get; set; }

        [Display(Name = "ชำระเงินแล้ว")]
        public bool Paid { get; set; }

        [Display(Name = "ชำระโดย")]
        //public int? PaymentTypeID { get; set; }
        //public virtual PaymentType PaymentType { get; set; }
        public string PaymentType { get; set; }

        [Display(Name = "ธนาคาร")]
        //public int? BankID { get; set; }
        //public virtual Bank Bank { get; set; }
        public string Bank { get; set; }

        [Display(Name = "ธนาคารสาขา")]
        public string BankBranch { get; set; }

        [Display(Name = "เลขที่เช็ค")]
        public string ChequeNumber { get; set; }

        [Display(Name = "ลงวันที่")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }

        [Display(Name = "จำนวนเงิน")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "ผู้รับเงิน")]
        public string PaidCollector { get; set; }

        [Display(Name = "ผู้รับเงินรับเงินวันที่")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? PaidCollectedDate { get; set; }

        [Display(Name = "ผู้ส่งสินค้า")]
        public string DeliveryByPerson { get; set; }

        [Display(Name = "ผู้รับสินค้า")]
        public string ReceivedByPerson { get; set; }

        [Display(Name = "วันที่รับสินค้า")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ReceivedProductDate { get; set; }

        [Display(Name = "หมายเหตุ")]
        public string Notes { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

        [Display(Name = "รวมเงิน")]
        public decimal? SubTotal { get; set; }

        [Display(Name = "ส่วนลด")]
        public decimal? Discount { get; set; }

        [Display(Name = "ภาษีมูลค่าเพิ่ม")]
        //[Range(0.00, 100.0, ErrorMessage = "ค่าจะต้องเป็น % และอยู่ระหว่าง 0 ถึง 100")]
        public decimal VAT { get; set; }

        [Display(Name = "จำนวนเงินสุทธิ")]
        [DisplayFormat(DataFormatString = "{0:#,#.00}", ApplyFormatInEditMode = true)]
        public decimal NetTotal { get; set; }
    }
}