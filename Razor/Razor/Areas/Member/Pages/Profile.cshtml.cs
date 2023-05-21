using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Pages;
using Razor.Services.Abstracts;
using Razor.ViewModels;

namespace Razor.Areas.Member.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ICurrentUserService _currentUserService;

        public ProfileModel(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }


        [BindProperty]
        public ProfileViewModel profileViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            profileViewModel = new ProfileViewModel()
            {
                FirstName = _currentUserService.GetFirstName(), 
                LastName = _currentUserService.GetLastName(),
                Email = _currentUserService.GetEmail(),
            };

            return Page();
        }
    }
}
