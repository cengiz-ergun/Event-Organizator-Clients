using Razor.Services.Abstracts;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace Razor.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static int _pagination;
        private static int _currentPage;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "email").Value;
            }
            else
            {
                return null;
            }
        }

        public string GetFirstName()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "firstname").Value;
            }
            else
            {
                return null;
            }
        }

        public string GetId()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "id").Value;
            }
            else
            {
                return null;
            }
        }

        public string GetLastName()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "lastname").Value;
            }
            else
            {
                return null;
            }
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

        public string GetToken()
        {
            //HttpContext.Request.Cookies["token"];
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] != null)
            {
                return _httpContextAccessor.HttpContext.Request.Cookies["token"];
            }
            else
            {
                return null;
            }
        }

        public void SetPagination(int pagination)
        {
            _pagination = pagination;
        }
        public int GetPagination()
        {
            return _pagination;
        }

        public void SetCurrentPage(int currentPage)
        {
            _currentPage = currentPage;
        }

        public int GetCurrentPage()
        {
            return _currentPage;
        }
    }
}
