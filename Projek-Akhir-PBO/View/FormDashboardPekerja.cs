using Projek_Akhir_PBO.View;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormDashboardPekerja : Form
    {
        private bool isLogout = false;

        public FormDashboardPekerja()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
            this.FormClosed += FormDashboardPekerja_FormClosed;
        }

        private void FormDashboardPekerja_Load(object sender, EventArgs e)
        {
            FieldInfo fieldNama = typeof(Program).GetField(
                "NamaSesi", BindingFlags.Public | BindingFlags.Static);
            string nama = fieldNama?.GetValue(null)?.ToString();

            if (!string.IsNullOrEmpty(nama))
                lblSelamatDatang.Text = $"Selamat Datang, {nama} ({Program.RoleSesi})";
            else if (string.IsNullOrWhiteSpace(lblSelamatDatang.Text))
                lblSelamatDatang.Text = "Selamat Datang, Pekerja";
        }

        private void btnPenerimaan_Click(object sender, EventArgs e)
        {
            new FormPenerimaan().ShowDialog();
        }

        private void btnGrading_Click(object sender, EventArgs e)
        {
            new FormGrading().ShowDialog();
        }

        private void btnProsesProduksi_Click(object sender, EventArgs e)
        {
            new FormProsesProduksi().ShowDialog();
        }

        private void btnQC_Click(object sender, EventArgs e)
        {
            new FormQC().ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            isLogout = true;

            Program.IdSesi = 0;
            Program.RoleSesi = "";

            FieldInfo fieldNama = typeof(Program).GetField(
                "NamaSesi", BindingFlags.Public | BindingFlags.Static);
            fieldNama?.SetValue(null, "");

            this.Close();
        }

        private void FormDashboardPekerja_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLogout)
            {
                Application.Exit();
            }
        }
    }
}