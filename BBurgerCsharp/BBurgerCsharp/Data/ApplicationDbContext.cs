using Microsoft.EntityFrameworkCore;
using BBurgerCsharp.Web.Models;

namespace BBurgerCsharp.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuComposition> MenuCompositions { get; set; }
        public DbSet<Complement> Complements { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<LigneCommande> LigneCommandes { get; set; }
        public DbSet<CommandeComplement> CommandeComplements { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Livreur> Livreurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(e => e.Nom).HasColumnName("nom").IsRequired();
                entity.Property(e => e.Prenom).HasColumnName("prenom").IsRequired();
                entity.Property(e => e.Telephone).HasColumnName("telephone").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.Password).HasColumnName("password").IsRequired();
                entity.Property(e => e.DateInscription).HasColumnName("date_inscription").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Telephone).IsUnique();
            });

            modelBuilder.Entity<Burger>(entity =>
            {
                entity.ToTable("burger");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<MenuComposition>(entity =>
            {
                entity.ToTable("menucomposition");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(e => e.Menu)
                    .WithMany(m => m.Composition)
                    .HasForeignKey(e => e.MenuId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Burger)
                    .WithMany()
                    .HasForeignKey(e => e.BurgerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Complement)
                    .WithMany()
                    .HasForeignKey(e => e.ComplementId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Complement>(entity =>
            {
                entity.ToTable("complement");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.ToTable("zone");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Livreur>(entity =>
            {
                entity.ToTable("livreur");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.ToTable("commande");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn();

                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.DateCommande)
                    .HasColumnName("date_commande")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TypeLivraison).HasColumnName("type_livraison").IsRequired();
                entity.Property(e => e.Etat).HasColumnName("etat").HasDefaultValue("en_cours");
                entity.Property(e => e.MontantTotal).HasColumnName("montant_total");
                entity.Property(e => e.ZoneId).HasColumnName("zone_id");
                entity.Property(e => e.LivreurId).HasColumnName("livreur_id");

                entity.HasOne(e => e.Client)
                    .WithMany(c => c.Commandes)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });



            modelBuilder.Entity<LigneCommande>(entity =>
            {
                entity.ToTable("lignecommande");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(e => e.Commande)
                    .WithMany(c => c.LigneCommandes)
                    .HasForeignKey(e => e.CommandeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CommandeComplement>(entity =>
            {
                entity.ToTable("commandecomplement");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(e => e.Commande)
                    .WithMany(c => c.CommandeComplements)
                    .HasForeignKey(e => e.CommandeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Complement)
                    .WithMany()
                    .HasForeignKey(e => e.ComplementId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.ToTable("paiement");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasIndex(e => e.CommandeId).IsUnique();

                entity.HasOne(e => e.Commande)
                    .WithOne(c => c.Paiement)
                    .HasForeignKey<Paiement>(e => e.CommandeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}