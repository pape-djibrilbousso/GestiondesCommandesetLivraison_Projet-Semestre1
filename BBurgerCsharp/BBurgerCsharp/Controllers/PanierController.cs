using Microsoft.AspNetCore.Mvc;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Controllers
{
    public class PanierController : Controller
    {
        private readonly IPanierService _panierService;
        private readonly IBurgerService _burgerService;
        private readonly IMenuService _menuService;

        public PanierController(
            IPanierService panierService,
            IBurgerService burgerService,
            IMenuService menuService)
        {
            _panierService = panierService;
            _burgerService = burgerService;
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            var items = _panierService.GetItems();
            ViewBag.Total = _panierService.GetTotal();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AjouterBurger(int id, List<int>? complementIds)
        {
            var burger = await _burgerService.GetByIdAsync(id);

            if (burger == null || burger.EstArchive)
            {
                TempData["Error"] = "Ce burger n'est pas disponible";
                return RedirectToAction("DetailsBurger", "Catalogue", new { id });
            }

            _panierService.AjouterBurger(
                burger.Id,
                burger.Nom,
                burger.Prix,
                burger.Image,
                complementIds ?? new List<int>()
            );

            TempData["Success"] = $"{burger.Nom} ajouté au panier !";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AjouterMenu(int id)
        {
            var menuAvecPrix = await _menuService.GetMenuAvecPrixAsync(id);

            if (menuAvecPrix.Menu.EstArchive)
            {
                TempData["Error"] = "Ce menu n'est pas disponible";
                return RedirectToAction("DetailsMenu", "Catalogue", new { id });
            }

            _panierService.AjouterMenu(
                menuAvecPrix.Menu.Id,
                menuAvecPrix.Menu.Nom,
                menuAvecPrix.PrixTotal,
                menuAvecPrix.Menu.Image
            );

            TempData["Success"] = $"{menuAvecPrix.Menu.Nom} ajouté au panier !";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Supprimer(int index)
        {
            _panierService.SupprimerItem(index);
            TempData["Success"] = "Article supprimé du panier";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vider()
        {
            _panierService.Vider();
            TempData["Success"] = "Panier vidé";

            return RedirectToAction("Index");
        }

        public IActionResult Count()
        {
            return Json(new { count = _panierService.GetCount() });
        }
    }
}