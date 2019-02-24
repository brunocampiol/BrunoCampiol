using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCampiol.Service.Service
{
    // https://codereview.stackexchange.com/questions/85321/unit-testing-http-requests
    public class WebsiteService
    {
        public IWebClient WebClient { get; private set; }

        public WebsiteService (IWebClient webClient)
        {
            if (webClient == null) throw new ArgumentNullException(nameof(webClient));

            WebClient = webClient;
        }

        public VISITORS GetVisitorObjectFromJsonString (string jsonString)
        {
            if (String.IsNullOrEmpty(jsonString)) throw new ArgumentException("Cannot be null or empty", nameof(jsonString));

            // {\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",
            // \"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"qu
            // ery\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"
            // timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}
            JObject visitorJsonObject = JObject.Parse(jsonString);

            VISITORS visitor = new VISITORS();
            visitor.CITY = (string)visitorJsonObject["city"];
            visitor.COUNTRY = (string)visitorJsonObject["countryCode"];
            visitor.IP = (string)visitorJsonObject["query"];
            visitor.ISP = (string)visitorJsonObject["isp"];
            visitor.REGION = (string)visitorJsonObject["regionName"];
            visitor.CREATED_ON_UTC = DateTime.UtcNow;

            return visitor;
        }
    }
}
