package devoir.ism.ui;

import devoir.ism.entity.Commande;
import devoir.ism.entity.EtatCommande;
import devoir.ism.entity.Produit;
import devoir.ism.repository.CommandeRepository;
import devoir.ism.repository.ProduitRepository;
import devoir.ism.service.CommandeService;

import java.util.List;
import java.util.Scanner;

public class GestionnaireUI {

    private final CommandeService commandeService;
    private final CommandeRepository commandeRepository;
    private final ProduitRepository produitRepository;

    public GestionnaireUI(CommandeService commandeService,
                          CommandeRepository commandeRepository,
                          ProduitRepository produitRepository) {
        this.commandeService = commandeService;
        this.commandeRepository = commandeRepository;
        this.produitRepository = produitRepository;
    }

    public void afficherMenuGestionnaire() {
        Scanner scanner = new Scanner(System.in);
        boolean retour = false;

        while (!retour) {
            System.out.println("\n===== MENU GESTIONNAIRE =====");
            System.out.println("1. Lister toutes les commandes");
            System.out.println("2. Modifier l'état d'une commande");
            System.out.println("3. Affecter commande à un livreur");
            System.out.println("4. Gérer le catalogue");
            System.out.println("5. Retour");
            System.out.print("Choix : ");
            String choix = scanner.nextLine();

            switch (choix) {
                case "1":
                    listerCommandes();
                    break;
                case "2":
                    modifierEtatCommande(scanner);
                    break;
                case "3":
                    affecterLivreur(scanner);
                    break;
                case "4":
                    gererCatalogue(scanner);
                    break;
                case "5":
                    retour = true;
                    break;
                default:
                    System.out.println(" Choix invalide !");
            }
        }
    }

    private void listerCommandes() {
        List<Commande> commandes = commandeRepository.getToutesCommandes();
        if (commandes.isEmpty()) {
            System.out.println("Aucune commande trouvée !");
        } else {
            commandes.forEach(c -> System.out.println(
                "ID: " + c.getId() +
                " | Montant: " + c.getMontant() +
                " FCFA | Etat: " + c.getEtat() +
                " | Livreur: " + (c.getLivreur() != null ? c.getLivreur() : "Non affecté")
            ));
        }
    }

    private void modifierEtatCommande(Scanner scanner) {
        System.out.print("ID de la commande : ");
        int id = Integer.parseInt(scanner.nextLine());
        Commande commande = commandeRepository.getCommandeById(id);

        if (commande != null) {
            System.out.println("Etat actuel : " + commande.getEtat());
            System.out.print("Nouvel état (EN_COURS, PAYEE, ANNULEE, LIVREE) : ");
            String nouvelEtat = scanner.nextLine();

            try {
                commande.setEtat(EtatCommande.valueOf(nouvelEtat));
                commandeRepository.updateEtat(commande.getId(), commande.getEtat());
                System.out.println(" Etat mis à jour !");
            } catch (Exception e) {
                System.out.println(" Etat invalide !");
            }
        } else {
            System.out.println(" Commande introuvable !");
        }
    }

    private void affecterLivreur(Scanner scanner) {
        System.out.print("ID de la commande : ");
        int id = Integer.parseInt(scanner.nextLine());
        Commande commande = commandeRepository.getCommandeById(id);

        if (commande != null) {
            System.out.print("Nom du livreur à affecter : ");
            String livreur = scanner.nextLine();
            commande.setLivreur(livreur);
            commandeRepository.updateLivreur(commande.getId(), livreur);
            System.out.println(" Livreur affecté !");
        } else {
            System.out.println(" Commande introuvable !");
        }
    }

    private void gererCatalogue(Scanner scanner) {
        boolean retour = false;

        while (!retour) {
            System.out.println("\n----- GESTION CATALOGUE -----");
            System.out.println("1. Lister les produits");
            System.out.println("2. Ajouter un produit");
            System.out.println("3. Modifier un produit");
            System.out.println("4. Supprimer un produit");
            System.out.println("5. Retour");
            System.out.print("Choix : ");
            String choix = scanner.nextLine();

            switch (choix) {
                case "1":
                    listerProduits();
                    break;
                case "2":
                    ajouterProduit(scanner);
                    break;
                case "3":
                    modifierProduit(scanner);
                    break;
                case "4":
                    supprimerProduit(scanner);
                    break;
                case "5":
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
            System.out.println("Aucun produit dans le catalogue !");
        } else {
            produits.forEach(p -> System.out.println(
                "ID: " + p.getId() +
                " | Nom: " + p.getNom() +
                " | Prix: " + p.getPrix() + " FCFA"
            ));
        }
    }

    private void ajouterProduit(Scanner scanner) {
        System.out.print("Nom du produit : ");
        String nom = scanner.nextLine();
        System.out.print("Prix du produit : ");
        double prix = Double.parseDouble(scanner.nextLine());

        Produit produit = new Produit();
        produit.setNom(nom);
        produit.setPrix(prix);
        produitRepository.ajouterProduit(produit);

        System.out.println(" Produit ajouté !");
    }

    private void modifierProduit(Scanner scanner) {
        System.out.print("ID du produit à modifier : ");
        int id = Integer.parseInt(scanner.nextLine());
        Produit produit = produitRepository.getProduitById(id);

        if (produit != null) {
            System.out.print("Nouveau nom (" + produit.getNom() + ") : ");
            String nom = scanner.nextLine();
            if (!nom.isEmpty()) produit.setNom(nom);

            System.out.print("Nouveau prix (" + produit.getPrix() + ") : ");
            String prixStr = scanner.nextLine();
            if (!prixStr.isEmpty()) produit.setPrix(Double.parseDouble(prixStr));

            produitRepository.updateProduit(produit);
            System.out.println(" Produit modifié !");
        } else {
            System.out.println(" Produit introuvable !");
        }
    }

    private void supprimerProduit(Scanner scanner) {
        System.out.print("ID du produit à supprimer : ");
        int id = Integer.parseInt(scanner.nextLine());
        produitRepository.supprimerProduit(id);
        System.out.println(" Produit supprimé !");
    }
}
