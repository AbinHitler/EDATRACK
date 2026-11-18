using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class ProsesProduksiController
    {
        private static bool skemaTerdeteksi = false;
        private static string kolomSuhu = "suhu_celsius";
        private static string kolomDurasi = "durasi_menit";
        private static string kolomMulai = "waktu_mulai";
        private static string kolomSelesai = "waktu_selesai";

        private static string CariKolom(HashSet<string> cols, string[] kandidatUtama, params string[] kataKunci)
        {
            foreach (string k in kandidatUtama)
            {
                string match = cols.FirstOrDefault(c => c.Equals(k, StringComparison.OrdinalIgnoreCase));
                if (match != null) return match;
            }

            foreach (string key in kataKunci)
            {
                string match = cols.FirstOrDefault(c =>
                    !c.Equals("tanggal_input", StringComparison.OrdinalIgnoreCase) &&
                    c.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0);
                if (match != null) return match;
            }

            return null;
        }

        private void PastikanNamaKolom(NpgsqlConnection koneksi)
        {
            if (skemaTerdeteksi)
                return;

            using (NpgsqlCommand alter = new NpgsqlCommand(
                "ALTER TABLE proses_produksi " +
                "ADD COLUMN IF NOT EXISTS waktu_selesai TIMESTAMP, " +
                "ADD COLUMN IF NOT EXISTS tanggal_input TIMESTAMP DEFAULT CURRENT_TIMESTAMP;", koneksi))
            {
                alter.ExecuteNonQuery();
            }

            string sql =
                "SELECT column_name FROM information_schema.columns " +
                "WHERE table_schema = 'public' AND table_name = 'proses_produksi'";

            HashSet<string> cols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    cols.Add(reader.GetString(0));
            }

            kolomSuhu = CariKolom(cols, new[] { "suhu", "suhu_celsius", "suhu_celcius", "temperatur" }, "suhu", "temp") ?? "suhu_celsius";
            kolomDurasi = CariKolom(cols, new[] { "durasi", "durasi_menit", "lama_menit", "lama" }, "durasi", "lama") ?? "durasi_menit";
            kolomMulai = CariKolom(cols, new[] { "waktu_mulai", "jam_mulai", "mulai" }, "mulai") ?? "waktu_mulai";
            kolomSelesai = CariKolom(cols, new[] { "waktu_selesai", "jam_selesai", "selesai" }, "selesai") ?? "waktu_selesai";

            skemaTerdeteksi = true;
        }

        public HashSet<string> GetProsesTercatat(string kodeBatch)
        {
            HashSet<string> prosesAda = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(kodeBatch))
                return prosesAda;

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT DISTINCT jenis_proses FROM proses_produksi " +
                    "WHERE kode_batch = @batch";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@batch", kodeBatch);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                prosesAda.Add(reader.GetString(0).Trim());
                        }
                    }
                }
            }

            return prosesAda;
        }

        public string GetTahapBerikutnya(string kodeBatch)
        {
            HashSet<string> prosesAda = GetProsesTercatat(kodeBatch);

            if (!prosesAda.Contains("Blanching"))
                return "Blanching";
            if (!prosesAda.Contains("Cooling"))
                return "Cooling";
            if (!prosesAda.Contains("IQF"))
                return "IQF";

            return null;
        }

        public HashSet<string> GetBatchSelesaiSemuaProses()
        {
            HashSet<string> selesai = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT kode_batch FROM proses_produksi " +
                    "WHERE LOWER(jenis_proses) IN ('blanching', 'cooling', 'iqf') " +
                    "GROUP BY kode_batch " +
                    "HAVING COUNT(DISTINCT LOWER(jenis_proses)) >= 3";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        selesai.Add(reader.GetString(0));
                }
            }

            return selesai;
        }

        //validasi urutan Blanching -> Cooling -> IQF
        public void Simpan(ProsesProduksi data)
        {
            if (string.IsNullOrEmpty(data.KodeBatch))
                throw new Exception("Kode batch wajib dipilih.");
            if (string.IsNullOrEmpty(data.JenisProses))
                throw new Exception("Jenis proses wajib dipilih.");
            if (data.Durasi <= 0)
                throw new Exception("Durasi harus lebih dari 0 menit.");

            HashSet<string> prosesAda = GetProsesTercatat(data.KodeBatch);
            string tahapWajib = GetTahapBerikutnya(data.KodeBatch);

            if (tahapWajib == null)
            {
                throw new Exception(
                    $"Batch {data.KodeBatch} sudah menyelesaikan seluruh urutan proses produksi (Blanching, Cooling, dan IQF).");
            }

            if (prosesAda.Contains(data.JenisProses))
            {
                throw new Exception(
                    $"Proses {data.JenisProses} untuk batch {data.KodeBatch} sudah tercatat. Tahap berikutnya wajib {tahapWajib}.");
            }

            if (!data.JenisProses.Equals(tahapWajib, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Urutan proses tidak valid! Setiap batch wajib berurutan (Blanching → Cooling → IQF).\n" +
                    $"Tahap wajib saat ini untuk batch {data.KodeBatch} adalah: {tahapWajib}.");
            }

            data.HitungWaktuSelesai();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanNamaKolom(koneksi);

                string query =
                    $"INSERT INTO proses_produksi " +
                    $"(kode_batch, jenis_proses, {kolomSuhu}, {kolomDurasi}, {kolomMulai}, {kolomSelesai}, id_pekerja) " +
                    $"VALUES (@batch, @jenis, @suhu, @durasi, @mulai, @selesai, @pekerja)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@batch", data.KodeBatch);
                    cmd.Parameters.AddWithValue("@jenis", data.JenisProses);
                    cmd.Parameters.AddWithValue("@suhu", data.Suhu);
                    cmd.Parameters.AddWithValue("@durasi", data.Durasi);
                    cmd.Parameters.AddWithValue("@mulai", data.WaktuMulai);
                    cmd.Parameters.AddWithValue("@selesai", data.WaktuSelesai);
                    cmd.Parameters.AddWithValue("@pekerja", data.IdPekerja);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ambil semua data proses produksi
        public List<ProsesProduksi> GetSemua()
        {
            List<ProsesProduksi> daftar = new List<ProsesProduksi>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanNamaKolom(koneksi);

                string query =
                    $"SELECT pp.kode_batch, pp.jenis_proses, pp.{kolomSuhu}, pp.{kolomDurasi}, " +
                    $"pp.{kolomMulai}, pp.{kolomSelesai}, u.nama_lengkap " +
                    $"FROM proses_produksi pp " +
                    $"JOIN users u ON pp.id_pekerja = u.id " +
                    $"ORDER BY pp.tanggal_input DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProsesProduksi pp = new ProsesProduksi();
                        pp.KodeBatch = reader.GetString(0);
                        pp.JenisProses = reader.GetString(1);
                        pp.Suhu = Convert.ToDecimal(reader.GetValue(2));
                        pp.Durasi = Convert.ToInt32(reader.GetValue(3));
                        pp.WaktuMulai = reader.GetDateTime(4);
                        pp.WaktuSelesai = reader.IsDBNull(5)
                            ? pp.WaktuMulai.AddMinutes(pp.Durasi)
                            : reader.GetDateTime(5);
                        pp.NamaPetugas = reader.GetString(6);
                        daftar.Add(pp);
                    }
                }
            }

            return daftar;
        }

        public List<string> GetBatchSelesaiIQF()
        {
            List<string> daftar = new List<string>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT DISTINCT kode_batch FROM proses_produksi " +
                    "WHERE jenis_proses ILIKE '%IQF%' OR jenis_proses LIKE 'Pembekuan%' " +
                    "ORDER BY kode_batch DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        daftar.Add(reader.GetString(0));
                }
            }

            return daftar;
        }
    }
}