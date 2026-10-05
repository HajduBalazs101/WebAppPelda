using MySql.Data.MySqlClient;
using WebAppPelda.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace WebAppPelda.Services
{
    public class VasarloService : Vasarlo
    {
        #region Read
        public List<Vasarlo> GetAllVasarlo()
        {
            List<Vasarlo> vasarlok = new List<Vasarlo>();
            string connectionString = "SERVER = localhost;" +
                          "DATABASE= webapppeldadb;" +
                          "UID = root;" +
                          "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            string sql = "SELECT * FROM vasarlo";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Vasarlo vasarlo = new Vasarlo();
                vasarlo.Id = reader.GetInt32("Id");
                vasarlo.Nev = reader.GetString("Nev");
                vasarlo.Cim = reader.GetString("Cim");
                vasarlo.Email = reader.GetString("Email");
                vasarlo.Telefon = reader.GetString("Telefon");
                vasarlo.Pontszam = reader.GetInt32("Pontszam");
                vasarlok.Add(vasarlo);
            }
            conn.Close();
            return vasarlok;
        }
        public Vasarlo GetById(int id)
        {
            try
            {
                Vasarlo result = new Vasarlo();

                string connectionString = "SERVER = localhost;" +
                                 "DATABASE= webapppeldadb;" +
                                 "UID = root;" +
                                 "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string sql = "SELECT * FROM vasarlo WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.Id = reader.GetInt32("Id");
                    result.Nev = reader.GetString("Nev");
                    result.Cim = reader.GetString("Cim");
                    result.Email = reader.GetString("Email");
                    result.Telefon = reader.GetString("Telefon");
                    result.Pontszam = reader.GetInt32("Pontszam");
                }
                else
                {
                    Console.WriteLine("Nincs ilyen vásárló!");
                }
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Create
        public string PostVasarlo(Vasarlo vasarlo)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "INSERT INTO vasarlo(Nev, Cim, Email, Telefon, Pontszam) VALUES (@nev, @cim, @email, @telefon, @pontszam)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", vasarlo.Nev);
                cmd.Parameters.AddWithValue("@cim", vasarlo.Cim);
                cmd.Parameters.AddWithValue("@email", vasarlo.Email);
                cmd.Parameters.AddWithValue("@telefon", vasarlo.Telefon);
                cmd.Parameters.AddWithValue("@pontszam", vasarlo.Pontszam);
                int sorokSzama = cmd.ExecuteNonQuery();
                conn.Close();
                return sorokSzama > 0 ? "Sikeres beszúrás" : "Sikertelen beszúrás";
            }
            catch (Exception ex)
            {
                return "Hiba történt: " + ex.Message;
            }
        }
        #endregion

        #region Update
        public string PutVasarlo(Vasarlo vasarlo)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                                  "DATABASE= webapppeldadb;" +
                                  "UID = root;" +
                                  "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();



                string sql = "UPDATE vasarlo SET Nev = @nev, Cim = @cim, Email = @email, Telefon = @telefon, Pontszam = @pontszam WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", vasarlo.Id);
                cmd.Parameters.AddWithValue("@nev", vasarlo.Nev);
                cmd.Parameters.AddWithValue("@cim", vasarlo.Cim);
                cmd.Parameters.AddWithValue("@email", vasarlo.Email);
                cmd.Parameters.AddWithValue("@Telefon", vasarlo.Telefon);
                cmd.Parameters.AddWithValue("@pontszam", vasarlo.Pontszam);
                int sorokSzama = cmd.ExecuteNonQuery();
                conn.Close();
                return sorokSzama > 0 ? "Sikeres frissítés" : "Sikertelen frissítés";
            }
            catch (Exception ex)
            {
                return "Hiba történt: " + ex.Message;
            }
        }
        #endregion

        #region Delete
        public string DeleteVasarlo(int Id)
        {
            string connectionString = "SERVER = localhost;" +
                             "DATABASE= webapppeldadb;" +
                             "UID = root;" +
                             "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string sql = "DELETE FROM vasarlo WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Id);

            int sorokSzama = cmd.ExecuteNonQuery();
            conn.Close();
            return sorokSzama > 0 ? "Sikeres törlés" : "Sikertelen törlés";
        }
        #endregion
    }
}
