using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Repositories.Abstract
{
    public abstract class SessionDependentRepository : AllegroRepository
    {
        private string sessionHandle;

        public SessionDependentRepository(string handle)
            : base()
        {
            sessionHandle = handle;
        }
        protected string SessionHandle { get { return sessionHandle; } }
        public void RefreshSessionHandle(string handle)
        {
            sessionHandle = handle;

        }
    }
}