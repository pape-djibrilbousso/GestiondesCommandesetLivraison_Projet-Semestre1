package devoir.ism.repository;

import devoir.ism.config.database.DatabaseConfig;
import devoir.ism.entity.Client;

import java.sql.*;

public class ClientRepository {

    public Client save(Client client) {
        String sql = """
            INSERT INTO client(nom, prenom, telephone, email, mot_de_passe)
            VALUES (?, ?, ?, ?, ?)
            RETURNING id
        """;

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setString(1, client.getNom());
            stmt.setString(2, client.getPrenom());
            stmt.setString(3, client.getTelephone());
            stmt.setString(4, client.getEmail());
            stmt.setString(5, client.getMotDePasse());

            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return new Client(
                        rs.getInt("id"),
                        client.getNom(),
                        client.getPrenom(),
                        client.getTelephone(),
                        client.getEmail(),
                        client.getMotDePasse()
                );
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public Client findByEmail(String email) {
        String sql = "SELECT * FROM client WHERE email = ?";

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setString(1, email);
            ResultSet rs = stmt.executeQuery();

            if (rs.next()) {
                return new Client(
                        rs.getInt("id"),
                        rs.getString("nom"),
                        rs.getString("prenom"),
                        rs.getString("telephone"),
                        rs.getString("email"),
                        rs.getString("mot_de_passe")
                );
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
}
