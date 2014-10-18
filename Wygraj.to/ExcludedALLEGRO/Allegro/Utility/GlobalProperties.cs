using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.pl.allegro.webapi;

namespace Allegro.Utility
{
    public static class GlobalProperties
    {
        public static AllegroWebApiService Service = new AllegroWebApiService();
        public static string WebAPIKey { get { return "7dda4ce4"; } set { } }
        public static string DBConnectionString { get{return "Data Source=localhost;Initial Catalog=AllegroDB;Integrated Security=True";} set; }
    }
}
