using EDATRACK.Controller;
using EDATRACK.Model;
using EDATRACK.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormProsesProduksi : Form
    {
        private ProsesProduksiController controller = new ProsesProduksiController();
        private GradingController gradingController = new GradingController();

        public FormProsesProduksi()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormProsesProduksi_Load(object sender, EventArgs e)
        {
            cboJenisProses.Items.Clear();
            cboJenisProses.Items.AddRange(new object[]
            {
                "Blanching",
                "Cooling",
                "IQF"
            });

            dtpWaktuMulai.Value = DateTime.Now;
            HitungWaktuSelesai();

            MuatBatchTersedia();
            MuatDaftarProses();

            if (Program.RoleSesi == "Admin")
                panelInput.Visible = false;
        }

        private void cboBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBatch.SelectedItem == null)
            {
                cboJenisProses.SelectedIndex = -1;
                return;
            }

            try
            {
                string kodeBatch = cboBatch.SelectedItem.ToString();
                string tahapBerikutnya = controller.GetTahapBerikutnya(kodeBatch);

                if (!string.IsNullOrEmpty(tahapBerikutnya))
                {
                    cboJenisProses.SelectedItem = tahapBerikutnya;
                }
                else
                {
                    cboJenisProses.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InputWaktu_Changed(object sender, EventArgs e)
        {
            HitungWaktuSelesai();
        }

        private void HitungWaktuSelesai()
        {
            if (int.TryParse(txtDurasi.Text.Trim(), out int durasi) && durasi >= 0)
            {
                dtpWaktuSelesai.Value = dtpWaktuMulai.Value.AddMinutes(durasi);
            }
            else
            {
                dtpWaktuSelesai.Value = dtpWaktuMulai.Value;
            }
        }

        private void MuatBatchTersedia()
        {
            cboBatch.Items.Clear();

            try
            {
                List<string> daftar = gradingController.GetBatchSudahGrading();
                HashSet<string> batchSelesai = controller.GetBatchSelesaiSemuaProses();

                foreach (string batch in daftar)
                {
                    if (!batchSelesai.Contains(batch))
                        cboBatch.Items.Add(batch);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MuatDaftarProses()
        {
            dgvProses.Rows.Clear();
            dgvProses.Columns.Clear();
            dgvProses.Columns.Add("colBatch", "Kode Batch");
            dgvProses.Columns.Add("colJenis", "Jenis Proses");
            dgvProses.Columns.Add("colSuhu", "Suhu (°C)");
            dgvProses.Columns.Add("colDurasi", "Durasi (menit)");
            dgvProses.Columns.Add("colMulai", "Waktu Mulai");
            dgvProses.Columns.Add("colSelesai", "Waktu Selesai");

            try
            {
                List<ProsesProduksi> daftar = controller.GetSemua();

                foreach (ProsesProduksi pp in daftar)
                {
                    dgvProses.Rows.Add(
                        pp.KodeBatch,
                        pp.JenisProses,
                        pp.Suhu,
                        pp.Durasi,
                        pp.WaktuMulai.ToString("dd/MM/yyyy HH:mm"),
                        pp.WaktuSelesai.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSimpanProses_Click(object sender, EventArgs e)
        {
            if (Program.RoleSesi != "Pekerja")
            {
                MessageBox.Show("Hanya Pekerja yang dapat mencatat proses produksi.");
                return;
            }

            if (cboBatch.SelectedItem == null)
            {
                MessageBox.Show("Pilih kode batch.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboJenisProses.SelectedItem == null)
            {
                MessageBox.Show("Pilih jenis proses.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSuhu.Text.Trim(), out decimal suhu))
            {
                MessageBox.Show("Suhu harus berupa angka.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi) || durasi <= 0)
            {
                MessageBox.Show("Durasi harus berupa angka lebih dari 0 menit.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ProsesProduksi data = new ProsesProduksi();
                data.KodeBatch = cboBatch.SelectedItem.ToString();
                data.JenisProses = cboJenisProses.SelectedItem.ToString();
                data.Suhu = suhu;
                data.Durasi = durasi;
                data.WaktuMulai = dtpWaktuMulai.Value;
                data.HitungWaktuSelesai();
                data.IdPekerja = Program.IdSesi;

                controller.Simpan(data);

                MessageBox.Show("Data proses produksi berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BersihkanForm();
                MuatBatchTersedia();
                MuatDaftarProses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi Urutan Proses",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BersihkanForm()
        {
            cboBatch.SelectedIndex = -1;
            cboJenisProses.SelectedIndex = -1;
            txtSuhu.Text = "";
            txtDurasi.Text = "";
            dtpWaktuMulai.Value = DateTime.Now;
            HitungWaktuSelesai();
        }
    }
}