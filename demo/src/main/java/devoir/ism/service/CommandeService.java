package devoir.ism.service;

import devoir.ism.entity.Commande;
import devoir.ism.entity.EtatCommande;
import devoir.ism.repository.CommandeRepository;

public class CommandeService {

    private final CommandeRepository commandeRepository;
    private final PaymentService paymentService;

    public CommandeService(CommandeRepository commandeRepository,
                           PaymentService paymentService) {
        this.commandeRepository = commandeRepository;
        this.paymentService = paymentService;
    }

    public void payerCommande(Commande commande) {

        boolean paiementOK = paymentService.effectuerPaiement(commande);

        if (paiementOK) {
            commande.setEtat(EtatCommande.PAYEE);
        } else {
            commande.setEtat(EtatCommande.ANNULEE);
        }

        commandeRepository.updateEtat(commande.getId(), commande.getEtat());
    }
}
