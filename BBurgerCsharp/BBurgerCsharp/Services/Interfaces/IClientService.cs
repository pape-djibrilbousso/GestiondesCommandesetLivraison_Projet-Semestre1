using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByEmailAsync(string email);
        Task<Client> RegisterAsync(RegisterViewModel model);
        Task<Client?> LoginAsync(LoginViewModel model);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> TelephoneExistsAsync(string telephone);
    }    
}