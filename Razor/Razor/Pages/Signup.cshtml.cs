using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor.Pages
{
    public class SignupModel : PageModel
    {
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

            //call api appplication

            return RedirectToPage("./Index");
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
