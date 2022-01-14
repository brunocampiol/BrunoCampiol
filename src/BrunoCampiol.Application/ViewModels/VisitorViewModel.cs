namespace BrunoCampiol.Application.ViewModels
{
    public class VisitorViewModel
    { 
        public string Ip { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Isp { get; set; }
        public string ClientHeaders { get; set; }
        public string ClientUserAgent { get; set; }
        public string ClientBrowser { get; set; }
        public string ClientOS { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}