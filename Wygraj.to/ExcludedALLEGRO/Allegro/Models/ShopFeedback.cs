using Allegro.Models.Abstract;
using Allegro.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Allegro.Models
{
    //class represent feedback to post to the seller
    public class ShopFeedback : PostFeedback
    {
        public List<SellRatingEstimationStruct> Rate { get; set; }

        public ShopFeedback(List<SellRatingEstimationStruct> rate)
        {
            Rate = rate;
        }

        //public List<SellRatingEstimationStruct> DeserializeRate()
        //{

        //}
    }
}
