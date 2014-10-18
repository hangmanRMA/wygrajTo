using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBid.Models
{
    [ImplementPropertyChanged]
    public class Bid : AllegroClassLibrary.Models.Bid
    {
        public long BidId { get; set; }
        public long UserId { get; set; }
        public DateTime BidTime { get; set; }
        public bool Posted { get; set; }

        public Bid()
        {
            Posted = false;
        }

        public override bool Equals(object obj)
        {
            var bid = obj as Bid;
            if (bid != null)
            {
                return base.Equals(obj) && BidId == bid.BidId && UserId == bid.UserId && BidTime == bid.BidTime && Posted == bid.Posted;
            }
            else
            {
                return false;
            }
        }
    }
}