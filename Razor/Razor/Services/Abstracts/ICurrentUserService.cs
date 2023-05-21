namespace Razor.Services.Abstracts
{
    public interface ICurrentUserService
    {
        string GetFirstName();
        string GetLastName();
        string GetEmail();
        string GetRole();
        string GetToken();
    }
}
