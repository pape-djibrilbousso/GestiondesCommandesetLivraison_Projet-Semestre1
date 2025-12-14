package devoir.ism.entity;

import java.util.List;

public class Menu {

    private int id;
    private String nom;
    private Burger burger;
    private List<Complement> complements;

    public Menu(int id, String nom, Burger burger, List<Complement> complements) {
        this.id = id;
        this.nom = nom;
        this.burger = burger;
        this.complements = complements;
    }

    public double getPrix() {
        double total = burger.getPrix();
        for (Complement c : complements) {
            total += c.getPrix();
        }
        return total;
    }

    @Override
    public String toString() {
        return nom + " - " + getPrix() + " FCFA";
    }
}
