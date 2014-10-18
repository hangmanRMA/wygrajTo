using AutoBid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBid.Repositories
{
    public class WygrajToDbRepository
    {
        public List<Bid> GetBids(DateTime fromDate)
        {
            return new List<Bid>
            {
                new Bid{ BidId=1,  AuctionId=1, BidTime=new DateTime(2000,12,12), UserId=1},
                new Bid{ BidId=2,  AuctionId=2, BidTime=new DateTime(2000,12,12), UserId=2},
                new Bid{ BidId=3,  AuctionId=3, BidTime=new DateTime(2000,12,12), UserId=3},
                new Bid{ BidId=4,  AuctionId=4, BidTime=new DateTime(2000,12,12), UserId=4}
            };
        }

    }
}
