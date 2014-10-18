using Allegro.pl.allegro.webapi;
using Allegro.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Repositories.Abstract
{
    public abstract class AllegroRepository
    {
        protected string WebApiKey { get; set; }
        protected AllegroWebApiService Service { get; set; }

        public AllegroRepository()
        {
            WebApiKey = ConnectionPool.Instance.WebAPIKey;
            Service = ConnectionPool.Instance.Service;
        }
    }
}
