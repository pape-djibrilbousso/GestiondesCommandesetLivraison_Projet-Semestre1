using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("livreur")]
    public class Livreur
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

        [NotMapped]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}