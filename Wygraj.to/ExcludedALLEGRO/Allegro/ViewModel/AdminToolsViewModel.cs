using Allegro.pl.allegro.webapi;
using Allegro.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.ViewModel
{
    public class AdminToolsViewModel
    {
        public ObservableCollection<CountryInfoType> Countries { get; set; }

        void TestMethod()
        {
            AdminTools tools = new AdminTools();
            var list = tools.GetCountryIds();
            Countries = new ObservableCollection<CountryInfoType>(list);
        }

    }
}
