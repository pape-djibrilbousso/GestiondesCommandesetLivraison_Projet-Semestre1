package devoir.ism.entity;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Commande {

    private int id;
    private Client client;
    private EtatCommande etat;
    private TypeCommande type;
    private double montant;
    private String livreur;
    private LocalDateTime dateCommande;

    private List<Burger> burgers = new ArrayList<>();
    private List<Menu> menus = new ArrayList<>();
    private List<Complement> complements = new ArrayList<>();

    public Commande(int id, Client client, TypeCommande type) {
        this.id = id;
        this.client = client;
        this.type = type;
        this.etat = EtatCommande.EN_COURS;
        this.montant = 0;
        this.dateCommande = LocalDateTime.now();
        this.livreur = null;
    }

    public Commande(int id, Client client, double montant, EtatCommande etat,
                    TypeCommande type, LocalDateTime dateCommande, List<Complement> complements) {
        this.id = id;
        this.client = client;
        this.montant = montant;
        this.etat = etat;
        this.type = type;
        this.dateCommande = dateCommande;
        this.livreur = null;
        if (complements != null) {
            this.complements.addAll(complements);
        }
    }


    public void ajouterBurger(Burger burger) {
        burgers.add(burger);
        recalculerMontant();
    }

    public void ajouterMenu(Menu menu) {
        menus.add(menu);
        recalculerMontant();
    }

    public void ajouterComplement(Complement complement) {
        complements.add(complement);
        recalculerMontant();
    }


    private void recalculerMontant() {
        montant = 0;

        for (Burger b : burgers) {
            montant += b.getPrix();
        }

        for (Menu m : menus) {
            montant += m.getPrix();
        }

        for (Complement c : complements) {
            montant += c.getPrix();
        }
    }


    public int getId() {
        return id;
    }

    public Client getClient() {
        return client;
    }

    public EtatCommande getEtat() {
        return etat;
    }

    public void setEtat(EtatCommande etat) {
        this.etat = etat;
    }

    public double getMontant() {
        return montant;
    }

    public void setMontant(double montant) {
        this.montant = montant;
    }

    public List<Complement> getComplements() {
        return complements;
    }

    public TypeCommande getType() {
        return type;
    }

    public void setType(TypeCommande type) {
        this.type = type;
    }

    public String getLivreur() {
        return livreur;
    }

    public void setLivreur(String livreur) {
        this.livreur = livreur;
    }

    public LocalDateTime getDateCommande() {
        return dateCommande;
    }

    public void setDateCommande(LocalDateTime dateCommande) {
        this.dateCommande = dateCommande;
    }
}
