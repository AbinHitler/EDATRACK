namespace EDATRACK.View
{
    partial class FormDashboardAdmin
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
            lblSelamatDatang = new Label();
            lblSubjudul = new Label();
            btnKelolaAkun = new Button();
            btnLihatPenerimaan = new Button();
            btnLihatGrading = new Button();
            btnLihatProses = new Button();
            btnLihatQC = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblSelamatDatang
            // 
            lblSelamatDatang.Font = new Font("Arial", 10F);
            lblSelamatDatang.Location = new Point(401, 138);
            lblSelamatDatang.Name = "lblSelamatDatang";
            lblSelamatDatang.Size = new Size(380, 25);
            lblSelamatDatang.TabIndex = 1;
            lblSelamatDatang.Text = "Selamat datang, Admin";
            lblSelamatDatang.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubjudul
            // 
            lblSubjudul.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblSubjudul.ForeColor = Color.DarkGreen;
            lblSubjudul.Location = new Point(501, 93);
            lblSubjudul.Name = "lblSubjudul";
            lblSubjudul.Size = new Size(200, 35);
            lblSubjudul.TabIndex = 0;
            lblSubjudul.Text = "EDATRACK";
            lblSubjudul.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnKelolaAkun
            // 
            btnKelolaAkun.BackColor = Color.DarkGreen;
            btnKelolaAkun.FlatStyle = FlatStyle.Flat;
            btnKelolaAkun.Font = new Font("Arial", 10F);
            btnKelolaAkun.ForeColor = Color.White;
            btnKelolaAkun.Location = new Point(416, 183);
            btnKelolaAkun.Name = "btnKelolaAkun";
            btnKelolaAkun.Size = new Size(360, 45);
            btnKelolaAkun.TabIndex = 2;
            btnKelolaAkun.Text = "Kelola Akun Pekerja";
            btnKelolaAkun.UseVisualStyleBackColor = false;
            btnKelolaAkun.Click += btnKelolaAkun_Click;
            // 
            // btnLihatPenerimaan
            // 
            btnLihatPenerimaan.BackColor = Color.SeaGreen;
            btnLihatPenerimaan.FlatStyle = FlatStyle.Flat;
            btnLihatPenerimaan.Font = new Font("Arial", 10F);
            btnLihatPenerimaan.ForeColor = Color.White;
            btnLihatPenerimaan.Location = new Point(416, 238);
            btnLihatPenerimaan.Name = "btnLihatPenerimaan";
            btnLihatPenerimaan.Size = new Size(360, 45);
            btnLihatPenerimaan.TabIndex = 3;
            btnLihatPenerimaan.Text = "Lihat Data Penerimaan Bahan Baku";
            btnLihatPenerimaan.UseVisualStyleBackColor = false;
            btnLihatPenerimaan.Click += btnLihatPenerimaan_Click;
            // 
            // btnLihatGrading
            // 
            btnLihatGrading.BackColor = Color.SeaGreen;
            btnLihatGrading.FlatStyle = FlatStyle.Flat;
            btnLihatGrading.Font = new Font("Arial", 10F);
            btnLihatGrading.ForeColor = Color.White;
            btnLihatGrading.Location = new Point(416, 293);
            btnLihatGrading.Name = "btnLihatGrading";
            btnLihatGrading.Size = new Size(360, 45);
            btnLihatGrading.TabIndex = 4;
            btnLihatGrading.Text = "Lihat Data Grading";
            btnLihatGrading.UseVisualStyleBackColor = false;
            btnLihatGrading.Click += btnLihatGrading_Click;
            // 
            // btnLihatProses
            // 
            btnLihatProses.BackColor = Color.SeaGreen;
            btnLihatProses.FlatStyle = FlatStyle.Flat;
            btnLihatProses.Font = new Font("Arial", 10F);
            btnLihatProses.ForeColor = Color.White;
            btnLihatProses.Location = new Point(416, 348);
            btnLihatProses.Name = "btnLihatProses";
            btnLihatProses.Size = new Size(360, 45);
            btnLihatProses.TabIndex = 5;
            btnLihatProses.Text = "Lihat Data Proses Produksi";
            btnLihatProses.UseVisualStyleBackColor = false;
            btnLihatProses.Click += btnLihatProses_Click;
            // 
            // btnLihatQC
            // 
            btnLihatQC.BackColor = Color.SeaGreen;
            btnLihatQC.FlatStyle = FlatStyle.Flat;
            btnLihatQC.Font = new Font("Arial", 10F);
            btnLihatQC.ForeColor = Color.White;
            btnLihatQC.Location = new Point(416, 403);
            btnLihatQC.Name = "btnLihatQC";
            btnLihatQC.Size = new Size(360, 45);
            btnLihatQC.TabIndex = 6;
            btnLihatQC.Text = "Lihat Data QC dan Pengemasan";
            btnLihatQC.UseVisualStyleBackColor = false;
            btnLihatQC.Click += btnLihatQC_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Firebrick;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Arial", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(416, 468);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(360, 45);
            btnLogout.TabIndex = 7;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // FormDashboardAdmin
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1182, 673);
            Controls.Add(lblSubjudul);
            Controls.Add(lblSelamatDatang);
            Controls.Add(btnKelolaAkun);
            Controls.Add(btnLihatPenerimaan);
            Controls.Add(btnLihatGrading);
            Controls.Add(btnLihatProses);
            Controls.Add(btnLihatQC);
            Controls.Add(btnLogout);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormDashboardAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EDATRACK - Dashboard Admin";
            Load += FormDashboardAdmin_Load;
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblSelamatDatang;
        private System.Windows.Forms.Label lblSubjudul;
        private System.Windows.Forms.Button btnKelolaAkun;
        private System.Windows.Forms.Button btnLihatPenerimaan;
        private System.Windows.Forms.Button btnLihatGrading;
        private System.Windows.Forms.Button btnLihatProses;
        private System.Windows.Forms.Button btnLihatQC;
        private System.Windows.Forms.Button btnLogout;
    }
}