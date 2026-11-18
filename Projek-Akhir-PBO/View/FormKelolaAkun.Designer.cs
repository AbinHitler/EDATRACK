namespace EDATRACK.View
{
    partial class FormKelolaAkun
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblJudul = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.dgvPekerja = new System.Windows.Forms.DataGridView();
            this.btnAktifkan = new System.Windows.Forms.Button();
            this.btnNonaktifkan = new System.Windows.Forms.Button();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPekerja)).BeginInit();
            this.SuspendLayout();

            // lblJudul
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Text = "Kelola Akun Pekerja";
            this.lblJudul.Location = new System.Drawing.Point(250, 15);
            this.lblJudul.Size = new System.Drawing.Size(350, 30);

            // panelForm
            this.panelForm.Name = "panelForm";
            this.panelForm.Location = new System.Drawing.Point(15, 55);
            this.panelForm.Size = new System.Drawing.Size(280, 450);

            // lblNama & txtNama
            this.lblNama.Name = "lblNama";
            this.lblNama.Text = "Nama Lengkap *";
            this.txtNama.Name = "txtNama";

            // lblUsername & txtUsername
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "Username *";
            this.txtUsername.Name = "txtUsername";

            // lblPassword & txtPassword
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "Password *";
            this.txtPassword.Name = "txtPassword";

            // btnTambah
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Text = "Tambah Akun Pekerja";
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);

            // dgvPekerja
            this.dgvPekerja.Name = "dgvPekerja";
            this.dgvPekerja.AllowUserToAddRows = false;
            this.dgvPekerja.ReadOnly = true;
            this.dgvPekerja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // btnAktifkan
            this.btnAktifkan.Name = "btnAktifkan";
            this.btnAktifkan.Text = "Aktifkan Akun Terpilih";
            this.btnAktifkan.Click += new System.EventHandler(this.btnAktifkan_Click);

            // btnNonaktifkan
            this.btnNonaktifkan.Name = "btnNonaktifkan";
            this.btnNonaktifkan.Text = "Nonaktifkan Akun Terpilih";
            this.btnNonaktifkan.Click += new System.EventHandler(this.btnNonaktifkan_Click);

            this.panelForm.Controls.Add(this.lblNama);
            this.panelForm.Controls.Add(this.txtNama);
            this.panelForm.Controls.Add(this.lblUsername);
            this.panelForm.Controls.Add(this.txtUsername);
            this.panelForm.Controls.Add(this.lblPassword);
            this.panelForm.Controls.Add(this.txtPassword);
            this.panelForm.Controls.Add(this.btnTambah);

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.dgvPekerja);
            this.Controls.Add(this.btnAktifkan);
            this.Controls.Add(this.btnNonaktifkan);

            // FormKelolaAkun
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Name = "FormKelolaAkun";
            this.Text = "EDATRACK - Kelola Akun Pekerja";
            this.Load += new System.EventHandler(this.FormKelolaAkun_Load);

            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPekerja)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.DataGridView dgvPekerja;
        private System.Windows.Forms.Button btnAktifkan;
        private System.Windows.Forms.Button btnNonaktifkan;
    }
}