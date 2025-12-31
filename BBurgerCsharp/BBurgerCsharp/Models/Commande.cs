using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("commande")]
    public class Commande
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("date_commande")]
        public DateTime DateCommande { get; set; } = DateTime.Now;

        [Required]
        [Column("type_livraison")]
        public string TypeLivraison { get; set; } = string.Empty;

        [Column("etat")]
        public string Etat { get; set; } = "en_cours";

        [Required]
        [Column("montant_total")]
        public decimal MontantTotal { get; set; }

        [Column("zone_id")]
        public int? ZoneId { get; set; }

        [Column("livreur_id")]
        public int? LivreurId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }

        [ForeignKey("ZoneId")]
        public virtual Zone? Zone { get; set; }

        [ForeignKey("LivreurId")]
        public virtual Livreur? Livreur { get; set; }

        public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();
        public virtual ICollection<CommandeComplement> CommandeComplements { get; set; } = new List<CommandeComplement>();
        public virtual Paiement? Paiement { get; set; }

        [NotMapped]
        public string MontantFormate => $"{MontantTotal:N0} FCFA";

        [NotMapped]
        public string EtatLibelle => Etat switch
        {
            "en_cours" => "En cours",
            "validee" => "Validée",
            "terminee" => "Terminée",
            "annulee" => "Annulée",
            _ => Etat
        };
    }
}