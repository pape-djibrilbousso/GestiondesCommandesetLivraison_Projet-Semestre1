package devoir.ism.repository;

import devoir.ism.config.database.DatabaseConfig;
import devoir.ism.entity.Burger;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class BurgerRepository {

    public Burger save(Burger burger) {
        String sql = """
            INSERT INTO burger(nom, prix, image, archive)
            VALUES (?, ?, ?, ?)
            RETURNING id
        """;

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setString(1, burger.getNom());
            stmt.setDouble(2, burger.getPrix());
            stmt.setString(3, burger.getImage());
            stmt.setBoolean(4, burger.isArchive());

            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return new Burger(
                        rs.getInt("id"),
                        burger.getNom(),
                        burger.getPrix(),
                        burger.getImage()
                );
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public List<Burger> findAll() {
        List<Burger> burgers = new ArrayList<>();
        String sql = "SELECT * FROM burger WHERE archive = false";

        try (Connection conn = DatabaseConfig.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {

            while (rs.next()) {
                Burger burger = new Burger(
                        rs.getInt("id"),
                        rs.getString("nom"),
                        rs.getDouble("prix"),
                        rs.getString("image")
                );
                burgers.add(burger);
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return burgers;
    }

    public void archive(int id) {
        String sql = "UPDATE burger SET archive = true WHERE id = ?";

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setInt(1, id);
            stmt.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public Burger findById(int id) {
        String sql = "SELECT * FROM burger WHERE id = ?";

        try (Connection conn = DatabaseConfig.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setInt(1, id);
            ResultSet rs = stmt.executeQuery();

            if (rs.next()) {
                return new Burger(
                        rs.getInt("id"),
                        rs.getString("nom"),
                        rs.getDouble("prix"),
                        rs.getString("image")
                );
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
}
