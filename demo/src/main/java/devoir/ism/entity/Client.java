package devoir.ism.entity;

public class Client {

    private int id;
    private String nom;
    private String prenom;
    private String telephone;
    private String email;
    private String motDePasse;

    public Client(int id, String nom, String prenom, String telephone,
                  String email, String motDePasse) {
        this.id = id;
        this.nom = nom;
        this.prenom = prenom;
        this.telephone = telephone;
        this.email = email;
        this.motDePasse = motDePasse;
    }

    public int getId() {
        return id;
    }

    public String getNom() {
        return nom;
    }

    public String getPrenom() {
        return prenom;
    }

    public String getTelephone() {
        return telephone;
    }

    public String getEmail() {
        return email;
    }

    public String getMotDePasse() {
        return motDePasse;
    }

    @Override
    public String toString() {
        return prenom + " " + nom + " (" + telephone + ")";
    }
}
