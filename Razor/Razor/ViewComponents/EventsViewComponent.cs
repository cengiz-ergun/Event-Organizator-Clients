using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Razor.DTOs.ResponseModels;
using Razor.Services;
using Razor.Services.Abstracts;
using System.Net.Http;

namespace Razor.ViewComponents
{
    public class EventsViewComponent: ViewComponent
    {
        private readonly CrudService _crudService;
        private readonly ICurrentUserService _currentUserService;

        public EventsViewComponent(CrudService crudService,
                                   ICurrentUserService currentUserService)
        {
            _crudService = crudService;
            _currentUserService = currentUserService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = "Bearer " + _currentUserService.GetToken();
            HttpResponseMessage httpResponseMessage = await _crudService.GetEvents(token);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            OkResponseModel<EventResponseModel> okResponseModel = JsonConvert.DeserializeObject<OkResponseModel<EventResponseModel>>(content);
            List<EventResponseModel> eventsList = okResponseModel.Data;
            return View(eventsList);
        }
    }
}
