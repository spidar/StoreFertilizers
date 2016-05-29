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
            get { return (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }
    }
}
