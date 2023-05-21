using System.ComponentModel;

namespace Razor.ViewModels
{
    public class ProfileViewModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
