package devoir.ism.ui;

import devoir.ism.entity.Commande;
import devoir.ism.entity.Produit;
import devoir.ism.repository.CommandeRepository;
import devoir.ism.repository.ProduitRepository;
import devoir.ism.service.CommandeService;

import java.util.List;
import java.util.Scanner;

public class ClientUI {

    private final CommandeService commandeService;
    private final ProduitRepository produitRepository;
    private final CommandeRepository commandeRepository;

    public ClientUI(CommandeService commandeService,
                    ProduitRepository produitRepository,
                    CommandeRepository commandeRepository) {
        this.commandeService = commandeService;
        this.produitRepository = produitRepository;
        this.commandeRepository = commandeRepository;
    }

    public void afficherMenuClient() {
        Scanner scanner = new Scanner(System.in);
        boolean retour = false;

        while (!retour) {
            System.out.println("\n===== MENU CLIENT =====");
            System.out.println("1. Lister les produits");
            System.out.println("2. Créer une commande");
            System.out.println("3. Payer une commande");
            System.out.println("4. Retour");
            System.out.print("Choix : ");
            String choix = scanner.nextLine();

            switch (choix) {
                case "1":
                    listerProduits();
                    break;
                case "2":
                    creerCommande(scanner);
                    break;
                case "3":
                    payerCommande(scanner);
                    break;
                case "4":
                    retour = true;
                    break;
                default:
                    System.out.println(" Choix invalide !");
            }
        }
    }

    private void listerProduits() {
        List<Produit> produits = produitRepository.getTousProduits();
        if (produits.isEmpty()) {
            System.out.println("Aucun produit disponible !");
        } else {
            produits.forEach(p -> System.out.println(
                "ID: " + p.getId() +
                " | Nom: " + p.getNom() +
                " | Prix: " + p.getPrix() + " FCFA"
            ));
        }
    }

    private void creerCommande(Scanner scanner) {
        System.out.print("ID du produit à commander : ");
        int produitId = Integer.parseInt(scanner.nextLine());
        Produit produit = produitRepository.getProduitById(produitId);

        if (produit != null) {
            Commande commande = new Commande(produitId, null, produitId, null, null, null, null);
            commande.setMontant(produit.getPrix());
            commande.setEtat(devoir.ism.entity.EtatCommande.EN_COURS);
            commandeRepository.ajouterCommande(commande);
            System.out.println(" Commande créée avec ID : " + commande.getId());
        } else {
            System.out.println(" Produit introuvable !");
        }
    }

    private void payerCommande(Scanner scanner) {
        System.out.print("ID de la commande à payer : ");
        int id = Integer.parseInt(scanner.nextLine());
        Commande commande = commandeRepository.getCommandeById(id);

        if (commande != null) {
            commandeService.payerCommande(commande);
            System.out.println("Etat de la commande : " + commande.getEtat());
        } else {
            System.out.println(" Commande introuvable !");
        }
    }
}
