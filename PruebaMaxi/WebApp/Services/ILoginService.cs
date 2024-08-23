using WebApp.Dtos;

namespace WebApp.Services
{
    public interface ILoginService
    {
        Task<bool> ValidateUser(CredentialDto employee);
    }
}
