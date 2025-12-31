using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;

        public CommandeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Commande> CreateAsync(int clientId, CommandeViewModel model, List<PanierItemViewModel> items)
        {
            
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new Exception("Client non trouvé");
            }

            decimal montantProduits = items.Sum(i => i.SousTotal);
            decimal fraisLivraison = 0;

            if (model.TypeLivraison == "livraison" && model.ZoneId.HasValue)
            {
                var zone = await _context.Zones.FindAsync(model.ZoneId.Value);
                if (zone == null)
                {
                    throw new Exception("Zone de livraison non trouvée");
                }
                fraisLivraison = zone.PrixLivraison;
            }

            decimal montantTotal = montantProduits + fraisLivraison;

            var commande = new Commande
            {
                ClientId = clientId,
                DateCommande = DateTime.UtcNow,
                TypeLivraison = model.TypeLivraison,
                Etat = "en_cours",
                MontantTotal = montantTotal,
                ZoneId = model.ZoneId
            };

            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync(); 

            foreach (var item in items)
            {
                var ligne = new LigneCommande
                {
                    CommandeId = commande.Id,
                    TypeProduit = item.Type,
                    ProduitId = item.Id,
                    Quantite = item.Quantite,
                    PrixUnitaire = item.Prix
                };

                _context.LigneCommandes.Add(ligne);

                if (item.Type == "burger" && item.Complements != null && item.Complements.Count > 0)
                {
                    foreach (var complement in item.Complements)
                    {
                        var commandeComplement = new CommandeComplement
                        {
                            CommandeId = commande.Id,
                            ComplementId = complement.Id,
                            Quantite = item.Quantite
                        };

                        _context.CommandeComplements.Add(commandeComplement);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return commande;
        }

        public async Task<List<Commande>> GetByClientIdAsync(int clientId)
        {
            return await _context.Commandes
                .Include(c => c.Zone)
                .Include(c => c.Paiement)
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.DateCommande)
                .ToListAsync();
        }

        public async Task<Commande?> GetByIdAsync(int id)
        {
            return await _context.Commandes.FindAsync(id);
        }

        public async Task<Commande?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Zone)
                .Include(c => c.LigneCommandes)
                .Include(c => c.CommandeComplements)
                    .ThenInclude(cc => cc.Complement)
                .Include(c => c.Paiement)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AnnulerAsync(int commandeId, int clientId)
        {
            var commande = await _context.Commandes
                .FirstOrDefaultAsync(c => c.Id == commandeId && c.ClientId == clientId);

            if (commande == null)
            {
                return false;
            }

            if (commande.Etat != "en_cours")
            {
                return false;
            }

            commande.Etat = "annulee";
            await _context.SaveChangesAsync();

            return true;
        }
    }
}