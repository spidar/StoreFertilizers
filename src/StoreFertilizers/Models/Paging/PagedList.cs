using System;
using System.Collections;

namespace StoreFertilizers.Models.Paging
{
    public class PagedList
    {
        public IEnumerable Content { get; set; }
        public decimal TotalNetAmount { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages
        {
            get { return (PageSize == 0) ? 0 : (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }

        public IEnumerable ContentDetails { get; set; }
        public int TotalProductDetails { get; set; }
        public decimal TotalNetAmountDetails { get; set; }
        public Int32 CurrentPageDetails { get; set; }
        public Int32 PageSizeDetails { get; set; }
        public int TotalRecordsDetails { get; set; }

        public int TotalPagesDetails
        {
            get { return (PageSizeDetails == 0) ? 0 : (int)Math.Ceiling((decimal)TotalRecordsDetails / PageSizeDetails); }
        }
    }
}
