using Allegro.Models;
using Allegro.pl.allegro.webapi;
using Allegro.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.Repositories.Abstract;

namespace Allegro.Repositories
{
    public class SellRatingRepository:AllegroRepository
    {

        public SellRatingRepository():base()
        {
        }

        public SellRating GetSellRating(string sessionHandle)
        {
            SellRating rating = new SellRating();

            SellRatingAverageStruct[] avgRating;
            SellRatingDetailStruct[] details;
            SellRatingAveragePerMonthStruct[] monthDetails;

            rating.TotalRatings = Service.doGetMySellRating(sessionHandle, out avgRating, out details, out monthDetails);

            rating.AverageAreaRating = avgRating.ToList<SellRatingAverageStruct>();
            rating.AverageAreaRatingPerMonth = monthDetails.ToList<SellRatingAveragePerMonthStruct>();
            rating.DissatisfactionDetails = details.ToList<SellRatingDetailStruct>();

            return rating;
        }

        //used to fill SellRatingEstimationStruct(shop's auction rate)
        public List<SellRatingInfoStruct> GetSellRatingDissatisfactionReasons()
        {
            int countryId = Converter.GetCountryId(Countries.Poland);
            var reasons = Service.doGetSellRatingReasons(WebApiKey, countryId);
            return reasons.ToList<SellRatingInfoStruct>();
        }

    }
}
