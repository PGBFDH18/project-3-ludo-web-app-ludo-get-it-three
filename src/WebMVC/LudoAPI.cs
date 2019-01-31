using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApp
{
    public class LudoAPI
    {
        // example code
        private const string BaseUrl = "https://ludogame.azurewebsites.net/api/ludo/";

        public LudoAPI()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("{id}", Method.POST);
            request.AddUrlSegment("id", "1323");
        }
    }
}