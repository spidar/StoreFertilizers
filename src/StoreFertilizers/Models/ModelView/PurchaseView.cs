using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreFertilizers.Models.ModelView
{
    public class PurchaseView : Purchase
    {
        public string ProductName { get; set; }

        public string ProviderName { get; set; }
    }
}
