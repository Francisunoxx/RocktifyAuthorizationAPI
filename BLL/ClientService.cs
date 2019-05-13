using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BLL
{
    public static class ClientService
    {
        public static Client FindClientId(string clientId)
        {
            var w = ConfigurationManager.AppSettings["RocktifyClientId"];
            var e = ConfigurationManager.AppSettings["RocktifyClientSecret"];

            return new Client { ClientId = w, ClientSecret = e };
        }
    }
}
