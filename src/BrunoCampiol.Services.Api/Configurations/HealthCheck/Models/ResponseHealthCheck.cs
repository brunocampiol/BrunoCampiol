namespace BrunoCampiol.Services.Api.Configurations.HealthCheck.Models
{
    public class ResponseHealthCheck
    {
        public string Status { get; set; }

        public string TotalDuration { get; set; }

        public Dictionary<string, ResponseResults> Results { get; set; }
    }
}
