using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Razor.DTOs;
using Razor.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Razor.DTOs.ResponseModels;
using System;

namespace Razor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly CrudService _crudService;
        public LoginModel(CrudService crudService)
        {
            _crudService = crudService;
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginViewModel loginViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpResponseMessage httpResponseMessage = await _crudService.LoginUser(loginViewModel);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                string tempDataMessage = null;
                switch (httpResponseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.UnprocessableEntity:
                    case System.Net.HttpStatusCode.NotFound:
                        ErrorResponseModel errorResponseModel = JsonConvert.DeserializeObject<ErrorResponseModel>(content);
                        string message = string.Join("\n", errorResponseModel.Errors);
                        tempDataMessage = message;
                        break;
                }

                if (tempDataMessage == null )
                {
                    throw new NotImplementedException();
                }
                TempData["Message"] = tempDataMessage;
                return Page();
            }

            httpResponseMessage.EnsureSuccessStatusCode();
            //var content = await httpResponseMessage.Content.ReadAsStringAsync();
            OkResponseModel<Token> okResponseModel = JsonConvert.DeserializeObject<OkResponseModel<Token>>(content);
            Token token = okResponseModel.Data.FirstOrDefault();

            HttpContext.Response.Cookies.Append("token", token.AccessToken, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1)
            });
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.AccessToken);
            string role = jwt.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            string firstname = jwt.Claims.First(c => c.Type == "firstname").Value;
            string lastname = jwt.Claims.First(c => c.Type == "lastname").Value;
            string email = jwt.Claims.First(c => c.Type == "email").Value;
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, role),
                        new Claim("firstname", firstname),
                        new Claim("lastname", lastname),
                        new Claim("email", email),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());

            if (role == "Member")
            {
                return RedirectToPage("/Index", new { area = "Member" });
            }
            else if (role == "Administrator")
            {
                return RedirectToPage("/Index", new { area = "Administration" });
            }
            else
            {
                return RedirectToPage("./Index");
            }


            //return RedirectToPage("./Index");
        }
    }

    public class LoginViewModel
    {

        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

