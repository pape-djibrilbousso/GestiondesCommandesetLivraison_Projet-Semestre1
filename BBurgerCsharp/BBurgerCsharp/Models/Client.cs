using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("client")]
    public class Client
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [Column("prenom")]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [Column("telephone")]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("date_inscription")]
        public DateTime DateInscription { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

        [NotMapped]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}