package devoir.ism.service;

import devoir.ism.entity.Commande;

public interface PaymentService {
    boolean effectuerPaiement(Commande commande);
}
