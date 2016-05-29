using System;
using System.ComponentModel.DataAnnotations;

namespace StoreFertilizers.Models
{
    public class Stock
    {
        public int StockID { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string Location { get; set; }
        public decimal Balance { get; set; }
        public decimal FullCapStock { get; set; }
        public decimal LowCapStock { get; set; }
        public bool AlertLowStock { get; set; }
        //[Display(Name = "รูปสินค้า")]
        //public Byte[] ProductImage { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Notes { get; set; }
    }
}