using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class BurgerService : IBurgerService
    {
        private readonly ApplicationDbContext _context;

        public BurgerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Burger>> GetAllAsync(bool includeArchived = false)
        {
            var query = _context.Burgers.AsQueryable();

            if (!includeArchived)
            {
                query = query.Where(b => !b.EstArchive);
            }

            return await query.OrderBy(b => b.Nom).ToListAsync();
        }

        public async Task<Burger?> GetByIdAsync(int id)
        {
            return await _context.Burgers.FindAsync(id);
        }

        public async Task<List<Burger>> GetActiveAsync()
        {
            return await GetAllAsync(false);
        }
    }
}