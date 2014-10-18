using Allegro.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Models
{
    public class SellRating
    {
        public int TotalRatings { get; set; }
        public List<SellRatingAverageStruct> AverageAreaRating { get; set; }
        public List<SellRatingDetailStruct> DissatisfactionDetails { get; set; }
        public List<SellRatingAveragePerMonthStruct> AverageAreaRatingPerMonth { get; set; }
    }
}
