using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace BrunoCampiol.Service.Service
{
    public class IPGeolocationService : IIPGeolocationService
    {
        public IRestClient RestClient;
        private readonly string _baseUrl;
        private readonly string _resource;

        //http://ip-api.com/json/200.32.1.23.1

        public IPGeolocationService(IOptions<IPServiceAPIProvider> settingsProvider)
        {
            _baseUrl = settingsProvider.Value.Host;
            _resource = settingsProvider.Value.Resource;
            RestClient = new RestClient(_baseUrl);
        }

        public VISITORS GetVisitorInformation(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(ipAddress));

            RestRequest request = new RestRequest(_resource + ipAddress);
            IRestResponse restResponse = RestClient.Execute(request);

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
                    // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
                    // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
                    // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}
                    JObject visitorJsonObject = JObject.Parse(restResponse.Content);
                    VISITORS visitor = new VISITORS();
                    visitor.CITY = (string)visitorJsonObject["city"];
                    visitor.COUNTRY = (string)visitorJsonObject["countryCode"];
                    visitor.IP = (string)visitorJsonObject["query"];
                    visitor.ISP = (string)visitorJsonObject["isp"];
                    visitor.REGION = (string)visitorJsonObject["regionName"];
                    visitor.CREATED_ON_UTC = DateTime.UtcNow;
                    return visitor;
                default:
                    return null;
            }
        }
    }
}
