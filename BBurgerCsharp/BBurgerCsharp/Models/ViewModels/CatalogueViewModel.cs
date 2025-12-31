namespace BBurgerCsharp.Web.Models.ViewModels
{
    public class CatalogueViewModel
    {
        public List<Burger> Burgers { get; set; } = new();
        public List<MenuAvecPrix> Menus { get; set; } = new();
        public string? FiltreActif { get; set; }
    }

    public class MenuAvecPrix
    {
        public Menu Menu { get; set; } = new();
        public decimal PrixTotal { get; set; }
        public List<Burger> Burgers { get; set; } = new();
        public List<Complement> Complements { get; set; } = new();
    }

    public class DetailsBurgerViewModel
    {
        public Burger Burger { get; set; } = new();
        public List<Complement> Boissons { get; set; } = new();
        public List<Complement> Frites { get; set; } = new();
    }

    public class DetailsMenuViewModel
    {
        public Menu Menu { get; set; } = new();
        public decimal PrixTotal { get; set; }
        public List<Burger> Burgers { get; set; } = new();
        public List<Complement> Complements { get; set; } = new();
    }
}