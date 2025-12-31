using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class ZoneService : IZoneService
    {
        private readonly ApplicationDbContext _context;

        public ZoneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Zone>> GetAllAsync()
        {
            return await _context.Zones
                .OrderBy(z => z.PrixLivraison)
                .ToListAsync();
        }

        public async Task<Zone?> GetByIdAsync(int id)
        {
            return await _context.Zones.FindAsync(id);
        }
    }
}