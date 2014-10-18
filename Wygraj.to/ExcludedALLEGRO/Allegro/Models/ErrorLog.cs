using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Models
{
    public class ErrorLog
    {
        public int LogId { get; set; }
        public string UserEmail { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
