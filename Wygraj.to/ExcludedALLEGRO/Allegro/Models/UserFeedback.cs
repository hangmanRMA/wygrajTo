using Allegro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Models
{
    public class UserFeedback
    {
        public long AuctionId { get; set; }
        public long FeedbackId { get; set; }
        public DateTime Date { get; set; }

        public long FromUserId { get; set; }
        public string FromUserLogin { get; set; }
        public long ToUserId { get; set; }
        public string ToUserLogin { get; set; }
        public TransactionSide ToUserSide { get; set; }

        public string Comment { get; set; }
        public CommentType? CommentType { get; set; }//null if cancelled

        public bool Cancelled { get; set; }
        //public bool IsReply { get; set; } TODO
    }
}
