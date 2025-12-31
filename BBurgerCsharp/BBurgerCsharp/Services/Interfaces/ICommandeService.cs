using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface ICommandeService
    {
        Task<Commande> CreateAsync(int clientId, CommandeViewModel model, List<PanierItemViewModel> items);
        Task<List<Commande>> GetByClientIdAsync(int clientId);
        Task<Commande?> GetByIdAsync(int id);
        Task<Commande?> GetByIdWithDetailsAsync(int id);
        Task<bool> AnnulerAsync(int commandeId, int clientId);
    }
}