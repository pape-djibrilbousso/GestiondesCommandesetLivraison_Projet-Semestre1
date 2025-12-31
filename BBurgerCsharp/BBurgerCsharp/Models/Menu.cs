using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("menu")]
    public class Menu
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("image")]
        public string? Image { get; set; }

        [Column("est_archive")]
        public bool EstArchive { get; set; } = false;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        public virtual ICollection<MenuComposition> Composition { get; set; } = new List<MenuComposition>();

        [NotMapped]
        public decimal PrixTotal { get; set; }
    }
}