using EDATRACK.Database;
using Npgsql;
using Projek_Akhir_PBO.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormLogin : Form
    {
        private static string kolomPassword = null;

        public FormLogin()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private static string HashPassword(string password)
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

        private string PastikanKolomPassword(NpgsqlConnection koneksi)
        {
            if (!string.IsNullOrEmpty(kolomPassword))
                return kolomPassword;

            HashSet<string> cols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string sql =
                "SELECT column_name FROM information_schema.columns " +
                "WHERE table_schema = 'public' AND table_name = 'users'";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
            using (NpgsqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                    cols.Add(r.GetString(0));
            }

            string[] kandidat = { "password_hash", "password", "kata_sandi", "sandi", "pass" };
            foreach (string k in kandidat)
            {
                string match = cols.FirstOrDefault(c => c.Equals(k, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                {
                    kolomPassword = match;
                    return kolomPassword;
                }
            }

            kolomPassword = "password_hash";
            return kolomPassword;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan password wajib diisi.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idUser = 0;
                string roleUser = "";
                string namaLengkap = "";
                bool akunAktif = true;
                bool ditemukan = false;

                using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
                {
                    string colPass = PastikanKolomPassword(koneksi);
                    string query =
                        $"SELECT * FROM users WHERE username = @user AND ({colPass} = @pass OR {colPass} = @passHash) LIMIT 1";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.Parameters.AddWithValue("@passHash", HashPassword(password));

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ditemukan = true;
                                idUser = Convert.ToInt32(reader["id"]);
                                roleUser = reader["role"].ToString();

                                var kolom = Enumerable.Range(0, reader.FieldCount)
                                    .Select(reader.GetName)
                                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                                if (kolom.Contains("nama_lengkap") && !reader.IsDBNull(reader.GetOrdinal("nama_lengkap")))
                                    namaLengkap = reader["nama_lengkap"].ToString();

                                string[] kolomStatus = { "status_aktif", "status", "is_active" };
                                foreach (string ks in kolomStatus)
                                {
                                    if (kolom.Contains(ks) && !reader.IsDBNull(reader.GetOrdinal(ks)))
                                    {
                                        object val = reader[ks];
                                        if (val is bool aktifBool)
                                            akunAktif = aktifBool;
                                        else if (val.ToString().Equals("Nonaktif", StringComparison.OrdinalIgnoreCase) ||
                                                 val.ToString() == "0" ||
                                                 val.ToString().Equals("false", StringComparison.OrdinalIgnoreCase))
                                            akunAktif = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!ditemukan)
                {
                    MessageBox.Show("Username atau password salah.", "Login Gagal",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    return;
                }

                if (!akunAktif)
                {
                    MessageBox.Show("Akun Anda berstatus Nonaktif. Silakan hubungi Admin.", "Akses Ditolak",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Simpan sesi user yang login
                Program.IdSesi = idUser;
                Program.RoleSesi = roleUser;

                FieldInfo fieldNama = typeof(Program).GetField(
                    "NamaSesi", BindingFlags.Public | BindingFlags.Static);
                fieldNama?.SetValue(null, namaLengkap);

                // Sembunyikan FormLogin dan buka Dashboard sesuai role
                this.Hide();

                if (Program.RoleSesi.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    new FormDashboardAdmin().ShowDialog();
                }
                else
                {
                    new FormDashboardPekerja().ShowDialog();
                }

                // Jika Program.IdSesi == 0 artinya user klik Logout -> tampilkan FormLogin lagi
                if (Program.IdSesi == 0)
                {
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    this.Show();
                    txtUsername.Focus();
                }
                else
                {
                    // Jika user menutup Dashboard lewat tombol X -> tutup seluruh program
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat login: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}