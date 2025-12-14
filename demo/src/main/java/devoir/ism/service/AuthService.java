package devoir.ism.service;

import devoir.ism.entity.Client;
import devoir.ism.repository.ClientRepository;

public class AuthService {

    private final ClientRepository clientRepository;

    public AuthService(ClientRepository clientRepository) {
        this.clientRepository = clientRepository;
    }

    public Client inscription(String nom, String prenom, String telephone,
                              String email, String motDePasse) {

        if (clientRepository.findByEmail(email) != null) {
            throw new IllegalArgumentException("Email déjà utilisé");
        }

        Client client = new Client(
                0, nom, prenom, telephone, email, motDePasse
        );

        return clientRepository.save(client);
    }

    public Client connexion(String email, String motDePasse) {
        Client client = clientRepository.findByEmail(email);

        if (client == null || !client.getMotDePasse().equals(motDePasse)) {
            return null;
        }
        return client;
    }
}
