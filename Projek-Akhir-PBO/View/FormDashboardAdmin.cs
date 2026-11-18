using EDATRACK.View;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace EDATRACK.View
{
    public partial class FormDashboardAdmin : Form
    {
        private bool isLogout = false;

        public FormDashboardAdmin()
        {
            InitializeComponent();
            EDATrackTheme.Apply(this);
            this.FormClosed += FormDashboardAdmin_FormClosed;
        }

        private void FormDashboardAdmin_Load(object sender, EventArgs e)
        {
            FieldInfo fieldNama = typeof(Program).GetField(
                "NamaSesi", BindingFlags.Public | BindingFlags.Static);
            string nama = fieldNama?.GetValue(null)?.ToString();

            if (!string.IsNullOrEmpty(nama))
                lblSelamatDatang.Text = $"Selamat Datang, {nama} ({Program.RoleSesi})";
            else if (string.IsNullOrWhiteSpace(lblSelamatDatang.Text))
                lblSelamatDatang.Text = "Selamat Datang, Admin";
        }

        private void btnKelolaAkun_Click(object sender, EventArgs e)
        {
            new FormKelolaAkun().ShowDialog();
        }

        private void btnLihatPenerimaan_Click(object sender, EventArgs e)
        {
            new FormPenerimaan().ShowDialog();
        }

        private void btnLihatGrading_Click(object sender, EventArgs e)
        {
            new FormGrading().ShowDialog();
        }

        private void btnLihatProses_Click(object sender, EventArgs e)
        {
            new FormProsesProduksi().ShowDialog();
        }

        private void btnLihatQC_Click(object sender, EventArgs e)
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

        private void FormDashboardAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLogout)
            {
                Application.Exit();
            }
        }
    }
}