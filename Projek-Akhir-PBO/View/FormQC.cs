using EDATRACK.Controller;
using EDATRACK.Model;
using Projek_Akhir_PBO.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormQC : Form
    {
        private QCController controller = new QCController();

        public FormQC()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormQC_Load(object sender, EventArgs e)
        {
            cboKondisiFisik.DataSource = Enum.GetValues(typeof(KondisiFisikQC));
            cboKondisiFisik.SelectedIndex = -1;

            cboKondisiWarna.DataSource = Enum.GetValues(typeof(KondisiWarnaQC));
            cboKondisiWarna.SelectedIndex = -1;

            cboKondisiKemasan.DataSource = Enum.GetValues(typeof(KondisiKemasanQC));
            cboKondisiKemasan.SelectedIndex = -1;

            cboHasilQC.Items.Clear();
            cboHasilQC.Items.AddRange(new object[] { "Lulus", "Tidak Lulus" });
            cboHasilQC.SelectedIndex = -1;

            AturTampilanPengemasan(false);
            MuatBatchTersedia();
            MuatDaftarQC();

            if (Program.RoleSesi == "Admin")
                panelInput.Visible = false;
        }

        private void cboHasilQC_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool lulus = cboHasilQC.SelectedItem != null &&
                         cboHasilQC.SelectedItem.ToString() == "Lulus";
            AturTampilanPengemasan(lulus);
        }

        private void AturTampilanPengemasan(bool tampil)
        {
            lblSeparator.Visible = tampil;
            lblBeratKemasan.Visible = tampil;
            txtBeratKemasan.Visible = tampil;
            lblJumlahKemasan.Visible = tampil;
            txtJumlahKemasan.Visible = tampil;
            lblJenisKemasan.Visible = tampil;
            txtJenisKemasan.Visible = tampil;

            if (!tampil)
            {
                txtBeratKemasan.Text = "";
                txtJumlahKemasan.Text = "";
                txtJenisKemasan.Text = "";
            }

            // Geser posisi tombol Simpan & tinggi card form agar tidak menyisakan ruang kosong
            Control acuanBawah = tampil ? (Control)txtJenisKemasan : txtCatatan;
            btnSimpanQC.Top = acuanBawah.Bottom + 35;

            if (btnSimpanQC.Parent != null)
            {
                btnSimpanQC.Parent.Height = btnSimpanQC.Bottom + 24;
            }
        }

        private void MuatBatchTersedia()
        {
            cboBatch.Items.Clear();

            try
            {
                List<string> daftar = controller.GetBatchSiapQC();
                foreach (string batch in daftar)
                    cboBatch.Items.Add(batch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MuatDaftarQC()
        {
            dgvQC.Rows.Clear();
            dgvQC.Columns.Clear();
            dgvQC.Columns.Add("colBatch", "Kode Batch");
            dgvQC.Columns.Add("colTanggal", "Tanggal QC");
            dgvQC.Columns.Add("colFisik", "Kondisi Fisik");
            dgvQC.Columns.Add("colWarna", "Kondisi Warna");
            dgvQC.Columns.Add("colKemasan", "Kondisi Kemasan");
            dgvQC.Columns.Add("colHasil", "Hasil QC");
            dgvQC.Columns.Add("colCatatan", "Catatan QC");
            dgvQC.Columns.Add("colBeratKemasan", "Berat/Kemasan (kg)");
            dgvQC.Columns.Add("colJumlahKemasan", "Jumlah Kemasan");
            dgvQC.Columns.Add("colJenisKemasan", "Jenis Kemasan");

            try
            {
                List<QualityControl> daftar = controller.GetSemua();

                foreach (QualityControl q in daftar)
                {
                    bool tampilKemasan = q.IsLulus && q.DataPengemasan != null;

                    dgvQC.Rows.Add(
                        q.KodeBatch,
                        q.TanggalQC.ToString("dd/MM/yyyy"),
                        q.KondisiFisik,
                        q.KondisiWarna,
                        q.KondisiKemasan,
                        q.HasilQC,
                        q.CatatanQC,
                        tampilKemasan ? q.DataPengemasan.BeratPerKemasan.ToString("0.##") : "-",
                        tampilKemasan ? q.DataPengemasan.JumlahKemasan.ToString() : "-",
                        tampilKemasan ? q.DataPengemasan.JenisKemasan : "-"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSimpanQC_Click(object sender, EventArgs e)
        {
            if (Program.RoleSesi != "Pekerja")
            {
                MessageBox.Show("Hanya Pekerja yang dapat mencatat QC & pengemasan.");
                return;
            }

            if (cboBatch.SelectedItem == null)
            {
                MessageBox.Show("Pilih kode batch.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKondisiFisik.SelectedItem == null ||
                cboKondisiWarna.SelectedItem == null ||
                cboKondisiKemasan.SelectedItem == null)
            {
                MessageBox.Show("Pilih kondisi fisik, kondisi warna, dan kondisi kemasan.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboHasilQC.SelectedItem == null)
            {
                MessageBox.Show("Pilih hasil QC (Lulus / Tidak Lulus).", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool lulus = cboHasilQC.SelectedItem.ToString() == "Lulus";
            decimal beratKemasan = 0;
            int jumlahKemasan = 0;

            if (lulus)
            {
                if (!decimal.TryParse(txtBeratKemasan.Text.Trim(), out beratKemasan) || beratKemasan <= 0)
                {
                    MessageBox.Show("Berat per kemasan harus berupa angka lebih dari 0.", "Validasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtJumlahKemasan.Text.Trim(), out jumlahKemasan) || jumlahKemasan <= 0)
                {
                    MessageBox.Show("Jumlah kemasan harus berupa angka lebih dari 0.", "Validasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtJenisKemasan.Text))
                {
                    MessageBox.Show("Jenis kemasan wajib diisi jika hasil QC Lulus.", "Validasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                QualityControl data = new QualityControl();
                data.KodeBatch = cboBatch.SelectedItem.ToString();
                data.TanggalQC = dtpTanggalQC.Value.Date;
                data.KondisiFisik = (KondisiFisikQC)cboKondisiFisik.SelectedItem;
                data.KondisiWarna = (KondisiWarnaQC)cboKondisiWarna.SelectedItem;
                data.KondisiKemasan = (KondisiKemasanQC)cboKondisiKemasan.SelectedItem;
                data.HasilQC = cboHasilQC.SelectedItem.ToString();
                data.CatatanQC = txtCatatan.Text.Trim();
                data.IdPekerja = Program.IdSesi;

                if (lulus)
                {
                    data.DataPengemasan = new Pengemasan
                    {
                        KodeBatch = data.KodeBatch,
                        BeratPerKemasan = beratKemasan,
                        JumlahKemasan = jumlahKemasan,
                        JenisKemasan = txtJenisKemasan.Text.Trim()
                    };
                }

                controller.Simpan(data);

                MessageBox.Show("Data Quality Control berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BersihkanForm();
                MuatBatchTersedia();
                MuatDaftarQC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BersihkanForm()
        {
            cboBatch.SelectedIndex = -1;
            cboKondisiFisik.SelectedIndex = -1;
            cboKondisiWarna.SelectedIndex = -1;
            cboKondisiKemasan.SelectedIndex = -1;
            cboHasilQC.SelectedIndex = -1;
            txtCatatan.Text = "";
            AturTampilanPengemasan(false);
        }
    }
}