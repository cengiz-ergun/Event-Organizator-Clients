using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Services;
using Razor.Services.Abstracts;

namespace Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            string role = null;
            if (_currentUserService.GetRole() != null)
            {
                role = _currentUserService.GetRole();
            }
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
                return Page();
            }
        }
    }
}