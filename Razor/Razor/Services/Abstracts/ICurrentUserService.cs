namespace Razor.Services.Abstracts
{
    public interface ICurrentUserService
    {
        string GetFirstName();
        string GetLastName();
        string GetEmail();
        string GetRole();
        string GetToken();
        string GetId();
        void SetPagination(int pagination);
        int GetPagination();
        void SetCurrentPage(int currentPage);
        int GetCurrentPage();
    }
}
