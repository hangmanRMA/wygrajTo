using Allegro.Models;
using Allegro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.Utility
{
    public sealed class Logger
    {
        private static readonly Logger m_oInstance = new Logger();
        private int m_nCounter = 0;

        static Logger()
        {

        }

        public static Logger Instance
        {
            get
            {
                return m_oInstance;
            }
        }






        private Logger()
        {
            m_nCounter = 1;
            repo = new LoggerRepository();
        }


        private LoggerRepository repo;

        public void PostLog(ErrorLog log)
        {
            repo.InsertErrorLog(log);
        }



    }
}
