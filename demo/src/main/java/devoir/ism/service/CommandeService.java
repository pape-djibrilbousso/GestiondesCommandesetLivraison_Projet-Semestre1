package devoir.ism.service;

import devoir.ism.entity.*;
import devoir.ism.repository.CommandeRepository;

import java.time.LocalDateTime;
import java.util.List;

public class CommandeService {

    private final CommandeRepository commandeRepository;

    public CommandeService(CommandeRepository commandeRepository) {
        this.commandeRepository = commandeRepository;
    }

    public Commande creerCommande(Client client,
                                  double prixProduits,
                                  List<Complement> complements,
                                  TypeCommande type) {

        double total = prixProduits;

        if (complements != null) {
            for (Complement c : complements) {
                total += c.getPrix();
            }
        }

        Commande commande = new Commande(
                0,
                client,
                total,
                EtatCommande.EN_COURS,
                type,
                LocalDateTime.now(),
                complements
        );

        return commandeRepository.save(commande);
    }
}
