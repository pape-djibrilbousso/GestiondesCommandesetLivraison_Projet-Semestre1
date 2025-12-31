using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("zone")]
    public class Zone
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [Column("quartiers")]
        public string Quartiers { get; set; } = string.Empty;

        [Required]
        [Column("prix_livraison")]
        public decimal PrixLivraison { get; set; }

        [NotMapped]
        public string PrixFormate => $"{PrixLivraison:N0} FCFA";
    }
}