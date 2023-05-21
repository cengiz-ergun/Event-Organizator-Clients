using Newtonsoft.Json;
using Razor.Clients;
using Razor.Pages;
using System.Net.Http.Headers;

namespace Razor.Services
{
    public class CrudService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EventOrganizatorClient _eventOrganizatorClient;

        public CrudService(IHttpClientFactory httpClientFactory,
                           EventOrganizatorClient eventOrganizatorClient)
        {
            _httpClientFactory = httpClientFactory;
            _eventOrganizatorClient = eventOrganizatorClient;
        }
        public async Task<HttpResponseMessage> SignUpUser(SignupViewModel signupViewModel)
        {
            var serializedUser = JsonConvert.SerializeObject(signupViewModel);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/signup");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedUser);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _eventOrganizatorClient._httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> LoginUser(LoginViewModel loginViewModel)
        {
            var serializedUser = JsonConvert.SerializeObject(loginViewModel);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/login");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedUser);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _eventOrganizatorClient._httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> GetCities(string accessToken)
        {
            _eventOrganizatorClient._httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Get, "api/city");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _eventOrganizatorClient._httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> GetCategories(string accessToken)
        {
            _eventOrganizatorClient._httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Get, "api/category");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _eventOrganizatorClient._httpClient.SendAsync(request);

            return response;
        }
    }
}
