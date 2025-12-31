using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("lignecommande")]
    public class LigneCommande
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("commande_id")]
        public int CommandeId { get; set; }

        [Required]
        [Column("type_produit")]
        public string TypeProduit { get; set; } = string.Empty;

        [Required]
        [Column("produit_id")]
        public int ProduitId { get; set; }

        [Required]
        [Column("quantite")]
        public int Quantite { get; set; } = 1;

        [Required]
        [Column("prix_unitaire")]
        public decimal PrixUnitaire { get; set; }

        [ForeignKey("CommandeId")]
        public virtual Commande? Commande { get; set; }

        [NotMapped]
        public decimal SousTotal => PrixUnitaire * Quantite;
    }
}