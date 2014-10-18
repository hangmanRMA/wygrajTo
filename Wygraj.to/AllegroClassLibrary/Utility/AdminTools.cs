using AllegroClassLibrary.Models;
//using AllegroClassLibrary.pl.allegro.webapi;
using AllegroClassLibrary.pl.webapisandbox.pl.allegro.webapi;
using AllegroClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroClassLibrary.Utility
{
    public class AdminTools
    {
        //AllegroWebApiService service; //allegro
        pl.webapisandbox.pl.allegro.webapi.serviceService service;  //sandbox
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
            int countryId = Converter.GetCountryId(country);

            var list = GetSystemStatus(country);
            
            var statusInfo = list.Find(x => x.countryId == countryId);

            return statusInfo.verKey;
        }

        private List<SysStatusType> GetSystemStatus(Countries country)
        {
            int id = Converter.GetCountryId(country);
            var sysStatusList = service.doQueryAllSysStatus(id, webApiKey);

            return sysStatusList.ToList<SysStatusType>();
        }

    }
}
