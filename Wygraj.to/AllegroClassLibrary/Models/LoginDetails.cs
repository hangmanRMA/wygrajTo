using AllegroClassLibrary.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllegroClassLibrary.Models;

namespace AllegroClassLibrary.Models
{
    //Created ONLY after successfull login attempt
    public class LoginDetails
    {
        public string UserLogin { get; set; }
        public string PasswordHash { get; set; }
        public long UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public Countries Country { get; set; }
        public string SessionHandle { get; set; }
    }
}
