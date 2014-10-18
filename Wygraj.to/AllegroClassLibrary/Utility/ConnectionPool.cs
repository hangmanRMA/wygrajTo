using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AllegroClassLibrary.pl.allegro.webapi;

namespace AllegroClassLibrary.Utility
{
    public sealed class ConnectionPool
    {

        private static readonly ConnectionPool m_oInstance = new ConnectionPool();
        private int m_nCounter = 0;

        static ConnectionPool() { }

        public static ConnectionPool Instance { get { return m_oInstance; } }

        private ConnectionPool()
        {
            m_nCounter = 1;
            connString = "Data Source=localhost;Initial Catalog=AllegroDB;Integrated Security=True";
            conn = new SqlConnection(connString);
        }








        private string connString;
        private SqlConnection conn;

        ///public string WebAPIKey { get { return "7dda4ce4"; } }       //KEY SERWISU
        public string WebAPIKey { get { return "sf833fa2"; } }      //KEY SANDBOXA

        //public AllegroWebApiService Service = new AllegroWebApiService();     API SERWISU
        public pl.webapisandbox.pl.allegro.webapi.serviceService Service = new pl.webapisandbox.pl.allegro.webapi.serviceService();         //API SANDBOXA

        public SqlConnection Connection { get { return conn; } }

    }
}
