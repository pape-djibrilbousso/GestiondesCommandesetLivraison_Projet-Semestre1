using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("paiement")]
    public class Paiement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("commande_id")]
        public int CommandeId { get; set; }

        [Column("date_paiement")]
        public DateTime DatePaiement { get; set; } = DateTime.Now;

        [Required]
        [Column("montant")]
        public decimal Montant { get; set; }

        [Required]
        [Column("moyen_paiement")]
        public string MoyenPaiement { get; set; } = string.Empty;

        [ForeignKey("CommandeId")]
        public virtual Commande? Commande { get; set; }

        [NotMapped]
        public string MontantFormate => $"{Montant:N0} FCFA";
    }
}