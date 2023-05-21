using Microsoft.Extensions.Configuration;

namespace Razor.Clients
{
    public class EventOrganizatorClient
    {
        private readonly IConfiguration _configuration;

        public HttpClient _httpClient { get; set; }
        public EventOrganizatorClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["Server:Uri"]);
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
        }
    }
}
