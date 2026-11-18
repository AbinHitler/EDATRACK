using EDATRACK.Controller;
using EDATRACK.Model;
using EDATRACK.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormGrading : Form
    {
        private GradingController controller = new GradingController();
        private PenerimaanController penerimaanController = new PenerimaanController();

        public FormGrading()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormGrading_Load(object sender, EventArgs e)
        {
            cboKondisiWarna.DataSource = Enum.GetValues(typeof(KondisiWarnaEdamame));
            cboKondisiWarna.SelectedIndex = -1;

            cboKondisiFisik.DataSource = Enum.GetValues(typeof(KondisiFisikEdamame));
            cboKondisiFisik.SelectedIndex = -1;

            MuatBatchTersedia();
            MuatDaftarGrading();

            if (Program.RoleSesi == "Admin")
                panelInput.Visible = false;
        }

        private void MuatBatchTersedia()
        {
            cboBatch.Items.Clear();

            try
            {
                List<string> daftar = penerimaanController.GetBatchBelumGrading();
                foreach (string batch in daftar)
                    cboBatch.Items.Add(batch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MuatDaftarGrading()
        {
            dgvGrading.Rows.Clear();
            dgvGrading.Columns.Clear();
            dgvGrading.Columns.Add("colBatch", "Kode Batch");
            dgvGrading.Columns.Add("colTanggal", "Tanggal");
            dgvGrading.Columns.Add("colSampel", "Jumlah Sampel");
            dgvGrading.Columns.Add("colWarna", "Kondisi Warna");
            dgvGrading.Columns.Add("colFisik", "Kondisi Fisik");
            dgvGrading.Columns.Add("colUkuran", "Ukuran Polong");
            dgvGrading.Columns.Add("colA", "Grade A (kg)");
            dgvGrading.Columns.Add("colB", "Grade B (kg)");
            dgvGrading.Columns.Add("colR", "Reject (kg)");
            dgvGrading.Columns.Add("colPetugas", "Petugas");

            try
            {
                List<Grading> daftar = controller.GetSemua();

                foreach (Grading g in daftar)
                {
                    dgvGrading.Rows.Add(
                        g.KodeBatch,
                        g.TanggalGrading.ToString("dd/MM/yyyy"),
                        g.JumlahSampel,
                        g.KondisiWarna,
                        g.KondisiFisik,
                        g.UkuranPolong,
                        g.BeratGradeA,
                        g.BeratGradeB,
                        g.BeratReject,
                        g.NamaPetugas
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSimpanGrading_Click(object sender, EventArgs e)
        {
            if (Program.RoleSesi != "Pekerja")
            {
                MessageBox.Show("Hanya Pekerja yang dapat mencatat grading.");
                return;
            }

            if (cboBatch.SelectedItem == null)
            {
                MessageBox.Show("Pilih kode batch.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKondisiWarna.SelectedItem == null || cboKondisiFisik.SelectedItem == null)
            {
                MessageBox.Show("Pilih kondisi warna dan kondisi fisik.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahSampel.Text, out int sampel) || sampel <= 0)
            {
                MessageBox.Show("Jumlah sampel harus berupa angka lebih dari 0.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal gradeA, gradeB, reject;
            if (!decimal.TryParse(txtGradeA.Text, out gradeA) ||
                !decimal.TryParse(txtGradeB.Text, out gradeB) ||
                !decimal.TryParse(txtReject.Text, out reject))
            {
                MessageBox.Show("Berat Grade A, B, dan Reject harus angka.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Grading data = new Grading();
                data.KodeBatch = cboBatch.SelectedItem.ToString();
                data.TanggalGrading = dtpTanggalGrading.Value.Date;
                data.JumlahSampel = sampel;
                data.KondisiWarna = (KondisiWarnaEdamame)cboKondisiWarna.SelectedItem;
                data.KondisiFisik = (KondisiFisikEdamame)cboKondisiFisik.SelectedItem;
                data.UkuranPolong = txtUkuranPolong.Text.Trim();
                data.BeratGradeA = gradeA;
                data.BeratGradeB = gradeB;
                data.BeratReject = reject;
                data.IdPekerja = Program.IdSesi;

                controller.Simpan(data);

                MessageBox.Show("Hasil grading berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MuatBatchTersedia();
                MuatDaftarGrading();
                BersihkanForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BersihkanForm()
        {
            cboBatch.SelectedIndex = -1;
            txtJumlahSampel.Text = "";
            cboKondisiWarna.SelectedIndex = -1;
            cboKondisiFisik.SelectedIndex = -1;
            txtUkuranPolong.Text = "";
            txtGradeA.Text = "";
            txtGradeB.Text = "";
            txtReject.Text = "";
        }
    }
}