using System;
using System.Collections;

namespace StoreFertilizers.Models.Paging
{
    public class PagedList
    {
        public IEnumerable Content { get; set; }
        public IEnumerable SumNetTotalEachDay { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal TotalUnPaidAmount { get; set; }
        public int TotalProducts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages
        {
            get { return (PageSize == 0) ? 0 : (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }
    }
}
