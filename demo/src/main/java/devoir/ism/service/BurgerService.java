package devoir.ism.service;

import devoir.ism.entity.Burger;
import devoir.ism.repository.BurgerRepository;

import java.util.List;

public class BurgerService {

    private final BurgerRepository burgerRepository;

    public BurgerService(BurgerRepository burgerRepository) {
        this.burgerRepository = burgerRepository;
    }

    public Burger ajouterBurger(String nom, double prix, String image) {
        if (nom == null || nom.isEmpty()) {
            throw new IllegalArgumentException("Le nom du burger est obligatoire");
        }
        if (prix <= 0) {
            throw new IllegalArgumentException("Le prix doit être supérieur à 0");
        }

        Burger burger = new Burger(0, nom, prix, image);
        return burgerRepository.save(burger);
    }

    public List<Burger> listerBurgers() {
        return burgerRepository.findAll();
    }

    public void archiverBurger(int id) {
        burgerRepository.archive(id);
    }

    public Burger getBurgerById(int id) {
        return burgerRepository.findById(id);
    }
}
