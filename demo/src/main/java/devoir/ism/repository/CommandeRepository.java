package devoir.ism.repository;

import devoir.ism.config.database.DatabaseConfig;
import devoir.ism.entity.*;

import java.sql.*;
import java.time.LocalDateTime;

public class CommandeRepository {

    public Commande save(Commande commande) {
        String sql = """
            INSERT INTO commande(client_id, montant, etat, type, date_commande)
            VALUES (?, ?, ?, ?, ?)
            RETURNING id
        """;

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setInt(1, commande.getClient().getId());
            stmt.setDouble(2, commande.getMontant());
            stmt.setString(3, commande.getEtat().name());
            stmt.setString(4, commande.getType().name());
            stmt.setTimestamp(5, Timestamp.valueOf(commande.getDateCommande()));

            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return new Commande(
                        rs.getInt("id"),
                        commande.getClient(),
                        commande.getMontant(),
                        commande.getEtat(),
                        commande.getType(),
                        commande.getDateCommande()
                );
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
}
