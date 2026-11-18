using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using EDATRACK.Database;
using EDATRACK.Model;

namespace EDATRACK.Controller
{
    public class QCController
    {
        private static bool skemaTerdeteksi = false;
        private static string tabelQC = "quality_control";
        private static bool punyaTabelPengemasan = false;
        private static string kolomTanggal = "tanggal_qc";
        private static string kolomFisik = "kondisi_fisik";
        private static string kolomWarna = "kondisi_warna";
        private static string kolomKemasan = "kondisi_kemasan";
        private static string kolomHasil = "hasil_qc";
        private static string kolomCatatan = "catatan_qc";
        private static string kolomBeratKemasan = "berat_per_kemasan";
        private static string kolomJumlahKemasan = "jumlah_kemasan";
        private static string kolomJenisKemasan = "jenis_kemasan";
        private static string kolomTanggalKemasan = null;
        private static bool kemasPunyaIdQc = false;
        private static bool kemasPunyaKodeBatch = false;
        private static bool kemasPunyaIdPekerja = false;

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

        private void PastikanSkema(NpgsqlConnection koneksi)
        {
            if (skemaTerdeteksi)
                return;

            HashSet<string> tables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", koneksi))
            using (NpgsqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                    tables.Add(r.GetString(0));
            }

            if (tables.Contains("quality_control")) tabelQC = "quality_control";
            else if (tables.Contains("qc")) tabelQC = "qc";
            else if (tables.Contains("qc_pengemasan")) tabelQC = "qc_pengemasan";

            punyaTabelPengemasan = tables.Contains("pengemasan");

            HashSet<string> colsQC = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT column_name FROM information_schema.columns WHERE table_schema = 'public' AND table_name = @tbl", koneksi))
            {
                cmd.Parameters.AddWithValue("@tbl", tabelQC);
                using (NpgsqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                        colsQC.Add(r.GetString(0));
                }
            }

            kolomTanggal = CariKolom(colsQC, new[] { "tanggal_qc", "tanggal", "tgl_qc", "tanggal_pemeriksaan", "tgl" }, "tanggal", "tgl");
            if (kolomTanggal == null)
            {
                using (NpgsqlCommand alter = new NpgsqlCommand(
                    $"ALTER TABLE {tabelQC} ADD COLUMN IF NOT EXISTS tanggal_qc DATE DEFAULT CURRENT_DATE;", koneksi))
                {
                    alter.ExecuteNonQuery();
                }
                kolomTanggal = "tanggal_qc";
            }

            kolomFisik = CariKolom(colsQC, new[] { "kondisi_fisik", "fisik" }, "fisik") ?? "kondisi_fisik";
            kolomWarna = CariKolom(colsQC, new[] { "kondisi_warna", "warna" }, "warna") ?? "kondisi_warna";
            kolomKemasan = CariKolom(colsQC, new[] { "kondisi_kemasan", "kemasan" }, "kemasan") ?? "kondisi_kemasan";
            kolomHasil = CariKolom(colsQC, new[] { "hasil_qc", "status_qc", "hasil" }, "hasil", "status") ?? "hasil_qc";

            kolomCatatan = CariKolom(colsQC, new[] { "catatan_qc", "catatan", "keterangan" }, "catatan", "ket");
            if (kolomCatatan == null)
            {
                using (NpgsqlCommand alter = new NpgsqlCommand(
                    $"ALTER TABLE {tabelQC} ADD COLUMN IF NOT EXISTS catatan_qc TEXT;", koneksi))
                {
                    alter.ExecuteNonQuery();
                }
                kolomCatatan = "catatan_qc";
            }

            if (punyaTabelPengemasan)
            {
                HashSet<string> colsKemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT column_name FROM information_schema.columns WHERE table_schema = 'public' AND table_name = 'pengemasan'", koneksi))
                using (NpgsqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                        colsKemas.Add(r.GetString(0));
                }

                kolomBeratKemasan = CariKolom(colsKemas, new[] { "berat_per_kemasan", "berat_kemasan", "berat_kg", "berat" }, "berat") ?? "berat_per_kemasan";
                kolomJumlahKemasan = CariKolom(colsKemas, new[] { "jumlah_kemasan", "jumlah", "qty" }, "jumlah") ?? "jumlah_kemasan";
                kolomJenisKemasan = CariKolom(colsKemas, new[] { "jenis_kemasan", "jenis", "kemasan" }, "jenis", "kemasan") ?? "jenis_kemasan";
                kolomTanggalKemasan = CariKolom(colsKemas, new[] { "tanggal_pengemasan", "tanggal", "tgl_pengemasan" }, "tanggal", "tgl");
                kemasPunyaIdQc = colsKemas.Contains("id_qc");
                kemasPunyaKodeBatch = colsKemas.Contains("kode_batch");
                kemasPunyaIdPekerja = colsKemas.Contains("id_pekerja");
            }
            else
            {
                kolomBeratKemasan = CariKolom(colsQC, new[] { "berat_per_kemasan", "berat_kemasan", "berat_kg", "berat" }, "berat") ?? "berat_per_kemasan";
                kolomJumlahKemasan = CariKolom(colsQC, new[] { "jumlah_kemasan", "jumlah", "qty" }, "jumlah") ?? "jumlah_kemasan";
                kolomJenisKemasan = CariKolom(colsQC, new[] { "jenis_kemasan", "jenis" }, "jenis") ?? "jenis_kemasan";
            }

            skemaTerdeteksi = true;
        }

        // Ambil daftar batch yang siap dilakukan QC
        public List<string> GetBatchSiapQC()
        {
            List<string> daftar = new List<string>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanSkema(koneksi);

                string query =
                    $"SELECT DISTINCT pp.kode_batch FROM proses_produksi pp " +
                    $"WHERE pp.kode_batch NOT IN (SELECT kode_batch FROM {tabelQC}) " +
                    $"ORDER BY pp.kode_batch DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        daftar.Add(reader.GetString(0));
                }
            }

            return daftar;
        }

        // Simpan hasil QC dan data pengemasan (jika Lulus)
        public void Simpan(QualityControl data)
        {
            if (string.IsNullOrEmpty(data.KodeBatch))
                throw new Exception("Kode batch wajib dipilih.");
            if (string.IsNullOrEmpty(data.HasilQC))
                throw new Exception("Hasil QC wajib dipilih.");

            if (data.IsLulus)
            {
                if (data.DataPengemasan == null ||
                    data.DataPengemasan.BeratPerKemasan <= 0 ||
                    data.DataPengemasan.JumlahKemasan <= 0 ||
                    string.IsNullOrWhiteSpace(data.DataPengemasan.JenisKemasan))
                {
                    throw new Exception("Untuk hasil Lulus, berat per kemasan, jumlah kemasan, dan jenis kemasan wajib diisi.");
                }
            }

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanSkema(koneksi);

                using (NpgsqlTransaction trx = koneksi.BeginTransaction())
                {
                    try
                    {
                        if (punyaTabelPengemasan)
                        {
                            string queryQC =
                                $"INSERT INTO {tabelQC} " +
                                $"(kode_batch, {kolomTanggal}, {kolomFisik}, {kolomWarna}, {kolomKemasan}, {kolomHasil}, {kolomCatatan}, id_pekerja) " +
                                $"VALUES (@batch, @tgl, @fisik, @warna, @kemasan, @hasil, @catatan, @pekerja) " +
                                $"RETURNING id";

                            int idQcBaru;
                            using (NpgsqlCommand cmd = new NpgsqlCommand(queryQC, koneksi, trx))
                            {
                                cmd.Parameters.AddWithValue("@batch", data.KodeBatch);
                                cmd.Parameters.AddWithValue("@tgl", data.TanggalQC);
                                cmd.Parameters.AddWithValue("@fisik", data.KondisiFisik.ToString());
                                cmd.Parameters.AddWithValue("@warna", data.KondisiWarna.ToString());
                                cmd.Parameters.AddWithValue("@kemasan", data.KondisiKemasan.ToString());
                                cmd.Parameters.AddWithValue("@hasil", data.HasilQC);
                                cmd.Parameters.AddWithValue("@catatan", data.CatatanQC ?? "");
                                cmd.Parameters.AddWithValue("@pekerja", data.IdPekerja);
                                idQcBaru = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            if (data.IsLulus && data.DataPengemasan != null)
                            {
                                List<string> kolomList = new List<string>();
                                List<string> paramList = new List<string>();

                                if (kemasPunyaIdQc)
                                {
                                    kolomList.Add("id_qc");
                                    paramList.Add("@id_qc");
                                }
                                if (kemasPunyaKodeBatch)
                                {
                                    kolomList.Add("kode_batch");
                                    paramList.Add("@batch");
                                }
                                if (!string.IsNullOrEmpty(kolomTanggalKemasan))
                                {
                                    kolomList.Add(kolomTanggalKemasan);
                                    paramList.Add("@tgl_kemas");
                                }

                                kolomList.Add(kolomBeratKemasan);
                                paramList.Add("@berat");
                                kolomList.Add(kolomJumlahKemasan);
                                paramList.Add("@jumlah");
                                kolomList.Add(kolomJenisKemasan);
                                paramList.Add("@jenis");

                                if (kemasPunyaIdPekerja)
                                {
                                    kolomList.Add("id_pekerja");
                                    paramList.Add("@pekerja");
                                }

                                string queryKemas =
                                    $"INSERT INTO pengemasan ({string.Join(", ", kolomList)}) " +
                                    $"VALUES ({string.Join(", ", paramList)})";

                                using (NpgsqlCommand cmdKemas = new NpgsqlCommand(queryKemas, koneksi, trx))
                                {
                                    if (kemasPunyaIdQc)
                                        cmdKemas.Parameters.AddWithValue("@id_qc", idQcBaru);
                                    if (kemasPunyaKodeBatch)
                                        cmdKemas.Parameters.AddWithValue("@batch", data.KodeBatch);
                                    if (!string.IsNullOrEmpty(kolomTanggalKemasan))
                                        cmdKemas.Parameters.AddWithValue("@tgl_kemas", data.TanggalQC);

                                    cmdKemas.Parameters.AddWithValue("@berat", data.DataPengemasan.BeratPerKemasan);
                                    cmdKemas.Parameters.AddWithValue("@jumlah", data.DataPengemasan.JumlahKemasan);
                                    cmdKemas.Parameters.AddWithValue("@jenis", data.DataPengemasan.JenisKemasan);

                                    if (kemasPunyaIdPekerja)
                                        cmdKemas.Parameters.AddWithValue("@pekerja", data.IdPekerja);

                                    cmdKemas.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            string queryTunggal =
                                $"INSERT INTO {tabelQC} " +
                                $"(kode_batch, {kolomTanggal}, {kolomFisik}, {kolomWarna}, {kolomKemasan}, {kolomHasil}, {kolomCatatan}, " +
                                $"{kolomBeratKemasan}, {kolomJumlahKemasan}, {kolomJenisKemasan}, id_pekerja) " +
                                $"VALUES (@batch, @tgl, @fisik, @warna, @kemasan, @hasil, @catatan, @berat, @jumlah, @jenis, @pekerja)";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(queryTunggal, koneksi, trx))
                            {
                                cmd.Parameters.AddWithValue("@batch", data.KodeBatch);
                                cmd.Parameters.AddWithValue("@tgl", data.TanggalQC);
                                cmd.Parameters.AddWithValue("@fisik", data.KondisiFisik.ToString());
                                cmd.Parameters.AddWithValue("@warna", data.KondisiWarna.ToString());
                                cmd.Parameters.AddWithValue("@kemasan", data.KondisiKemasan.ToString());
                                cmd.Parameters.AddWithValue("@hasil", data.HasilQC);
                                cmd.Parameters.AddWithValue("@catatan", data.CatatanQC ?? "");
                                cmd.Parameters.AddWithValue("@berat",
                                    data.IsLulus ? (object)data.DataPengemasan.BeratPerKemasan : DBNull.Value);
                                cmd.Parameters.AddWithValue("@jumlah",
                                    data.IsLulus ? (object)data.DataPengemasan.JumlahKemasan : DBNull.Value);
                                cmd.Parameters.AddWithValue("@jenis",
                                    data.IsLulus ? (object)data.DataPengemasan.JenisKemasan : DBNull.Value);
                                cmd.Parameters.AddWithValue("@pekerja", data.IdPekerja);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trx.Commit();
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }

        // Ambil semua data QC beserta data pengemasan
        public List<QualityControl> GetSemua()
        {
            List<QualityControl> daftar = new List<QualityControl>();

            using (NpgsqlConnection koneksi = KoneksiDB.BukaKoneksi())
            {
                PastikanSkema(koneksi);

                string query;
                if (punyaTabelPengemasan)
                {
                    string joinCond = kemasPunyaIdQc
                        ? "q.id = pk.id_qc"
                        : "q.kode_batch = pk.kode_batch";

                    query =
                        $"SELECT q.kode_batch, q.{kolomTanggal}, q.{kolomFisik}, q.{kolomWarna}, " +
                        $"q.{kolomKemasan}, q.{kolomHasil}, q.{kolomCatatan}, " +
                        $"pk.{kolomBeratKemasan}, pk.{kolomJumlahKemasan}, pk.{kolomJenisKemasan}, u.nama_lengkap " +
                        $"FROM {tabelQC} q " +
                        $"LEFT JOIN pengemasan pk ON {joinCond} " +
                        $"JOIN users u ON q.id_pekerja = u.id " +
                        $"ORDER BY q.tanggal_input DESC";
                }
                else
                {
                    query =
                        $"SELECT q.kode_batch, q.{kolomTanggal}, q.{kolomFisik}, q.{kolomWarna}, " +
                        $"q.{kolomKemasan}, q.{kolomHasil}, q.{kolomCatatan}, " +
                        $"q.{kolomBeratKemasan}, q.{kolomJumlahKemasan}, q.{kolomJenisKemasan}, u.nama_lengkap " +
                        $"FROM {tabelQC} q " +
                        $"JOIN users u ON q.id_pekerja = u.id " +
                        $"ORDER BY q.tanggal_input DESC";
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QualityControl qc = new QualityControl();
                        qc.KodeBatch = reader.GetString(0);
                        qc.TanggalQC = reader.GetDateTime(1);

                        string fisikDb = reader.IsDBNull(2) ? "Utuh" : reader.GetString(2);
                        qc.KondisiFisik = Enum.TryParse(fisikDb, true, out KondisiFisikQC hasilFisik)
                            ? hasilFisik
                            : KondisiFisikQC.Utuh;

                        string warnaDb = reader.IsDBNull(3) ? "HijauSegar" : reader.GetString(3);
                        qc.KondisiWarna = Enum.TryParse(warnaDb, true, out KondisiWarnaQC hasilWarna)
                            ? hasilWarna
                            : KondisiWarnaQC.HijauSegar;

                        string kemasanDb = reader.IsDBNull(4) ? "Baik" : reader.GetString(4);
                        qc.KondisiKemasan = Enum.TryParse(kemasanDb, true, out KondisiKemasanQC hasilKemasan)
                            ? hasilKemasan
                            : KondisiKemasanQC.Baik;

                        qc.HasilQC = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        qc.CatatanQC = reader.IsDBNull(6) ? "" : reader.GetString(6);

                        if (!reader.IsDBNull(7) || !reader.IsDBNull(8) || !reader.IsDBNull(9))
                        {
                            qc.DataPengemasan = new Pengemasan
                            {
                                KodeBatch = qc.KodeBatch,
                                BeratPerKemasan = reader.IsDBNull(7) ? 0 : Convert.ToDecimal(reader.GetValue(7)),
                                JumlahKemasan = reader.IsDBNull(8) ? 0 : Convert.ToInt32(reader.GetValue(8)),
                                JenisKemasan = reader.IsDBNull(9) ? "" : reader.GetString(9)
                            };
                        }

                        qc.NamaPetugas = reader.GetString(10);
                        daftar.Add(qc);
                    }
                }
            }

            return daftar;
        }
    }
}