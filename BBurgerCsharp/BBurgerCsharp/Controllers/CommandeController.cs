using Microsoft.AspNetCore.Mvc;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ICommandeService _commandeService;
        private readonly IPaiementService _paiementService;
        private readonly IZoneService _zoneService;
        private readonly IPanierService _panierService;

        public CommandeController(
            ICommandeService commandeService,
            IPaiementService paiementService,
            IZoneService zoneService,
            IPanierService panierService)
        {
            _commandeService = commandeService;
            _paiementService = paiementService;
            _zoneService = zoneService;
            _panierService = panierService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                TempData["Error"] = "Vous devez vous connecter pour commander";
                return RedirectToAction("Login", "Account");
            }

            var items = _panierService.GetItems();
            if (items.Count == 0)
            {
                TempData["Error"] = "Votre panier est vide";
                return RedirectToAction("Index", "Catalogue");
            }

            var viewModel = new CommandeViewModel
            {
                ZonesDisponibles = await _zoneService.GetAllAsync(),
                MontantTotal = _panierService.GetTotal()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeViewModel model)
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            if (model.TypeLivraison == "livraison" && !model.ZoneId.HasValue)
            {
                ModelState.AddModelError("ZoneId", "Veuillez sélectionner une zone de livraison");
            }

            if (!ModelState.IsValid)
            {
                model.ZonesDisponibles = await _zoneService.GetAllAsync();
                model.MontantTotal = _panierService.GetTotal();
                return View(model);
            }

            try
            {
                var items = _panierService.GetItems();

                var commande = await _commandeService.CreateAsync(
                    clientId.Value,
                    model,
                    items
                );

                await _paiementService.CreateAsync(
                    commande.Id,
                    commande.MontantTotal,
                    model.MoyenPaiement
                );

                _panierService.Vider();

                TempData["Success"] = "Commande passée avec succès ! Numéro : " + commande.Id;

                return RedirectToAction("Details", new { id = commande.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
                model.ZonesDisponibles = await _zoneService.GetAllAsync();
                model.MontantTotal = _panierService.GetTotal();
                return View(model);
            }
        }

        public async Task<IActionResult> Index()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var commandes = await _commandeService.GetByClientIdAsync(clientId.Value);

            return View(commandes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var commande = await _commandeService.GetByIdWithDetailsAsync(id);

            if (commande == null || commande.ClientId != clientId.Value)
            {
                TempData["Error"] = "Commande introuvable";
                return RedirectToAction("Index");
            }

            return View(commande);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Annuler(int id)
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _commandeService.AnnulerAsync(id, clientId.Value);

            if (result)
            {
                TempData["Success"] = "Commande annulée";
            }
            else
            {
                TempData["Error"] = "Impossible d'annuler cette commande";
            }

            return RedirectToAction("Details", new { id });
        }
    }
}