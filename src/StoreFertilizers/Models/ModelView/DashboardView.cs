using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreFertilizers.Models.ModelView
{
    public class DashboardView
    {
        public IEnumerable Notifications { get; set; }
        public IEnumerable SumNetTotalEachDay { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal TotalNetPaidAmount { get; set; }
        public decimal TotalNetUnPaidAmount { get; set; }
        public decimal TotalNetUnPaidAmountInSystem { get; set; }
        public int TotalProducts { get; set; }
    }
}
