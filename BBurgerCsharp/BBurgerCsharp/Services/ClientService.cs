using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Data;
using BBurgerCsharp.Web.Models;
using BBurgerCsharp.Web.Models.ViewModels;
using BBurgerCsharp.Web.Services.Interfaces;

namespace BBurgerCsharp.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == email.Trim().ToLower());
        }

        public async Task<Client> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                if (await EmailExistsAsync(model.Email))
                {
                    throw new Exception("Cet email est déjà utilisé");
                }

                if (await TelephoneExistsAsync(model.Telephone))
                {
                    throw new Exception("Ce numéro de téléphone est déjà utilisé");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var client = new Client
                {
                    Nom = model.Nom.Trim(),
                    Prenom = model.Prenom.Trim(),
                    Telephone = model.Telephone.Trim(),
                    Email = model.Email.Trim().ToLower(),
                    Password = hashedPassword,
                    DateInscription = DateTime.UtcNow
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return client;
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Erreur lors de l'inscription : {innerMessage}", ex);
            }
        }

        public async Task<Client?> LoginAsync(LoginViewModel model)
        {
            try
            {
                var client = await GetByEmailAsync(model.Email);

                if (client == null)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(client.Password) || 
                    (!client.Password.StartsWith("$2a$") && 
                     !client.Password.StartsWith("$2b$") && 
                     !client.Password.StartsWith("$2y$")))
                {
                    throw new Exception("Le mot de passe en base de données est invalide. Contactez l'administrateur.");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, client.Password);

                return isPasswordValid ? client : null;
            }
            catch (BCrypt.Net.SaltParseException)
            {
                throw new Exception("Erreur de format du mot de passe. Veuillez contacter l'administrateur.");
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Clients.AnyAsync(c => c.Email == email.Trim().ToLower());
        }

        public async Task<bool> TelephoneExistsAsync(string telephone)
        {
            return await _context.Clients.AnyAsync(c => c.Telephone == telephone.Trim());
        }
    }
}