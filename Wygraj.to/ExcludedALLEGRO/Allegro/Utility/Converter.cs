using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.Repositories;
using System.Security.Cryptography;
using Allegro.pl.allegro.webapi;
using Allegro.Models;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Allegro.Utility
{
    public static class Converter
    {
        /*Actual parameter list can be downloaded by Utility.AdminTools
         * updated manualy, TODO:: create tool for it
         */
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

        public static string GetCommentType(CommentType type)
        {
            switch (type)
            {
                case CommentType.Positive:
                    return "POS";
                case CommentType.Negative:
                    return "NEG";
                case CommentType.Neutral:
                    return "NEU";
                default:
                    throw new ArgumentException("Invalid type argument.");
            }
        }

        public static CommentType GetCommentType(string type)
        {
            switch (type)
            {
                case "NEG":
                    return CommentType.Negative;
                case "NEU":
                    return CommentType.Neutral;
                case "POS":
                    return CommentType.Positive;
                default:
                    throw new ArgumentException("Invalid type argument.");
            }
        }

        public static TransactionSide GetTransactionSide(string side)
        {
            switch (side)
            {
                case "BUYER":
                    return TransactionSide.Buyer;
                case "SELLER":
                    return TransactionSide.Seller;
                default:
                    throw new ArgumentException("Invalid side argument.");
            }
        }

        public static UserFeedback GetUserFeedback(FeedbackList allegroClass)
        {
            const string CancelledFeedbackValue = "";
            UserFeedback uFeedback = new UserFeedback();

            uFeedback.AuctionId = allegroClass.fitemid;
            uFeedback.FeedbackId = allegroClass.fid;
            uFeedback.Date = Converter.GetDateTime(allegroClass.fdate);

            uFeedback.FromUserId = allegroClass.ffromid;
            uFeedback.FromUserLogin = ConnectionPool.Instance.Service.doGetUserLogin(GetCountryId(Countries.Poland), (int)uFeedback.FromUserId, ConnectionPool.Instance.WebAPIKey);
            uFeedback.ToUserId = allegroClass.ftoid;
            uFeedback.ToUserLogin = ConnectionPool.Instance.Service.doGetUserLogin(GetCountryId(Countries.Poland), (int)uFeedback.ToUserId, ConnectionPool.Instance.WebAPIKey);
            uFeedback.ToUserSide = Converter.GetTransactionSide(allegroClass.freceivertype);

            uFeedback.Comment = allegroClass.fdesc;

            if (allegroClass.ftype == CancelledFeedbackValue)
            {
                uFeedback.Cancelled = true;
                uFeedback.CommentType = null;
            }
            else
            {
                uFeedback.CommentType = Converter.GetCommentType(allegroClass.ftype);
            }

            //uFeedback.IsReply

            return uFeedback;
        }

        public static string SerializeToString<TData>(TData settings)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, settings);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static TData DeserializeFromString<TData>(string settings)
        {
            byte[] b = Convert.FromBase64String(settings);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (TData)formatter.Deserialize(stream);
            }
        }
    }
}
