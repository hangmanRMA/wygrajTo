using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.pl.allegro.webapi;
using System.Data.SqlClient;
using Allegro.Repositories;

namespace Allegro.Utility
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

        public string WebAPIKey { get { return "7dda4ce4"; } }
        public AllegroWebApiService Service = new AllegroWebApiService();
        public SqlConnection Connection { get { return conn; } }

    }
}
