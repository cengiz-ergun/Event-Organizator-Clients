using Newtonsoft.Json;
using Razor.Pages;
using System.Net.Http.Headers;

namespace Razor.Services
{
    public class CrudService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CrudService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> SignUpUser(SignupViewModel signupViewModel)
        {
            //HttpClient _httpClient = new HttpClient();
            HttpClient _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7085");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);

            var serializedUser = JsonConvert.SerializeObject(signupViewModel);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/signup");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedUser);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> LoginUser(LoginViewModel loginViewModel)
        {
            //HttpClient _httpClient = new HttpClient();
            HttpClient _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7085");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);

            var serializedUser = JsonConvert.SerializeObject(loginViewModel);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/login");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedUser);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        //public async Task<HttpResponseMessage> GetCategories(string accessToken)
        //{
        //    HttpClient _httpClient = _httpClientFactory.CreateClient();
        //    _httpClient.BaseAddress = new Uri("https://localhost:7092");
        //    _httpClient.Timeout = new TimeSpan(0, 0, 30);
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

        //    var request = new HttpRequestMessage(HttpMethod.Get, "api/category");
        //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await _httpClient.SendAsync(request);

        //    return response;
        //}

        //public async Task<HttpResponseMessage> GetCities(string accessToken)
        //{
        //    HttpClient _httpClient = _httpClientFactory.CreateClient();
        //    _httpClient.BaseAddress = new Uri("https://localhost:7092");
        //    _httpClient.Timeout = new TimeSpan(0, 0, 30);
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

        //    var request = new HttpRequestMessage(HttpMethod.Get, "api/city");
        //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await _httpClient.SendAsync(request);

        //    return response;
        //}
    }
}
