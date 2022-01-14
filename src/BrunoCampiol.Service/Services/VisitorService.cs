using BrunoCampiol.CrossCutting.Common.Models;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Infra.Data.Interfaces;
using BrunoCampiol.Infra.Data.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BrunoCampiol.Domain.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        // http://ip-api.com/json/200.32.1.23.1
        // https://ip-api.com/docs/api:json

        public VisitorService(IOptions<IPServiceAPIProvider> settingsProvider, 
                              IHttpClientFactory httpClientFactory,
                              IVisitorRepository visitorRepository)
        {
            // Guard clauses
            if (visitorRepository == null) throw new ArgumentNullException(nameof(visitorRepository));
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            if (settingsProvider == null) throw new ArgumentNullException(nameof(settingsProvider));
            if (settingsProvider.Value == null) throw new ArgumentNullException(nameof(settingsProvider.Value));
            if (string.IsNullOrWhiteSpace(settingsProvider.Value.Host)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(settingsProvider.Value.Host));

            _baseUrl = settingsProvider.Value.Host;
            _httpClientFactory = httpClientFactory;
            _visitorRepository = visitorRepository;
        }

        public ICollection<VISITORS> GetPagedVisitors(int page, int pageSize)
        {
            return _visitorRepository.GetPagedVisitors(page, pageSize);
        }

        public void HandleVisitor(VISITORS visitor)
        {
            // Checks if localhost / already exists
            if (visitor.IP == "localhost" || visitor.IP == "::1") return;
            if (_visitorRepository.Exists(visitor.IP)) return;

            visitor = PopulateVisitorInformation(visitor);

            _visitorRepository.Add(visitor);
        }

        public async Task HandleVisitorAsync(VISITORS visitor)
        {
            // Checks if localhost / already exists
            if (visitor.IP == "localhost" || visitor.IP == "::1") return;
            if (_visitorRepository.Exists(visitor.IP)) return;

            visitor = await PopulateVisitorInformationAsync(visitor);

            await _visitorRepository.AddAsync(visitor);
        }

        // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
        // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
        // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
        // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}

        private VISITORS PopulateVisitorInformation(VISITORS visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (string.IsNullOrWhiteSpace(visitor.IP)) throw new ArgumentException("Cannot be space, empty or null", nameof(visitor.IP));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/json/{visitor.IP}");
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponse = httpClient.SendAsync(httpRequest).Result;

            if (httpResponse.StatusCode != HttpStatusCode.OK) return visitor;

            JObject jobject = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

            visitor.CITY = jobject.GetValue("city", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.COUNTRY = jobject.GetValue("country", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            //visitor.IP = jobject.GetValue("ip", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.ISP = jobject.GetValue("isp", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.REGION = jobject.GetValue("region", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            //visitor.CREATED_ON_UTC = DateTime.UtcNow;

            return visitor;
        }

        private async Task<VISITORS> PopulateVisitorInformationAsync(VISITORS visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (string.IsNullOrWhiteSpace(visitor.IP)) throw new ArgumentException("Cannot be space, empty or null", nameof(visitor.IP));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/json/{visitor.IP}");
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponse = await httpClient.SendAsync(httpRequest);

            if (httpResponse.StatusCode != HttpStatusCode.OK) return visitor;

            JObject jobject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            visitor.CITY = jobject.GetValue("city", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.COUNTRY = jobject.GetValue("country", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            //visitor.IP = jobject.GetValue("ip", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.ISP = jobject.GetValue("isp", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            visitor.REGION = jobject.GetValue("region", StringComparison.OrdinalIgnoreCase)?.Value<string>();
            //visitor.CREATED_ON_UTC = DateTime.UtcNow;

            return visitor;
        }

        // TODO: change to IPaddress object instead
        //public VISITORS GetVisitorInformation(string ipAddress)
        //{
        //    if (string.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(ipAddress));

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/json/{ipAddress}");
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var httpResponse = httpClient.SendAsync(httpRequest).Result;

        //    // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
        //    // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
        //    // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
        //    // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}

        //    if (httpResponse.StatusCode != HttpStatusCode.OK) return null;

        //    JObject jobject = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

        //    // TODO: check for invalid responses here
        //    // TODO: change to automapper or similar

        //    VISITORS visitor = new VISITORS();
        //    visitor.CITY = jobject.GetValue("city", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.COUNTRY = jobject.GetValue("country", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.IP = jobject.GetValue("ip", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.ISP = jobject.GetValue("isp", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.REGION = jobject.GetValue("region", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.CREATED_ON_UTC = DateTime.UtcNow;

        //    return visitor;
        //}

        //public async Task<VISITORS> GetVisitorInformationAsync(string ipAddress)
        //{
        //    if (string.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Cannot be null, empty or white-space", nameof(ipAddress));

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/json/{ipAddress}");
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var httpResponse = await httpClient.SendAsync(httpRequest);

        //    // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
        //    // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
        //    // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
        //    // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}

        //    if (httpResponse.StatusCode != HttpStatusCode.OK) return null;

        //    JObject jobject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

        //    // TODO: check for invalid responses here
        //    // TODO: change to automapper or similar

        //    VISITORS visitor = new VISITORS();
        //    visitor.CITY = jobject.GetValue("city", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.COUNTRY = jobject.GetValue("country", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.IP = jobject.GetValue("ip", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.ISP = jobject.GetValue("isp", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.REGION = jobject.GetValue("region", StringComparison.OrdinalIgnoreCase)?.Value<string>();
        //    visitor.CREATED_ON_UTC = DateTime.UtcNow;

        //    return visitor;
        //}
    }
}