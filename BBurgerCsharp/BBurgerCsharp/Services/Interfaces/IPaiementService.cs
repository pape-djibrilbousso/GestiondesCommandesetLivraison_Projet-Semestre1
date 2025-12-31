using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IPaiementService
    {
        Task<Paiement> CreateAsync(int commandeId, decimal montant, string moyenPaiement);
        Task<Paiement?> GetByCommandeIdAsync(int commandeId);
        Task<bool> CommandeEstPayeeAsync(int commandeId);
    }
}