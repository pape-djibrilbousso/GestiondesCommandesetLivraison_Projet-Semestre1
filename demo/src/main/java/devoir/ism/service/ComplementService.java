package devoir.ism.service;

import devoir.ism.entity.Complement;
import devoir.ism.repository.ComplementRepository;

import java.util.List;

public class ComplementService {

    private final ComplementRepository complementRepository;

    public ComplementService(ComplementRepository complementRepository) {
        this.complementRepository = complementRepository;
    }

    public Complement ajouterComplement(String nom, double prix, String image) {
        if (nom == null || nom.isEmpty()) {
            throw new IllegalArgumentException("Le nom du complément est obligatoire");
        }
        if (prix <= 0) {
            throw new IllegalArgumentException("Le prix doit être supérieur à 0");
        }

        Complement complement = new Complement(0, nom, prix, image);
        return complementRepository.save(complement);
    }

    public List<Complement> listerComplements() {
        return complementRepository.findAll();
    }

    public Complement getComplementById(int id) {
        return complementRepository.findById(id);
    }
}
