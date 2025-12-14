package devoir.ism.service;

import devoir.ism.entity.Commande;

public class WavePaymentService implements PaymentService {

    @Override
    public boolean effectuerPaiement(Commande commande) {
        System.out.println("Demande de paiement Wave : " + commande.getMontant() + " FCFA");

        return true; 
    }
}
