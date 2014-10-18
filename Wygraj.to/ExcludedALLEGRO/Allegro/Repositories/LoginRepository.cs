using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.pl.allegro.webapi;
using Allegro.Utility;
using Allegro.Models;
using System.Windows;
using Allegro.Repositories.Abstract;

namespace Allegro.Repositories
{
    public enum Countries
    {
        Poland,
        Test
    };

    public class LoginRepository:AllegroRepository
    {
        AdminTools tools;

        public LoginRepository():base()
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
    }
}

































