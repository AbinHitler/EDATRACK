using EDATRACK.Controller;
using EDATRACK.Model;
using EDATRACK.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormPenerimaan : Form
    {
        private PenerimaanController controller = new PenerimaanController();

        public FormPenerimaan()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormPenerimaan_Load(object sender, EventArgs e)
        {
            cboKondisi.DataSource = Enum.GetValues(typeof(KondisiBahanBaku));
            cboKondisi.SelectedIndex = -1;

            MuatDaftarPenerimaan();

            
            if (Program.RoleSesi == "Admin")
                panelInput.Visible = false;
        }

        private void MuatDaftarPenerimaan()
        {
            dgvPenerimaan.Rows.Clear();
            dgvPenerimaan.Columns.Clear();
            dgvPenerimaan.Columns.Add("colBatch", "Kode Batch");
            dgvPenerimaan.Columns.Add("colTanggal", "Tanggal");
            dgvPenerimaan.Columns.Add("colAsal", "Asal Bahan Baku");
            dgvPenerimaan.Columns.Add("colBerat", "Berat (kg)");
            dgvPenerimaan.Columns.Add("colKondisi", "Kondisi Awal");
            dgvPenerimaan.Columns.Add("colKeterangan", "Keterangan");

            try
            {
                List<DataPenerimaan> daftar = controller.GetSemua();

                foreach (DataPenerimaan d in daftar)
                {
                    dgvPenerimaan.Rows.Add(
                        d.KodeBatch,
                        d.TanggalPenerimaan.ToString("dd/MM/yyyy"),
                        d.AsalBahanBaku,
                        d.BeratKg,
                        d.KondisiAwal,
                        d.Keterangan
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (Program.RoleSesi != "Pekerja")
            {
                MessageBox.Show("Hanya Pekerja yang dapat mencatat penerimaan.");
                return;
            }

            decimal berat;
            if (!decimal.TryParse(txtBerat.Text, out berat))
            {
                MessageBox.Show("Berat harus berupa angka.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKondisi.SelectedItem == null)
            {
                MessageBox.Show("Pilih kondisi awal bahan baku.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                DataPenerimaan data = new DataPenerimaan();
                data.KodeBatch = txtKodeBatch.Text.Trim();
                data.TanggalPenerimaan = dtpTanggal.Value.Date;
                data.AsalBahanBaku = txtAsal.Text.Trim();
                data.BeratKg = berat;
                data.KondisiAwal = (KondisiBahanBaku)cboKondisi.SelectedItem;
                data.Keterangan = txtKeterangan.Text.Trim();
                data.IdPekerja = Program.IdSesi;

                
                controller.Simpan(data);

                MessageBox.Show("Data penerimaan berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtKodeBatch.Text = "";
                txtAsal.Text = "";
                txtBerat.Text = "";
                cboKondisi.SelectedIndex = -1;
                txtKeterangan.Text = "";
                MuatDaftarPenerimaan();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("23505"))
                    MessageBox.Show("Kode batch sudah digunakan.", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}