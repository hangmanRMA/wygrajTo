using Allegro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Models.Abstract
{
    public abstract class PostFeedback
    {
        public long AuctionId { get; set; }
        public int ToUserId { get; set; }
        public string Comment { get; set; }
        public CommentType CommentType { get; set; }
    }
}
