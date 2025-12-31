using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IComplementService
    {
        Task<List<Complement>> GetAllAsync(bool includeArchived = false);
        Task<Complement?> GetByIdAsync(int id);
        Task<List<Complement>> GetByTypeAsync(string type);
        Task<List<Complement>> GetActiveAsync();
        Task<List<Complement>> GetByIdsAsync(List<int> ids);
    }
}