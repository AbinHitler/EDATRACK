namespace EDATRACK.View
{
    partial class FormDashboardPekerja
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
            lblSubjudul = new Label();
            lblSelamatDatang = new Label();
            btnPenerimaan = new Button();
            btnGrading = new Button();
            btnProsesProduksi = new Button();
            btnQC = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblSubjudul
            // 
            lblSubjudul.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblSubjudul.ForeColor = Color.DarkGreen;
            lblSubjudul.Location = new Point(523, 113);
            lblSubjudul.Name = "lblSubjudul";
            lblSubjudul.Size = new Size(200, 35);
            lblSubjudul.TabIndex = 0;
            lblSubjudul.Text = "EDATRACK";
            lblSubjudul.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSelamatDatang
            // 
            lblSelamatDatang.Font = new Font("Arial", 10F);
            lblSelamatDatang.Location = new Point(423, 158);
            lblSelamatDatang.Name = "lblSelamatDatang";
            lblSelamatDatang.Size = new Size(380, 25);
            lblSelamatDatang.TabIndex = 1;
            lblSelamatDatang.Text = "Selamat datang, Pekerja";
            lblSelamatDatang.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPenerimaan
            // 
            btnPenerimaan.BackColor = Color.DarkGreen;
            btnPenerimaan.FlatStyle = FlatStyle.Flat;
            btnPenerimaan.Font = new Font("Arial", 10F);
            btnPenerimaan.ForeColor = Color.White;
            btnPenerimaan.Location = new Point(438, 203);
            btnPenerimaan.Name = "btnPenerimaan";
            btnPenerimaan.Size = new Size(360, 45);
            btnPenerimaan.TabIndex = 2;
            btnPenerimaan.Text = "Penerimaan Bahan Baku";
            btnPenerimaan.UseVisualStyleBackColor = false;
            btnPenerimaan.Click += btnPenerimaan_Click;
            // 
            // btnGrading
            // 
            btnGrading.BackColor = Color.DarkGreen;
            btnGrading.FlatStyle = FlatStyle.Flat;
            btnGrading.Font = new Font("Arial", 10F);
            btnGrading.ForeColor = Color.White;
            btnGrading.Location = new Point(438, 258);
            btnGrading.Name = "btnGrading";
            btnGrading.Size = new Size(360, 45);
            btnGrading.TabIndex = 3;
            btnGrading.Text = "Grading dan Klasifikasi Mutu";
            btnGrading.UseVisualStyleBackColor = false;
            btnGrading.Click += btnGrading_Click;
            // 
            // btnProsesProduksi
            // 
            btnProsesProduksi.BackColor = Color.DarkGreen;
            btnProsesProduksi.FlatStyle = FlatStyle.Flat;
            btnProsesProduksi.Font = new Font("Arial", 10F);
            btnProsesProduksi.ForeColor = Color.White;
            btnProsesProduksi.Location = new Point(438, 313);
            btnProsesProduksi.Name = "btnProsesProduksi";
            btnProsesProduksi.Size = new Size(360, 45);
            btnProsesProduksi.TabIndex = 4;
            btnProsesProduksi.Text = "Monitoring Proses Produksi";
            btnProsesProduksi.UseVisualStyleBackColor = false;
            btnProsesProduksi.Click += btnProsesProduksi_Click;
            // 
            // btnQC
            // 
            btnQC.BackColor = Color.DarkGreen;
            btnQC.FlatStyle = FlatStyle.Flat;
            btnQC.Font = new Font("Arial", 10F);
            btnQC.ForeColor = Color.White;
            btnQC.Location = new Point(438, 368);
            btnQC.Name = "btnQC";
            btnQC.Size = new Size(360, 45);
            btnQC.TabIndex = 5;
            btnQC.Text = "Quality Control dan Pengemasan";
            btnQC.UseVisualStyleBackColor = false;
            btnQC.Click += btnQC_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Firebrick;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Arial", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(438, 438);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(360, 45);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // FormDashboardPekerja
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1182, 673);
            Controls.Add(lblSubjudul);
            Controls.Add(lblSelamatDatang);
            Controls.Add(btnPenerimaan);
            Controls.Add(btnGrading);
            Controls.Add(btnProsesProduksi);
            Controls.Add(btnQC);
            Controls.Add(btnLogout);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormDashboardPekerja";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EDATRACK - Dashboard Pekerja";
            Load += FormDashboardPekerja_Load;
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblSubjudul;
        private System.Windows.Forms.Label lblSelamatDatang;
        private System.Windows.Forms.Button btnPenerimaan;
        private System.Windows.Forms.Button btnGrading;
        private System.Windows.Forms.Button btnProsesProduksi;
        private System.Windows.Forms.Button btnQC;
        private System.Windows.Forms.Button btnLogout;
    }
}