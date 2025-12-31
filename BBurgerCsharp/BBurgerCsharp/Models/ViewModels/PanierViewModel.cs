namespace BBurgerCsharp.Web.Models.ViewModels
{
    public class PanierItemViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; // "burger" ou "menu"
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string? Image { get; set; }
        public List<Complement> Complements { get; set; } = new();
        public decimal SousTotal => Prix * Quantite + Complements.Sum(c => c.Prix * Quantite);
    }

    public class PanierViewModel
    {
        public List<PanierItemViewModel> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.SousTotal);
        public int Count => Items.Count;
    }
}