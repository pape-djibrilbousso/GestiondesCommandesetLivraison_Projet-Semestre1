using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("complement")]
    public class Complement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [Column("prix")]
        public decimal Prix { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Column("est_archive")]
        public bool EstArchive { get; set; } = false;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [NotMapped]
        public string PrixFormate => $"{Prix:N0} FCFA";
    }
}