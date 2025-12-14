package devoir.ism.entity;

import java.time.LocalDateTime;

public class Commande {

    private int id;
    private Client client;
    private double montant;
    private EtatCommande etat;
    private TypeCommande type;
    private LocalDateTime dateCommande;

    public Commande(int id, Client client, double montant,
                    EtatCommande etat, TypeCommande type,
                    LocalDateTime dateCommande) {
        this.id = id;
        this.client = client;
        this.montant = montant;
        this.etat = etat;
        this.type = type;
        this.dateCommande = dateCommande;
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
}
