package devoir.ism.entity;

public class Burger {

    private int id;
    private String nom;
    private double prix;
    private String image;
    private boolean archive;

    public Burger(int id, String nom, double prix, String image) {
        this.id = id;
        this.nom = nom;
        this.prix = prix;
        this.image = image;
        this.archive = false;
    }

    public int getId() {
        return id;
    }

    public String getNom() {
        return nom;
    }

    public double getPrix() {
        return prix;
    }

    public String getImage() {
        return image;
    }

    public boolean isArchive() {
        return archive;
    }

    public void setArchive(boolean archive) {
        this.archive = archive;
    }

    @Override
    public String toString() {
        return nom + " - " + prix + " FCFA";
    }
}
