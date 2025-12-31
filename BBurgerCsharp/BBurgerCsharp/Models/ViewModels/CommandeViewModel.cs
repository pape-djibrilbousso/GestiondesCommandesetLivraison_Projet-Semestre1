using System.ComponentModel.DataAnnotations;

namespace BBurgerCsharp.Web.Models.ViewModels
{
    public class CommandeViewModel
    {
        [Required(ErrorMessage = "Le type de livraison est obligatoire")]
        [Display(Name = "Type de livraison")]
        public string TypeLivraison { get; set; } = string.Empty;

        [Display(Name = "Zone de livraison")]
        public int? ZoneId { get; set; }

        [Required(ErrorMessage = "Le moyen de paiement est obligatoire")]
        [Display(Name = "Moyen de paiement")]
        public string MoyenPaiement { get; set; } = string.Empty;

        public List<Zone> ZonesDisponibles { get; set; } = new();
        public decimal MontantTotal { get; set; }
        public decimal FraisLivraison { get; set; }
        public decimal MontantFinal => MontantTotal + FraisLivraison;
    }
}