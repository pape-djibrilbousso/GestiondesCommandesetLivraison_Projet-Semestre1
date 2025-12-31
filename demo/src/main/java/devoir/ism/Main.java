package devoir.ism;

import devoir.ism.entity.Commande;
import devoir.ism.entity.EtatCommande;
import devoir.ism.repository.CommandeRepository;
import devoir.ism.repository.ProduitRepository;
import devoir.ism.service.CommandeService;
import devoir.ism.service.PaymentService;
import devoir.ism.service.WavePaymentService;
import devoir.ism.ui.GestionnaireUI;
import devoir.ism.ui.ClientUI;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        CommandeRepository commandeRepository = new CommandeRepository();
        ProduitRepository produitRepository = new ProduitRepository();

        PaymentService paymentService = new WavePaymentService();
        CommandeService commandeService = new CommandeService(commandeRepository, paymentService);

        boolean quitter = false;

        while (!quitter) {
            System.out.println("\n===== APPLICATION DE COMMANDE =====");
            System.out.println("Vous êtes :");
            System.out.println("1. Client");
            System.out.println("2. Gestionnaire");
            System.out.println("3. Quitter");
            System.out.print("Choix : ");
            String role = scanner.nextLine();

            switch (role) {
                case "1":
                    ClientUI clientUI = new ClientUI(commandeService, produitRepository, commandeRepository);
                    clientUI.afficherMenuClient();
                    break;

                case "2":
                    GestionnaireUI gestionnaireUI = new GestionnaireUI(commandeService, commandeRepository, produitRepository);
                    gestionnaireUI.afficherMenuGestionnaire();
                    break;

                case "3":
                    quitter = true;
                    System.out.println("Au revoir !");
                    break;

                default:
                    System.out.println(" Choix invalide !");
            }
        }

        scanner.close();
    }
}
