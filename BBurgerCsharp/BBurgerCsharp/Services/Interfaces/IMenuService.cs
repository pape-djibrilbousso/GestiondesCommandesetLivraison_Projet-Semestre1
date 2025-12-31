using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
     public interface IMenuService
    {
        Task<List<Menu>> GetAllAsync(bool includeArchived = false);
        Task<Menu?> GetByIdAsync(int id);
        Task<List<Menu>> GetActiveAsync();
        Task<decimal> CalculatePrixAsync(int menuId);
        Task<MenuAvecPrix> GetMenuAvecPrixAsync(int menuId);
        Task<List<MenuAvecPrix>> GetAllMenusAvecPrixAsync();
    }
}