using System;
using System.Collections.Generic;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class PenerimaanController
    {
        public void Simpan(DataPenerimaan data)
        {
            if (string.IsNullOrEmpty(data.KodeBatch))
                throw new Exception("Kode batch tidak boleh kosong.");
            if (string.IsNullOrEmpty(data.AsalBahanBaku))
                throw new Exception("Asal bahan baku tidak boleh kosong.");
            if (data.BeratKg <= 0)
                throw new Exception("Berat harus lebih dari 0.");

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "INSERT INTO penerimaan " +
                    "(kode_batch, tanggal_penerimaan, asal_bahan_baku, berat_kg, " +
                    "kondisi_awal, keterangan, id_pekerja) " +
                    "VALUES (@batch, @tgl, @asal, @berat, @kondisi, @ket, @pekerja)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@batch", data.KodeBatch);
                    cmd.Parameters.AddWithValue("@tgl", data.TanggalPenerimaan);
                    cmd.Parameters.AddWithValue("@asal", data.AsalBahanBaku);
                    cmd.Parameters.AddWithValue("@berat", data.BeratKg);
                    cmd.Parameters.AddWithValue("@kondisi", data.KondisiAwal.ToString());
                    cmd.Parameters.AddWithValue("@ket", data.Keterangan ?? "");
                    cmd.Parameters.AddWithValue("@pekerja", data.IdPekerja);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        public List<DataPenerimaan> GetSemua()
        {
            List<DataPenerimaan> daftar = new List<DataPenerimaan>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT p.kode_batch, p.tanggal_penerimaan, p.asal_bahan_baku, " +
                    "p.berat_kg, p.kondisi_awal, p.keterangan, u.nama_lengkap " +
                    "FROM penerimaan p " +
                    "JOIN users u ON p.id_pekerja = u.id " +
                    "ORDER BY p.tanggal_input DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataPenerimaan d = new DataPenerimaan();
                        d.KodeBatch = reader.GetString(0);
                        d.TanggalPenerimaan = reader.GetDateTime(1);
                        d.AsalBahanBaku = reader.GetString(2);
                        d.BeratKg = reader.GetDecimal(3);

                        string kondisiDb = reader.IsDBNull(4) ? "Baik" : reader.GetString(4);
                        d.KondisiAwal = Enum.TryParse(kondisiDb, true, out KondisiBahanBaku hasilKondisi)
                            ? hasilKondisi
                            : KondisiBahanBaku.Baik;

                        d.Keterangan = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        daftar.Add(d);
                    }
                }
            }

            return daftar;
        }

        
        public List<string> GetBatchBelumGrading()
        {
            List<string> daftar = new List<string>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query =
                    "SELECT kode_batch FROM penerimaan " +
                    "WHERE kode_batch NOT IN (SELECT kode_batch FROM grading) " +
                    "ORDER BY tanggal_input DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        daftar.Add(reader.GetString(0));
                }
            }

            return daftar;
        }

        
        public decimal GetBeratBatch(string kodeBatch)
        {
            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                string query = "SELECT berat_kg FROM penerimaan WHERE kode_batch = @batch";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                {
                    cmd.Parameters.AddWithValue("@batch", kodeBatch);
                    object hasil = cmd.ExecuteScalar();
                    return hasil != null ? Convert.ToDecimal(hasil) : 0;
                }
            }
        }
    }
}