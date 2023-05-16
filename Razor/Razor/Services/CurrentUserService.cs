using Razor.Services.Abstracts;
using System.Data;

namespace Razor.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            throw new NotImplementedException();
        }

        public string GetFirstName()
        {
            throw new NotImplementedException();
        }

        public string GetLastName()
        {
            throw new NotImplementedException();
        }

        public string GetRole()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            }
            else
            {
                return null;
            }
        }
    }
}
