using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Models
{
    [ImplementPropertyChanged]
    public class Bid
    {
        public long AuctionId { get; set; }
        public float Price { get; set; }

        public override bool Equals(object obj)
        {
            var bid = obj as Bid;
            if (bid != null)
            {
                return AuctionId == bid.AuctionId && Price == bid.Price;
            }

            return false;
        }
    }
}