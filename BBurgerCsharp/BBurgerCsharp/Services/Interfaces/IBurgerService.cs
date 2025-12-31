using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IBurgerService
    {
        Task<List<Burger>> GetAllAsync(bool includeArchived = false);
        Task<Burger?> GetByIdAsync(int id);
        Task<List<Burger>> GetActiveAsync();
    }
}