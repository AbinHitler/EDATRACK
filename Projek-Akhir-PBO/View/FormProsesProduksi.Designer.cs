namespace EDATRACK.View
{
    partial class FormProsesProduksi
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
            this.panelInput = new System.Windows.Forms.Panel();
            this.lblBatch = new System.Windows.Forms.Label();
            this.cboBatch = new System.Windows.Forms.ComboBox();
            this.lblJenisProses = new System.Windows.Forms.Label();
            this.cboJenisProses = new System.Windows.Forms.ComboBox();
            this.lblSuhu = new System.Windows.Forms.Label();
            this.txtSuhu = new System.Windows.Forms.TextBox();
            this.lblDurasi = new System.Windows.Forms.Label();
            this.txtDurasi = new System.Windows.Forms.TextBox();
            this.lblWaktuMulai = new System.Windows.Forms.Label();
            this.dtpWaktuMulai = new System.Windows.Forms.DateTimePicker();
            this.lblWaktuSelesai = new System.Windows.Forms.Label();
            this.dtpWaktuSelesai = new System.Windows.Forms.DateTimePicker();
            this.btnSimpanProses = new System.Windows.Forms.Button();
            this.dgvProses = new System.Windows.Forms.DataGridView();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProses)).BeginInit();
            this.SuspendLayout();

            // Form
            this.Text = "Monitoring Proses Produksi";
            this.Size = new System.Drawing.Size(900, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.FormProsesProduksi_Load);

            // lblJudul
            this.lblJudul.Text = "Monitoring Proses Produksi";
            this.lblJudul.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblJudul.Location = new System.Drawing.Point(230, 15);
            this.lblJudul.Size = new System.Drawing.Size(400, 30);
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // panelInput
            this.panelInput.Name = "panelInput";
            this.panelInput.Location = new System.Drawing.Point(15, 55);
            this.panelInput.Size = new System.Drawing.Size(280, 500);
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.BackColor = System.Drawing.Color.White;

            int lX = 10, iX = 10, gap = 55;
            int y = 10;

            void TambahLabel(System.Windows.Forms.Label lbl, string teks)
            {
                lbl.Text = teks;
                lbl.Location = new System.Drawing.Point(lX, y);
                lbl.Size = new System.Drawing.Size(255, 20);
                y += 22;
            }

            void TambahInput(System.Windows.Forms.Control ctrl)
            {
                ctrl.Location = new System.Drawing.Point(iX, y);
                ctrl.Size = new System.Drawing.Size(255, 23);
                y += gap;
            }

            TambahLabel(this.lblBatch, "Kode Batch *");
            this.cboBatch.Name = "cboBatch";
            this.cboBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBatch.SelectedIndexChanged += new System.EventHandler(this.cboBatch_SelectedIndexChanged);
            TambahInput(this.cboBatch);

            TambahLabel(this.lblJenisProses, "Jenis Proses *");
            this.cboJenisProses.Name = "cboJenisProses";
            this.cboJenisProses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboJenisProses);

            TambahLabel(this.lblSuhu, "Suhu (°C) *");
            this.txtSuhu.Name = "txtSuhu";
            TambahInput(this.txtSuhu);

            TambahLabel(this.lblDurasi, "Durasi (menit) *");
            this.txtDurasi.Name = "txtDurasi";
            this.txtDurasi.TextChanged += new System.EventHandler(this.InputWaktu_Changed);
            TambahInput(this.txtDurasi);

            TambahLabel(this.lblWaktuMulai, "Waktu Mulai *");
            this.dtpWaktuMulai.Name = "dtpWaktuMulai";
            this.dtpWaktuMulai.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWaktuMulai.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpWaktuMulai.ValueChanged += new System.EventHandler(this.InputWaktu_Changed);
            TambahInput(this.dtpWaktuMulai);

            TambahLabel(this.lblWaktuSelesai, "Waktu Selesai (Otomatis)");
            this.dtpWaktuSelesai.Name = "dtpWaktuSelesai";
            this.dtpWaktuSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWaktuSelesai.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpWaktuSelesai.Enabled = false;
            TambahInput(this.dtpWaktuSelesai);

            this.btnSimpanProses.Text = "Simpan Proses";
            this.btnSimpanProses.Location = new System.Drawing.Point(iX, y);
            this.btnSimpanProses.Size = new System.Drawing.Size(255, 35);
            this.btnSimpanProses.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSimpanProses.ForeColor = System.Drawing.Color.White;
            this.btnSimpanProses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanProses.Click += new System.EventHandler(this.btnSimpanProses_Click);

            this.panelInput.Controls.Add(this.lblBatch);
            this.panelInput.Controls.Add(this.cboBatch);
            this.panelInput.Controls.Add(this.lblJenisProses);
            this.panelInput.Controls.Add(this.cboJenisProses);
            this.panelInput.Controls.Add(this.lblSuhu);
            this.panelInput.Controls.Add(this.txtSuhu);
            this.panelInput.Controls.Add(this.lblDurasi);
            this.panelInput.Controls.Add(this.txtDurasi);
            this.panelInput.Controls.Add(this.lblWaktuMulai);
            this.panelInput.Controls.Add(this.dtpWaktuMulai);
            this.panelInput.Controls.Add(this.lblWaktuSelesai);
            this.panelInput.Controls.Add(this.dtpWaktuSelesai);
            this.panelInput.Controls.Add(this.btnSimpanProses);

            // dgvProses
            this.dgvProses.Name = "dgvProses";
            this.dgvProses.Location = new System.Drawing.Point(310, 55);
            this.dgvProses.Size = new System.Drawing.Size(565, 500);
            this.dgvProses.AllowUserToAddRows = false;
            this.dgvProses.ReadOnly = true;
            this.dgvProses.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProses.BackgroundColor = System.Drawing.Color.White;
            this.dgvProses.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.dgvProses);

            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProses)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.ComboBox cboBatch;
        private System.Windows.Forms.Label lblJenisProses;
        private System.Windows.Forms.ComboBox cboJenisProses;
        private System.Windows.Forms.Label lblSuhu;
        private System.Windows.Forms.TextBox txtSuhu;
        private System.Windows.Forms.Label lblDurasi;
        private System.Windows.Forms.TextBox txtDurasi;
        private System.Windows.Forms.Label lblWaktuMulai;
        private System.Windows.Forms.DateTimePicker dtpWaktuMulai;
        private System.Windows.Forms.Label lblWaktuSelesai;
        private System.Windows.Forms.DateTimePicker dtpWaktuSelesai;
        private System.Windows.Forms.Button btnSimpanProses;
        private System.Windows.Forms.DataGridView dgvProses;
    }
}