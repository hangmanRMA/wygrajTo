using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Models
{
    public class AuctionInfo
    {
        public long AuctionId { get; set; }
        public string Name { get; set; }
        public float ActualPrice { get; set; }
        public string SellerName { get; set; }
        public string HighBiderName { get; set; }
        public DateTime EndTime { get; set; }

    }
}
