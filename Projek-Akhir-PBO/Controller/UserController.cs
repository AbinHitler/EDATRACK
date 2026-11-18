using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class UserController
    {
        private string HashPassword(string password)
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

        // Otomatis menambahkan kolom password_plain jika belum ada di tabel users
        private void PastikanKolomPasswordAda(NpgsqlConnection koneksi)
        {
            string alterQuery =
                "ALTER TABLE users ADD COLUMN IF NOT EXISTS password_plain VARCHAR(100) DEFAULT '-';";
            using (NpgsqlCommand cmd = new NpgsqlCommand(alterQuery, koneksi))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // Ambil semua akun pekerja dari database
        public List<Pekerja> GetSemuaPekerja()
        {
            List<Pekerja> daftar = new List<Pekerja>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanKolomPasswordAda(koneksi);

                string query =
                    "SELECT id, nama_lengkap, username, " +
                    "CASE WHEN COALESCE(password_plain, '-') = '-' AND LENGTH(password_hash) < 64 " +
                    "     THEN password_hash ELSE COALESCE(password_plain, '-') END, " +
                    "status_aktif, tanggal_buat " +
                    "FROM users " +
                    "WHERE role = 'Pekerja' " +
                    "ORDER BY id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pekerja p = new Pekerja();
                        p.Id = reader.GetInt32(0);
                        p.NamaLengkap = reader.GetString(1);
                        p.Username = reader.GetString(2);
                        p.Password = reader.GetString(3);
                        p.StatusAktif = reader.GetBoolean(4);
                        daftar.Add(p);
                    }
                }
            }

            return daftar;
        }

        // Tambah akun pekerja baru
        public void TambahPekerja(string namaLengkap, string username, string password)
        {
            if (string.IsNullOrEmpty(namaLengkap) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
                throw new Exception("Nama, username, dan password wajib diisi.");

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanKolomPasswordAda(koneksi);

                string query =
                    "INSERT INTO users (nama_lengkap, username, password_hash, password_plain, role) " +
                    "VALUES (@nama, @user, @passHash, @passPlain, 'Pekerja')";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nama", namaLengkap);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@passHash", HashPassword(password));
                    cmd.Parameters.AddWithValue("@passPlain", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Aktifkan kembali akun pekerja
        public void AktifkanPekerja(int idPekerja)
        {
            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query = "UPDATE users SET status_aktif = TRUE WHERE id = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@id", idPekerja);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Nonaktifkan akun pekerja (soft delete)
        public void NonaktifkanPekerja(int idPekerja)
        {
            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query = "UPDATE users SET status_aktif = FALSE WHERE id = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@id", idPekerja);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}