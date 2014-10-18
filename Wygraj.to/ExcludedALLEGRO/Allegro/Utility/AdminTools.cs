using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro.pl.allegro.webapi;
using Allegro.Repositories;

namespace Allegro.Utility
{
    public class AdminTools
    {
        AllegroWebApiService service;
        string webApiKey;

        public AdminTools()
        {
            service = ConnectionPool.Instance.Service;
            webApiKey = ConnectionPool.Instance.WebAPIKey;
        }

        public List<CountryInfoType> GetCountryIds()
        {
            const int defaultCountryCode = 1;//to get full country list
            var countryList = service.doGetCountries(defaultCountryCode, webApiKey);

            return countryList.ToList<CountryInfoType>();
        }

        public long GetLocalVersion(Countries country)
        {
            int countryId=Converter.GetCountryId(country);
            var list=GetSystemStatus(country);
            var statusInfo= list.Find(x => x.countryid == countryId);

            return statusInfo.verkey;
        }

        private List<SysStatusType> GetSystemStatus(Countries country)
        {
            int id = Converter.GetCountryId(country);
            var sysStatusList = service.doQueryAllSysStatus(id, webApiKey);

            return sysStatusList.ToList<SysStatusType>();
        }

    }
}
