using AllegroClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Utility
{
    public static class Converter
    {
        public static int GetCountryId(Countries country)
        {
            const int PolandId = 1, TestId = 228;
            int id;

            switch (country)
            {
                case Countries.Poland:
                    id = PolandId;
                    break;
                case Countries.Test:
                    id = TestId;
                    break;
                default:
                    id = -1;
                    break;
            }

            return id;
        }

        public static DateTime GetDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dtDateTime;
        }

        public static string GetHashCode(string unencodedPassword)
        {
            SHA256 sha = new SHA256Managed();
            byte[] byteArrayPassword = Encoding.ASCII.GetBytes(unencodedPassword);
            byte[] passwordHash = sha.ComputeHash(byteArrayPassword);
            string encodedPassword = Convert.ToBase64String(passwordHash);

            return encodedPassword;
        }
    }
}
