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
        public IEnumerable StockNotifications { get; set; }

        public string[] PurchaseVsSaleChartLabels { get; set; }
        public string[] PurchaseVsSaleChartSeries { get; set; }
        public IEnumerable PurchaseVsSaleChartData { get; set; }

        public List<string> StockPieChartLabels { get; set; }
        public List<decimal> StockPieChartData { get; set; }
    }

    public class ChartData
    {
    }
}
