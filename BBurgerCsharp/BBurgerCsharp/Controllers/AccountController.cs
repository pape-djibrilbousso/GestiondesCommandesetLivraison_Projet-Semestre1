using Microsoft.AspNetCore.Mvc;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientService _clientService;

        public AccountController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var client = await _clientService.RegisterAsync(model);

                HttpContext.Session.SetInt32("ClientId", client.Id);
                HttpContext.Session.SetString("ClientNom", client.NomComplet);

                TempData["Success"] = "Inscription réussie ! Bienvenue " + client.Prenom;

                return RedirectToAction("Index", "Catalogue");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var client = await _clientService.LoginAsync(model);

                if (client == null)
                {
                    ModelState.AddModelError("", "Email ou mot de passe incorrect");
                    return View(model);
                }

                HttpContext.Session.SetInt32("ClientId", client.Id);
                HttpContext.Session.SetString("ClientNom", client.NomComplet);

                TempData["Success"] = "Bienvenue " + client.Prenom + " !";

                return RedirectToAction("Index", "Catalogue");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Vous êtes déconnecté";
            return RedirectToAction("Index", "Home");
        }
    }
}