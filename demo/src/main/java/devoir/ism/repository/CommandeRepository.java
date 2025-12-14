package devoir.ism.repository;

import devoir.ism.config.database.DatabaseConfig;
import devoir.ism.entity.*;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.List;

public class CommandeRepository {

    public Commande save(Commande commande) {
        String sql = """
            INSERT INTO commande(client_id, montant, etat, type, date_commande)
            VALUES (?, ?, ?, ?, ?)
            RETURNING id
        """;

        try (Connection conn = DatabaseConfig.getConnection()) {

            // 1. insertion commande
            PreparedStatement stmt = conn.prepareStatement(sql);
            stmt.setInt(1, commande.getClient().getId());
            stmt.setDouble(2, commande.getMontant());
            stmt.setString(3, commande.getEtat().name());
            stmt.setString(4, commande.getType().name());
            stmt.setTimestamp(5, Timestamp.valueOf(commande.getDateCommande()));

            ResultSet rs = stmt.executeQuery();
            if (!rs.next()) return null;

            int commandeId = rs.getInt("id");

            // 2. insertion compléments (si burger simple)
            if (commande.getComplements() != null) {
                String sqlComp = """
                    INSERT INTO commande_complement(commande_id, complement_id)
                    VALUES (?, ?)
                """;

                for (Complement c : commande.getComplements()) {
                    PreparedStatement stmtComp =
                            conn.prepareStatement(sqlComp);
                    stmtComp.setInt(1, commandeId);
                    stmtComp.setInt(2, c.getId());
                    stmtComp.executeUpdate();
                }
            }

            return new Commande(
                    commandeId,
                    commande.getClient(),
                    commande.getMontant(),
                    commande.getEtat(),
                    commande.getType(),
                    commande.getDateCommande(),
                    commande.getComplements()
            );

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
}
