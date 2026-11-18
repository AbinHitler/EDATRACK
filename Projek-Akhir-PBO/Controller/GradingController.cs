using System;
using System.Collections.Generic;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class GradingController
    {
        private PenerimaanController penerimaanController = new PenerimaanController();

        // Simpan hasil grading
        public void Simpan(Grading data)
        {
            // Validasi total berat grading tidak melebihi berat bahan baku
            decimal beratBahanBaku = penerimaanController.GetBeratBatch(data.KodeBatch);
            decimal totalGrading = data.BeratGradeA + data.BeratGradeB + data.BeratReject;

            if (totalGrading > beratBahanBaku)
                throw new Exception(
                    "Total berat grading (" + totalGrading + " kg) " +
                    "melebihi berat bahan baku (" + beratBahanBaku + " kg).");

            if (data.BeratGradeA < 0 || data.BeratGradeB < 0 || data.BeratReject < 0)
                throw new Exception("Berat tidak boleh negatif.");

            if (data.JumlahSampel <= 0)
                throw new Exception("Jumlah sampel harus lebih dari 0.");

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "INSERT INTO grading " +
                    "(kode_batch, tanggal_grading, jumlah_sampel, kondisi_warna, " +
                    "kondisi_fisik, ukuran_polong, " +
                    "berat_grade_a, berat_grade_b, berat_reject, id_pekerja) " +
                    "VALUES (@batch, @tgl, @sampel, @warna, @fisik, @ukuran, " +
                    "@a, @b, @reject, @pekerja)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@batch", data.KodeBatch);
                    cmd.Parameters.AddWithValue("@tgl", data.TanggalGrading);
                    cmd.Parameters.AddWithValue("@sampel", data.JumlahSampel);
                    cmd.Parameters.AddWithValue("@warna", data.KondisiWarna.ToString());
                    cmd.Parameters.AddWithValue("@fisik", data.KondisiFisik.ToString());
                    cmd.Parameters.AddWithValue("@ukuran", data.UkuranPolong ?? "");
                    cmd.Parameters.AddWithValue("@a", data.BeratGradeA);
                    cmd.Parameters.AddWithValue("@b", data.BeratGradeB);
                    cmd.Parameters.AddWithValue("@reject", data.BeratReject);
                    cmd.Parameters.AddWithValue("@pekerja", data.IdPekerja);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ambil semua data grading
        public List<Grading> GetSemua()
        {
            List<Grading> daftar = new List<Grading>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT g.kode_batch, g.tanggal_grading, g.jumlah_sampel, " +
                    "g.kondisi_warna, g.kondisi_fisik, g.ukuran_polong, " +
                    "g.berat_grade_a, g.berat_grade_b, g.berat_reject, u.nama_lengkap " +
                    "FROM grading g " +
                    "JOIN users u ON g.id_pekerja = u.id " +
                    "ORDER BY g.tanggal_input DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grading g = new Grading();
                        g.KodeBatch = reader.GetString(0);
                        g.TanggalGrading = reader.GetDateTime(1);
                        g.JumlahSampel = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);

                        string warnaDb = reader.IsDBNull(3) ? "HijauSegar" : reader.GetString(3);
                        g.KondisiWarna = Enum.TryParse(warnaDb, true, out KondisiWarnaEdamame hasilWarna)
                            ? hasilWarna
                            : KondisiWarnaEdamame.HijauSegar;

                        string fisikDb = reader.IsDBNull(4) ? "Utuh" : reader.GetString(4);
                        g.KondisiFisik = Enum.TryParse(fisikDb, true, out KondisiFisikEdamame hasilFisik)
                            ? hasilFisik
                            : KondisiFisikEdamame.Utuh;

                        g.UkuranPolong = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        g.BeratGradeA = reader.GetDecimal(6);
                        g.BeratGradeB = reader.GetDecimal(7);
                        g.BeratReject = reader.GetDecimal(8);
                        g.NamaPetugas = reader.GetString(9);
                        daftar.Add(g);
                    }
                }
            }

            return daftar;
        }

        // Ambil batch yang sudah digrading (untuk proses produksi)
        public List<string> GetBatchSudahGrading()
        {
            List<string> daftar = new List<string>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT kode_batch FROM grading ORDER BY tanggal_input DESC";

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