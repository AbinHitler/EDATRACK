using EDATRACK.Controller;
using EDATRACK.Model;
using Projek_Akhir_PBO.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormKelolaAkun : Form
    {
        private UserController controller = new UserController();

        public FormKelolaAkun()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
        }

        private void FormKelolaAkun_Load(object sender, EventArgs e)
        {
            if (Program.RoleSesi != "Admin")
            {
                MessageBox.Show("Akses ditolak. Hanya Admin yang dapat mengelola akun.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            MuatDaftarPekerja();
        }

        private void MuatDaftarPekerja()
        {
            dgvPekerja.Rows.Clear();
            dgvPekerja.Columns.Clear();
            dgvPekerja.Columns.Add("colId", "ID");
            dgvPekerja.Columns.Add("colNama", "Nama Lengkap");
            dgvPekerja.Columns.Add("colUsername", "Username");
            dgvPekerja.Columns.Add("colPassword", "Password");
            dgvPekerja.Columns.Add("colStatus", "Status");

            dgvPekerja.Columns["colId"].FillWeight = 20;

            try
            {
                List<Pekerja> daftar = controller.GetSemuaPekerja();
                foreach (Pekerja p in daftar)
                {
                    dgvPekerja.Rows.Add(
                        p.Id,
                        p.NamaLengkap,
                        p.Username,
                        p.Password,
                        p.StatusAktif ? "Aktif" : "Nonaktif"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                controller.TambahPekerja(
                    txtNama.Text.Trim(),
                    txtUsername.Text.Trim(),
                    txtPassword.Text.Trim()
                );

                MessageBox.Show("Akun pekerja berhasil ditambahkan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNama.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                MuatDaftarPekerja();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("23505"))
                    MessageBox.Show("Username sudah digunakan.", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAktifkan_Click(object sender, EventArgs e)
        {
            if (dgvPekerja.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih akun pekerja pada tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvPekerja.SelectedRows[0];
            int idPekerja = Convert.ToInt32(row.Cells["colId"].Value);
            string nama = row.Cells["colNama"].Value.ToString();
            string status = row.Cells["colStatus"].Value.ToString();

            if (status == "Aktif")
            {
                MessageBox.Show("Akun '" + nama + "' sudah dalam keadaan Aktif.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                "Aktifkan kembali akun pekerja '" + nama + "'?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    controller.AktifkanPekerja(idPekerja);
                    MessageBox.Show("Akun berhasil diaktifkan!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatDaftarPekerja();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnNonaktifkan_Click(object sender, EventArgs e)
        {
            if (dgvPekerja.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih akun pekerja pada tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvPekerja.SelectedRows[0];
            int idPekerja = Convert.ToInt32(row.Cells["colId"].Value);
            string nama = row.Cells["colNama"].Value.ToString();
            string status = row.Cells["colStatus"].Value.ToString();

            if (status == "Nonaktif")
            {
                MessageBox.Show("Akun '" + nama + "' sudah dalam keadaan Nonaktif.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                "Nonaktifkan akun pekerja '" + nama + "'?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    controller.NonaktifkanPekerja(idPekerja);
                    MessageBox.Show("Akun berhasil dinonaktifkan!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatDaftarPekerja();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}