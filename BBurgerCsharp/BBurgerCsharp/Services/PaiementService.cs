using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class PaiementService : IPaiementService
    {
        private readonly ApplicationDbContext _context;

        public PaiementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Paiement> CreateAsync(int commandeId, decimal montant, string moyenPaiement)
        {
            var paiementExistant = await GetByCommandeIdAsync(commandeId);
            if (paiementExistant != null)
            {
                throw new Exception("Cette commande a déjà été payée");
            }

            var paiement = new Paiement
            {
                CommandeId = commandeId,
                Montant = montant,
                MoyenPaiement = moyenPaiement,
                DatePaiement = DateTime.UtcNow
            };

            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();

            var commande = await _context.Commandes.FindAsync(commandeId);
            if (commande != null)
            {
                commande.Etat = "validee";
                await _context.SaveChangesAsync();
            }

            return paiement;
        }

        public async Task<Paiement?> GetByCommandeIdAsync(int commandeId)
        {
            return await _context.Paiements
                .FirstOrDefaultAsync(p => p.CommandeId == commandeId);
        }

        public async Task<bool> CommandeEstPayeeAsync(int commandeId)
        {
            return await _context.Paiements
                .AnyAsync(p => p.CommandeId == commandeId);
        }
    }
}