package devoir.ism.entity;

import java.time.LocalDateTime;
import java.util.List;

public class Commande {

    private int id;
    private Client client;
    private double montant;
    private EtatCommande etat;
    private TypeCommande type;
    private LocalDateTime dateCommande;
    private List<Complement> complements; // pour burger simple

    public Commande(int id, Client client, double montant,
                    EtatCommande etat, TypeCommande type,
                    LocalDateTime dateCommande,
                    List<Complement> complements) {
        this.id = id;
        this.client = client;
        this.montant = montant;
        this.etat = etat;
        this.type = type;
        this.dateCommande = dateCommande;
        this.complements = complements;
    }

    public int getId() {
        return id;
    }

    public Client getClient() {
        return client;
    }

    public double getMontant() {
        return montant;
    }

    public EtatCommande getEtat() {
        return etat;
    }

    public void setEtat(EtatCommande etat) {
        this.etat = etat;
    }

    public TypeCommande getType() {
        return type;
    }

    public LocalDateTime getDateCommande() {
        return dateCommande;
    }

    public List<Complement> getComplements() {
        return complements;
    }
}
