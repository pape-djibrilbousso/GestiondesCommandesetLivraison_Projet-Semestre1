using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBurgerCsharp.Web.Models
{
    [Table("menucomposition")]
    public class MenuComposition
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("menu_id")]
        public int MenuId { get; set; }

        [Column("burger_id")]
        public int? BurgerId { get; set; }

        [Column("complement_id")]
        public int? ComplementId { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu? Menu { get; set; }

        [ForeignKey("BurgerId")]
        public virtual Burger? Burger { get; set; }

        [ForeignKey("ComplementId")]
        public virtual Complement? Complement { get; set; }
    }
}