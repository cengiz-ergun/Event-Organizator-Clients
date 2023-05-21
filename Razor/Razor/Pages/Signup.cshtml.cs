using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Razor.DTOs.ResponseModels;
using Razor.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor.Pages
{
    public class SignupModel : PageModel
    {
        private readonly CrudService _crudService;

        public SignupModel(CrudService crudService)
        {
            _crudService = crudService;
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SignupViewModel signupViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpResponseMessage httpResponseMessage = await _crudService.SignUpUser(signupViewModel);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                string tempDataMessage = null;
                switch (httpResponseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.UnprocessableEntity:
                        ErrorResponseModel errorResponseModel = JsonConvert.DeserializeObject<ErrorResponseModel>(content);
                        string message = string.Join("\n", errorResponseModel.Errors);
                        tempDataMessage = message;
                        break;
                    default:
                        break;
                }

                if (tempDataMessage == null)
                {
                    throw new NotImplementedException();
                }
                TempData["Message"] = tempDataMessage;
                return Page();
            }

            httpResponseMessage.EnsureSuccessStatusCode();
            
            // redirect to login page - loading icon

            return RedirectToPage("./Login");
        }
    }

    public class SignupViewModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
