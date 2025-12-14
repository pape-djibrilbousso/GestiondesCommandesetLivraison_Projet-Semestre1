package devoir.ism.entity;

public class Menu {

    private int id;
    private String nom;
    private Burger burger;
    private double prix; // prix fixé par le gestionnaire
    private String image;

    public Menu(int id, String nom, Burger burger, double prix, String image) {
        this.id = id;
        this.nom = nom;
        this.burger = burger;
        this.prix = prix;
        this.image = image;
    }

    public int getId() {
        return id;
    }

    public String getNom() {
        return nom;
    }

    public Burger getBurger() {
        return burger;
    }

    public double getPrix() {
        return prix;
    }

    public String getImage() {
        return image;
    }

    @Override
    public String toString() {
        return nom + " - " + prix + " FCFA";
    }
}
