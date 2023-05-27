using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Razor.DTOs.RequestModels;
using Razor.DTOs.ResponseModels;
using Razor.Services;
using Razor.Services.Abstracts;
using Razor.ViewModels;

namespace Razor.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly CrudService _crudService;
        private readonly ICurrentUserService _currentUserService;

        public IndexModel(IConfiguration configuration,
                          ICurrentUserService currentUserService, 
                          CrudService crudService)
        {
            _configuration = configuration;
            _crudService = crudService;
            _currentUserService = currentUserService;
        }

        [BindProperty]
        public OkResponseModel<EventResponseModel> okResponseModel { get; set; }

        [BindProperty]
        public Query query { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = "Bearer " + _currentUserService.GetToken();

            HttpResponseMessage httpResponseMessage = await _crudService.GetEvents(token);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            httpResponseMessage.EnsureSuccessStatusCode();
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            okResponseModel = JsonConvert.DeserializeObject<OkResponseModel<EventResponseModel>>(content);

            var count = okResponseModel.Count;

            int itemQuantityPerPagination = 5;
            int remainder = count % itemQuantityPerPagination;
            int result = count / itemQuantityPerPagination;
            int pagination = remainder != 0 ? result + 1 : result;

            //this.HttpContext.Session.SetInt32("Pagination", pagination);
            _currentUserService.SetPagination(pagination);
            _currentUserService.SetCurrentPage(1);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int submit)
        {
            var token = "Bearer " + _currentUserService.GetToken();

            HttpResponseMessage httpResponseMessage = await _crudService.GetEventsWithQuery(token, new Query { Page = submit - 1, Size = 5});

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            _currentUserService.SetCurrentPage(submit);
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            okResponseModel = JsonConvert.DeserializeObject<OkResponseModel<EventResponseModel>>(content);

            return Page();
        }
    }
}
