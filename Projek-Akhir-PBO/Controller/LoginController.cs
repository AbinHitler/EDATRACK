using System;
using System.Security.Cryptography;
using System.Text;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class LoginController
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public Pengguna CekLogin(string username, string password)
        {
            string passwordHash = HashPassword(password);

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT id, nama_lengkap, role, status_aktif " +
                    "FROM users " +
                    "WHERE username = @u AND password_hash = @p";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", passwordHash);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool aktif = reader.GetBoolean(3);
                            if (!aktif)
                                return null; // Akun nonaktif

                            string role = reader.GetString(2);

                            Pengguna pengguna;
                            if (role == "Admin")
                                pengguna = new Admin();
                            else
                                pengguna = new Pekerja();

                            pengguna.Id = reader.GetInt32(0);
                            pengguna.NamaLengkap = reader.GetString(1);
                            pengguna.Username = username;
                            pengguna.StatusAktif = aktif;

                            return pengguna;
                        }
                    }
                }
            }

            return null;
        }
    }
}