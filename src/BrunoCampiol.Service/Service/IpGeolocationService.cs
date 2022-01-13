using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;

namespace BrunoCampiol.Service.Service
{
    public class IPGeolocationService : IIPGeolocationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        // http://ip-api.com/json/200.32.1.23.1
        // https://ip-api.com/docs/api:json

        public IPGeolocationService(IOptions<IPServiceAPIProvider> settingsProvider, IHttpClientFactory httpClientFactory)
        {
            // Guard clauses
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            if (settingsProvider == null) throw new ArgumentNullException(nameof(settingsProvider));
            if (settingsProvider.Value == null) throw new ArgumentNullException(nameof(settingsProvider.Value));
            if (string.IsNullOrWhiteSpace(settingsProvider.Value.Host)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(settingsProvider.Value.Host));

            _baseUrl = settingsProvider.Value.Host;
            _httpClientFactory = httpClientFactory;
        }

        // TODO: change to async
        // TODO: change to IPaddress object instead
        public VISITORS GetVisitorInformation(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(ipAddress));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/json/{ipAddress}");
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponse = httpClient.SendAsync(httpRequest).Result;

            // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
            // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
            // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
            // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}

            if (httpResponse.StatusCode != HttpStatusCode.OK) return null;

            JObject jobject = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

            // TODO: check for invalid responses here
            // TODO: change to automapper or similar

            VISITORS visitor = new VISITORS();
            visitor.CITY = jobject.GetValue("city", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.COUNTRY = jobject.GetValue("country", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.IP = jobject.GetValue("ip", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.ISP = jobject.GetValue("isp", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.REGION = jobject.GetValue("region", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.CREATED_ON_UTC = DateTime.UtcNow;

            return visitor;
        }
    }
}