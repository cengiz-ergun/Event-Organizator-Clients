using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Razor.Pages
{
    public class LoginModel : PageModel
    {
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

            //call api appplication

            return RedirectToPage("./Index");
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

