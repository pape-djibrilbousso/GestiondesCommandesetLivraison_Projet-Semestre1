using Microsoft.AspNetCore.Mvc;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly IMenuService _menuService;

        public HomeController(
            IBurgerService burgerService,
            IMenuService menuService)
        {
            _burgerService = burgerService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Burgers = await _burgerService.GetActiveAsync();
            ViewBag.Menus = await _menuService.GetAllMenusAvecPrixAsync();

            return View();
        }
    }
}