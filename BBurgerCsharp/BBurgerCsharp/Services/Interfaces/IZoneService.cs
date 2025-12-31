using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IZoneService
    {
        Task<List<Zone>> GetAllAsync();
        Task<Zone?> GetByIdAsync(int id);
    }
}