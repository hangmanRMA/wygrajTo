using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AllegroClassLibrary.Utility;
using AllegroClassLibrary.Repositories.Abstract;
using AllegroClassLibrary.Models;
using AllegroClassLibrary.pl.webapisandbox.pl.allegro.webapi;

namespace AllegroClassLibrary.Repositories
{
    public class ServiceRepository : AllegroRepository
    {
        AdminTools tools;

        public ServiceRepository()
            : base()
        {
            tools = new AdminTools();
        }

        public LoginDetails LoginEnc(string UserLogin, string HashPassword, Countries Country)
        {
            int countryId = Converter.GetCountryId(Country);
            long localVersion = tools.GetLocalVersion(Country);
            LoginDetails details = new LoginDetails();

            long serverTime;
            long userId;

            details.SessionHandle = Service.doLoginEnc(
                UserLogin,
                HashPassword,
                countryId,
                WebApiKey,
                localVersion,
                out userId,
                out serverTime
                );

            details.UserId = userId;
            details.LoginTime = Converter.GetDateTime(serverTime);
            details.Country = Country;
            details.PasswordHash = HashPassword;
            details.UserLogin = UserLogin;

            return details;

        }

        /// <summary>
        /// 14/10/2014 NIE ZADZIALA DLA PHARMACY!!!!
        /// Zwraca komunikat allegro
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="details"></param>
        public string MakeBid(Bid bid, LoginDetails details)
        {
            const long bidQuantity = 1, buyOut = 0;
            const bool isBuyOutPassed = false;
            const string vartiantDefaultValue = "";

            var str = Service.doBidItem(
                details.SessionHandle,
                bid.AuctionId,
                bid.Price,

                bidQuantity, buyOut, isBuyOutPassed, null, vartiantDefaultValue);

            return str;
        }

        /// <summary>
        /// 14/10/2014
        /// </summary>
        /// <param name="details"></param>
        /// <param name="AuctionId"></param>
        /// <returns></returns>
        public AuctionInfo GetAuctionInfo(LoginDetails details, long AuctionId)
        {
            long[] ids = { AuctionId };
            long[] notFound;
            long[] adminKilled;

            const bool getDescription = false,
                getImages = false,
                getAttribs = false,
                getPostageOptions = false,
                getCompanyInfo = false,
                getProductInfo = false;

            const int defaultDescriptionValue = 0,
                defaultImageValue = 0,
                defaulAttribValue = 0,
                defaultPostageOptionsValue = 0,
                defaultCompanyInfoValue = 0,
                defaultProductInfoValue = 0;

            ItemInfoStruct[] allegroInfo = Service.doGetItemsInfo(
                details.SessionHandle,
                ids,
                defaultDescriptionValue, getDescription,
                defaultImageValue, getImages,
                defaulAttribValue, getAttribs,
                defaultPostageOptionsValue, getPostageOptions,
                defaultCompanyInfoValue, getCompanyInfo,
                defaultProductInfoValue, getProductInfo,
                out notFound, out adminKilled);

            AuctionInfo info = new AuctionInfo();
            var itemInfo = allegroInfo[0].itemInfo;

            info.AuctionId = itemInfo.itId;
            info.Name = itemInfo.itName;
            info.ActualPrice = itemInfo.itPrice;
            info.EndTime = Converter.GetDateTime(itemInfo.itEndingTime);
            info.SellerName = itemInfo.itSellerLogin;
            info.HighBiderName = itemInfo.itHighBidderLogin;

            return info;
        }
    }
}

































