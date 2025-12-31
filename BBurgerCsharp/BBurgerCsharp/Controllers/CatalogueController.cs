using Microsoft.AspNetCore.Mvc;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly IMenuService _menuService;
        private readonly IComplementService _complementService;

        public CatalogueController(
            IBurgerService burgerService,
            IMenuService menuService,
            IComplementService complementService)
        {
            _burgerService = burgerService;
            _menuService = menuService;
            _complementService = complementService;
        }

        public async Task<IActionResult> Index(string? filtre)
        {
            var viewModel = new CatalogueViewModel
            {
                FiltreActif = filtre
            };

            if (string.IsNullOrEmpty(filtre) || filtre.ToLower() == "burger")
            {
                viewModel.Burgers = await _burgerService.GetActiveAsync();
            }

            if (string.IsNullOrEmpty(filtre) || filtre.ToLower() == "menu")
            {
                viewModel.Menus = await _menuService.GetAllMenusAvecPrixAsync();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsBurger(int id)
        {
            var burger = await _burgerService.GetByIdAsync(id);

            if (burger == null || burger.EstArchive)
            {
                TempData["Error"] = "Ce burger n'est pas disponible";
                return RedirectToAction("Index");
            }

            var viewModel = new DetailsBurgerViewModel
            {
                Burger = burger,
                Boissons = await _complementService.GetByTypeAsync("boisson"),
                Frites = await _complementService.GetByTypeAsync("frites")
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsMenu(int id)
        {
            try
            {
                var menuAvecPrix = await _menuService.GetMenuAvecPrixAsync(id);

                if (menuAvecPrix.Menu.EstArchive)
                {
                    TempData["Error"] = "Ce menu n'est pas disponible";
                    return RedirectToAction("Index");
                }

                var viewModel = new DetailsMenuViewModel
                {
                    Menu = menuAvecPrix.Menu,
                    PrixTotal = menuAvecPrix.PrixTotal,
                    Burgers = menuAvecPrix.Burgers,
                    Complements = menuAvecPrix.Complements
                };

                return View(viewModel);
            }
            catch
            {
                TempData["Error"] = "Menu non trouvé";
                return RedirectToAction("Index");
            }
        }
    }
}