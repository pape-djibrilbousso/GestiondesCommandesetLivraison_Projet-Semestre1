using System.Text.Json;
using Microsoft.AspNetCore.Http;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class PanierService : IPanierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IComplementService _complementService;
        private const string PanierSessionKey = "Panier";

        public PanierService(
            IHttpContextAccessor httpContextAccessor,
            IComplementService complementService)
        {
            _httpContextAccessor = httpContextAccessor;
            _complementService = complementService;
        }

        private ISession? Session => _httpContextAccessor.HttpContext?.Session;

        public void AjouterBurger(int burgerId, string nom, decimal prix, string? image, List<int> complementIds)
        {
            var items = GetItems();
            var complements = new List<Complement>();

            if (complementIds != null && complementIds.Count > 0)
            {
                foreach (var complementId in complementIds)
                {
                    var complement = _complementService.GetByIdAsync(complementId).Result;
                    if (complement != null)
                    {
                        complements.Add(complement);
                    }
                }
            }

            var item = new PanierItemViewModel
            {
                Id = burgerId,
                Type = "burger",
                Nom = nom,
                Prix = prix,
                Quantite = 1,
                Image = image,
                Complements = complements
            };

            items.Add(item);
            SaveItems(items);
        }

        public void AjouterMenu(int menuId, string nom, decimal prix, string? image)
        {
            var items = GetItems();

            var item = new PanierItemViewModel
            {
                Id = menuId,
                Type = "menu",
                Nom = nom,
                Prix = prix,
                Quantite = 1,
                Image = image
            };

            items.Add(item);
            SaveItems(items);
        }

        public void SupprimerItem(int index)
        {
            var items = GetItems();
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                SaveItems(items);
            }
        }

        public void ModifierQuantite(int index, int quantite)
        {
            var items = GetItems();
            if (index >= 0 && index < items.Count && quantite > 0)
            {
                items[index].Quantite = quantite;
                SaveItems(items);
            }
        }

        public void Vider()
        {
            Session?.Remove(PanierSessionKey);
        }

        public List<PanierItemViewModel> GetItems()
        {
            var sessionData = Session?.GetString(PanierSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<PanierItemViewModel>();
            }

            return JsonSerializer.Deserialize<List<PanierItemViewModel>>(sessionData) 
                   ?? new List<PanierItemViewModel>();
        }

        public decimal GetTotal()
        {
            var items = GetItems();
            return items.Sum(i => i.SousTotal);
        }

        public int GetCount()
        {
            return GetItems().Count;
        }

        private void SaveItems(List<PanierItemViewModel> items)
        {
            var json = JsonSerializer.Serialize(items);
            Session?.SetString(PanierSessionKey, json);
        }
    }
}