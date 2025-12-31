using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetAllAsync(bool includeArchived = false)
        {
            var query = _context.Menus.AsQueryable();

            if (!includeArchived)
            {
                query = query.Where(m => !m.EstArchive);
            }

            return await query.OrderBy(m => m.Nom).ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.Composition)
                    .ThenInclude(mc => mc.Burger)
                .Include(m => m.Composition)
                    .ThenInclude(mc => mc.Complement)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Menu>> GetActiveAsync()
        {
            return await GetAllAsync(false);
        }

        public async Task<decimal> CalculatePrixAsync(int menuId)
        {
            var compositions = await _context.MenuCompositions
                .Include(mc => mc.Burger)
                .Include(mc => mc.Complement)
                .Where(mc => mc.MenuId == menuId)
                .ToListAsync();

            decimal total = 0;

            foreach (var comp in compositions)
            {
                if (comp.Burger != null)
                {
                    total += comp.Burger.Prix;
                }
                if (comp.Complement != null)
                {
                    total += comp.Complement.Prix;
                }
            }

            return total;
        }

        public async Task<MenuAvecPrix> GetMenuAvecPrixAsync(int menuId)
        {
            var menu = await GetByIdAsync(menuId);
            if (menu == null)
            {
                throw new Exception("Menu non trouvé");
            }

            var prix = await CalculatePrixAsync(menuId);

            var burgers = menu.Composition
                .Where(c => c.Burger != null)
                .Select(c => c.Burger!)
                .ToList();

            var complements = menu.Composition
                .Where(c => c.Complement != null)
                .Select(c => c.Complement!)
                .ToList();

            return new MenuAvecPrix
            {
                Menu = menu,
                PrixTotal = prix,
                Burgers = burgers,
                Complements = complements
            };
        }

        public async Task<List<MenuAvecPrix>> GetAllMenusAvecPrixAsync()
        {
            var menus = await GetActiveAsync();
            var menusAvecPrix = new List<MenuAvecPrix>();

            foreach (var menu in menus)
            {
                var menuAvecPrix = await GetMenuAvecPrixAsync(menu.Id);
                menusAvecPrix.Add(menuAvecPrix);
            }

            return menusAvecPrix;
        }
    }
}