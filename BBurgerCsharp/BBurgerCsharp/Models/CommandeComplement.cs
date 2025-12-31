using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("commandecomplement")]
    public class CommandeComplement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("commande_id")]
        public int CommandeId { get; set; }

        [Required]
        [Column("complement_id")]
        public int ComplementId { get; set; }

        [Required]
        [Column("quantite")]
        public int Quantite { get; set; } = 1;

        [ForeignKey("CommandeId")]
        public virtual Commande? Commande { get; set; }

        [ForeignKey("ComplementId")]
        public virtual Complement? Complement { get; set; }
    }
}