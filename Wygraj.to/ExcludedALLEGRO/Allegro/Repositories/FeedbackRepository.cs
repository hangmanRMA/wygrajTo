using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Allegro.pl.allegro.webapi;
using Allegro.Utility;
using Allegro.Models;
using Allegro.Models.Abstract;
using Allegro.Repositories.Abstract;

namespace Allegro.Repositories
{
    public enum CommentType
    {
        Positive,
        Negative,
        Neutral
    }
    public enum TransactionSide
    {
        Buyer,
        Seller
    }
    public enum FeedbackType
    {
        Recived,
        Given,
        All
    }



    public class FeedbackRepository : SessionDependentRepository
    {
        public FeedbackRepository(string sessionHandle)
            : base(sessionHandle) { }




        public List<WaitFeedbackStruct> GetWaitingFeedback(int Offset, int PackageSize)
        {
            const int maxPackageSize = 200, startIndex = 0;

            int innerOffset = Offset;

            int totalFeedback = Service.doGetWaitingFeedbacksCount(SessionHandle);
            List<WaitFeedbackStruct> list = new List<WaitFeedbackStruct>();

            if (PackageSize > maxPackageSize)
            {
                PackageSize = maxPackageSize;
            }

            if (Offset > totalFeedback || Offset < startIndex)
            {
                throw new ArgumentException("Invalid Offset value.");
            }

            if (PackageSize < 0)
            {
                throw new ArgumentException("Invalid PackageSize value.");
            }

            var tab = Service.doGetWaitingFeedbacks(SessionHandle, Offset, PackageSize);
            list = tab.ToList<WaitFeedbackStruct>();

            return list;
        }

        public int GetNumberOfFeedbacks(FeedbackType feedbackType,CommentType commentType, long UserId)
        {
            const int defaultIntValue = 0, firstFeedbackIndex=0;

            string type = Converter.GetCommentType(commentType);
            int total=-1;

            switch (feedbackType)
            {
                case FeedbackType.Recived:
                    Service.doGetFeedback(SessionHandle, defaultIntValue, (int)UserId, firstFeedbackIndex, type, out total);
                    break;
                case FeedbackType.Given:
                    Service.doGetFeedback(SessionHandle, (int)UserId, defaultIntValue, firstFeedbackIndex, type, out total);
                    break;
                case FeedbackType.All:
                    int temp;

                    Service.doGetFeedback(SessionHandle, (int)UserId, defaultIntValue, firstFeedbackIndex, type, out total);
                    Service.doGetFeedback(SessionHandle, defaultIntValue, (int)UserId, firstFeedbackIndex, type, out temp);

                    total += temp;
                    break;
                default:
                    break;
            }
            return total;
        }

        public List<FeedbackList> GetFeedback(FeedbackType feedbackType, long UserId, CommentType commentType, int offset)
        {
            const int defaultIntValue = 0;

            List<FeedbackList> list = new List<FeedbackList>();
            FeedbackList[] array = null;
            string type = Converter.GetCommentType(commentType);
            int totalDowloaded = 0;
            switch (feedbackType)
            {
                case FeedbackType.Recived:
                    array = Service.doGetFeedback(SessionHandle, defaultIntValue, (int)UserId, offset, type, out totalDowloaded);
                    break;
                case FeedbackType.Given:
                    array = Service.doGetFeedback(SessionHandle, (int)UserId, defaultIntValue, offset, type, out totalDowloaded);
                    break;
                case FeedbackType.All:
                    array = Service.doGetFeedback(SessionHandle, defaultIntValue, (int)UserId, offset, type, out totalDowloaded);
                    array = array.Concat(Service.doGetFeedback(SessionHandle, (int)UserId, defaultIntValue, offset, type, out totalDowloaded)).ToArray();
                    break;
                default:
                    break;
            }
            if (array != null)
            {
                list = array.ToList<FeedbackList>();
            }
            return list;
        }

        public List<FeedbackList> GetAllFeedback(FeedbackType feedbackType, long UserId, CommentType commentType) 
        {
            const int allegroDefaultPackageSize = 25;

            int total = GetNumberOfFeedbacks(feedbackType, commentType, UserId);
            int lastIndex = 0;
            List<FeedbackList> outputList = new List<FeedbackList>();

            for (int offset = 0; offset < total; offset += allegroDefaultPackageSize)
            {
                var temp = GetFeedback(feedbackType, UserId, commentType, offset);
                outputList= outputList.Concat(temp).ToList();
                lastIndex = offset;
            }

            return outputList;
        }

        
        
        
        
        
        public int PostFeedback(PostFeedback feedback)
        {
            const int templateNotUsed = 0, toBuyer = 1, toSeller = 2;
            string commentType = Converter.GetCommentType(feedback.CommentType);
            int feedbackId = -1;

            if (feedback is BuyerFeedback)
            {
                feedbackId = Service.doFeedback(
                    SessionHandle,
                    feedback.AuctionId,
                    templateNotUsed,
                    feedback.ToUserId,
                    feedback.Comment,
                    commentType,
                    toBuyer,
                    null);

            }
            else if (feedback is ShopFeedback)
            {
                var rate = (feedback as ShopFeedback).Rate.ToArray();
                feedbackId = Service.doFeedback(
                    SessionHandle,
                    feedback.AuctionId,
                    templateNotUsed,
                    feedback.ToUserId,
                    feedback.Comment,
                    commentType,
                    toSeller,
                    rate);
            }

            return feedbackId;
        }
    }
}
