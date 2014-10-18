using AllegroClassLibrary.pl.allegro.webapi;
using AllegroClassLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Repositories.Abstract
{
    public abstract class AllegroRepository
    {
        protected string WebApiKey { get; set; }
        //protected AllegroWebApiService Service { get; set; }  //allegro
        protected pl.webapisandbox.pl.allegro.webapi.serviceService Service { get; set; }   //sandbox

        public AllegroRepository()
        {
            WebApiKey = ConnectionPool.Instance.WebAPIKey;
            Service = ConnectionPool.Instance.Service;
        }
    }
}