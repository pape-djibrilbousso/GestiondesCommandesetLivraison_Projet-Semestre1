package devoir.ism.repository;

import devoir.ism.config.database.DatabaseConfig;
import devoir.ism.entity.*;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class CommandeRepository {

    public Commande save(Commande commande) {
        String sql = """
            INSERT INTO commande(client_id, montant, etat, type)
            VALUES (?, ?, ?, ?)
            RETURNING id
        """;

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setInt(1, commande.getClient().getId());
            stmt.setDouble(2, commande.getMontant());
            stmt.setString(3, commande.getEtat().name());
            stmt.setString(4, commande.getType().name());

            ResultSet rs = stmt.executeQuery();
            if (!rs.next()) return null;

            int commandeId = rs.getInt("id");

            if (commande.getComplements() != null && !commande.getComplements().isEmpty()) {
                String sqlComp = "INSERT INTO commande_complement(commande_id, complement_id) VALUES (?, ?)";
                try (PreparedStatement stmtComp = conn.prepareStatement(sqlComp)) {
                    for (Complement c : commande.getComplements()) {
                        stmtComp.setInt(1, commandeId);
                        stmtComp.setInt(2, c.getId());
                        stmtComp.addBatch();
                    }
                    stmtComp.executeBatch();
                }
            }

            Commande saved = new Commande(commandeId, commande.getClient(), commande.getType());
            saved.setEtat(commande.getEtat());
            if (commande.getComplements() != null) {
                for (Complement c : commande.getComplements()) {
                    saved.ajouterComplement(c);
                }
            }
            return saved;

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void updateEtat(int commandeId, EtatCommande etat) {
    String sql = "UPDATE commande SET etat = ? WHERE id = ?";

    try (Connection conn = DatabaseConfig.getConnection();
         PreparedStatement stmt = conn.prepareStatement(sql)) {

        stmt.setString(1, etat.name());
        stmt.setInt(2, commandeId);
        stmt.executeUpdate();

    } catch (Exception e) {
        e.printStackTrace();
    }
}

    public void updateLivreur(int commandeId, String livreur) {
    String sql = "UPDATE commande SET livreur = ? WHERE id = ?";
    try (Connection conn = DatabaseConfig.getConnection();
         PreparedStatement stmt = conn.prepareStatement(sql)) {
        stmt.setString(1, livreur);
        stmt.setInt(2, commandeId);
        stmt.executeUpdate();
    } catch (Exception e) {
        e.printStackTrace();
    }
}

    public Commande getCommandeById(int id) {
        String sql = "SELECT * FROM commande WHERE id = ?";
        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setInt(1, id);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                int clientId = rs.getInt("client_id");
                Client client = new Client(clientId, "", "", "", "", "");
                TypeCommande type = TypeCommande.valueOf(rs.getString("type"));

                Commande commande = new Commande(id, client, type);
                commande.setEtat(EtatCommande.valueOf(rs.getString("etat")));

                String sqlComp = "SELECT complement_id FROM commande_complement WHERE commande_id = ?";
                try (PreparedStatement stmtComp = conn.prepareStatement(sqlComp)) {
                    stmtComp.setInt(1, id);
                    ResultSet rsComp = stmtComp.executeQuery();
                    ComplementRepository complementRepository = new ComplementRepository();
                    while (rsComp.next()) {
                        Complement c = complementRepository.findById(rsComp.getInt("complement_id"));
                        if (c != null) commande.ajouterComplement(c);
                    }
                }

                return commande;
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public List<Commande> getToutesCommandes() {
        List<Commande> commandes = new ArrayList<>();
        String sql = "SELECT id FROM commande";
        try (Connection conn = DatabaseConfig.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {

            while (rs.next()) {
                Commande c = getCommandeById(rs.getInt("id"));
                if (c != null) commandes.add(c);
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return commandes;
    }

    public void ajouterCommande(Commande commande) {
        save(commande);
    }

}
