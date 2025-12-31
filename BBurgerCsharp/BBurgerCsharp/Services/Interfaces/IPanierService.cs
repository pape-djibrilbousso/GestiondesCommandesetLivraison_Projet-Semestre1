using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;

namespace BBurgerCsharp.Web.Services.Interfaces
{
    public interface IPanierService
    {
        void AjouterBurger(int burgerId, string nom, decimal prix, string? image, List<int> complementIds);
        void AjouterMenu(int menuId, string nom, decimal prix, string? image);
        void SupprimerItem(int index);
        void ModifierQuantite(int index, int quantite);
        void Vider();
        List<PanierItemViewModel> GetItems();
        decimal GetTotal();
        int GetCount();
    }
}