using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Repositories
{
    public class FeedbackTemplate
    {
        public int TemplateId { get; set; }
        public string Comment { get; set; }
        public CommentType CommentType { get; set; }
        public TransactionSide ReciverType { get; set; }
        public string UserEmail { get; set; }
    }
}
