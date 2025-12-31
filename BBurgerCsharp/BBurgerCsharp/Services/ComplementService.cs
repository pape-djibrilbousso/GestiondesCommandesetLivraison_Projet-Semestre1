using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class ComplementService : IComplementService
    {
        private readonly ApplicationDbContext _context;

        public ComplementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Complement>> GetAllAsync(bool includeArchived = false)
        {
            var query = _context.Complements.AsQueryable();

            if (!includeArchived)
            {
                query = query.Where(c => !c.EstArchive);
            }

            return await query
                .OrderBy(c => c.Type)
                .ThenBy(c => c.Nom)
                .ToListAsync();
        }

        public async Task<Complement?> GetByIdAsync(int id)
        {
            return await _context.Complements.FindAsync(id);
        }

        public async Task<List<Complement>> GetByTypeAsync(string type)
        {
            return await _context.Complements
                .Where(c => c.Type == type && !c.EstArchive)
                .OrderBy(c => c.Nom)
                .ToListAsync();
        }

        public async Task<List<Complement>> GetActiveAsync()
        {
            return await GetAllAsync(false);
        }

        public async Task<List<Complement>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Complements
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}