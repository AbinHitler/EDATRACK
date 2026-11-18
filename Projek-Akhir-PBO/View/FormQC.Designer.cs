namespace EDATRACK.View
{
    partial class FormQC
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
            this.lblTanggal = new System.Windows.Forms.Label();
            this.dtpTanggalQC = new System.Windows.Forms.DateTimePicker();
            this.lblKondisiFisik = new System.Windows.Forms.Label();
            this.cboKondisiFisik = new System.Windows.Forms.ComboBox();
            this.lblKondisiWarna = new System.Windows.Forms.Label();
            this.cboKondisiWarna = new System.Windows.Forms.ComboBox();
            this.lblKondisiKemasan = new System.Windows.Forms.Label();
            this.cboKondisiKemasan = new System.Windows.Forms.ComboBox();
            this.lblHasilQC = new System.Windows.Forms.Label();
            this.cboHasilQC = new System.Windows.Forms.ComboBox();
            this.lblCatatan = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblBeratKemasan = new System.Windows.Forms.Label();
            this.txtBeratKemasan = new System.Windows.Forms.TextBox();
            this.lblJumlahKemasan = new System.Windows.Forms.Label();
            this.txtJumlahKemasan = new System.Windows.Forms.TextBox();
            this.lblJenisKemasan = new System.Windows.Forms.Label();
            this.txtJenisKemasan = new System.Windows.Forms.TextBox();
            this.btnSimpanQC = new System.Windows.Forms.Button();
            this.dgvQC = new System.Windows.Forms.DataGridView();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQC)).BeginInit();
            this.SuspendLayout();

            // Form
            this.Text = "Quality Control dan Pengemasan";
            this.Size = new System.Drawing.Size(960, 720);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.FormQC_Load);

            // lblJudul
            this.lblJudul.Text = "Quality Control dan Pengemasan";
            this.lblJudul.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblJudul.Location = new System.Drawing.Point(250, 15);
            this.lblJudul.Size = new System.Drawing.Size(420, 30);
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // panelInput
            this.panelInput.Name = "panelInput";
            this.panelInput.Location = new System.Drawing.Point(15, 55);
            this.panelInput.Size = new System.Drawing.Size(290, 610);
            this.panelInput.AutoScroll = true;
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.BackColor = System.Drawing.Color.White;

            int lX = 10, iX = 10, gap = 48;
            int y = 10;

            void TambahLabel(System.Windows.Forms.Label lbl, string teks)
            {
                lbl.Text = teks;
                lbl.Location = new System.Drawing.Point(lX, y);
                lbl.Size = new System.Drawing.Size(255, 18);
                y += 20;
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
            TambahInput(this.cboBatch);

            TambahLabel(this.lblTanggal, "Tanggal QC *");
            this.dtpTanggalQC.Name = "dtpTanggalQC";
            this.dtpTanggalQC.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            TambahInput(this.dtpTanggalQC);

            TambahLabel(this.lblKondisiFisik, "Kondisi Fisik *");
            this.cboKondisiFisik.Name = "cboKondisiFisik";
            this.cboKondisiFisik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboKondisiFisik);

            TambahLabel(this.lblKondisiWarna, "Kondisi Warna *");
            this.cboKondisiWarna.Name = "cboKondisiWarna";
            this.cboKondisiWarna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboKondisiWarna);

            TambahLabel(this.lblKondisiKemasan, "Kondisi Kemasan *");
            this.cboKondisiKemasan.Name = "cboKondisiKemasan";
            this.cboKondisiKemasan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboKondisiKemasan);

            TambahLabel(this.lblHasilQC, "Hasil QC *");
            this.cboHasilQC.Name = "cboHasilQC";
            this.cboHasilQC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHasilQC.SelectedIndexChanged += new System.EventHandler(this.cboHasilQC_SelectedIndexChanged);
            TambahInput(this.cboHasilQC);

            TambahLabel(this.lblCatatan, "Catatan QC");
            this.txtCatatan.Name = "txtCatatan";
            TambahInput(this.txtCatatan);

            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Text = "Data Pengemasan (Jika Lulus QC)";
            this.lblSeparator.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            this.lblSeparator.Location = new System.Drawing.Point(lX, y);
            this.lblSeparator.Size = new System.Drawing.Size(255, 20);
            y += 26;

            TambahLabel(this.lblBeratKemasan, "Berat per Kemasan (kg)");
            this.txtBeratKemasan.Name = "txtBeratKemasan";
            TambahInput(this.txtBeratKemasan);

            TambahLabel(this.lblJumlahKemasan, "Jumlah Kemasan");
            this.txtJumlahKemasan.Name = "txtJumlahKemasan";
            TambahInput(this.txtJumlahKemasan);

            TambahLabel(this.lblJenisKemasan, "Jenis Kemasan");
            this.txtJenisKemasan.Name = "txtJenisKemasan";
            TambahInput(this.txtJenisKemasan);

            this.btnSimpanQC.Text = "Simpan QC & Pengemasan";
            this.btnSimpanQC.Location = new System.Drawing.Point(iX, y);
            this.btnSimpanQC.Size = new System.Drawing.Size(255, 35);
            this.btnSimpanQC.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSimpanQC.ForeColor = System.Drawing.Color.White;
            this.btnSimpanQC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanQC.Click += new System.EventHandler(this.btnSimpanQC_Click);

            this.panelInput.Controls.Add(this.lblBatch);
            this.panelInput.Controls.Add(this.cboBatch);
            this.panelInput.Controls.Add(this.lblTanggal);
            this.panelInput.Controls.Add(this.dtpTanggalQC);
            this.panelInput.Controls.Add(this.lblKondisiFisik);
            this.panelInput.Controls.Add(this.cboKondisiFisik);
            this.panelInput.Controls.Add(this.lblKondisiWarna);
            this.panelInput.Controls.Add(this.cboKondisiWarna);
            this.panelInput.Controls.Add(this.lblKondisiKemasan);
            this.panelInput.Controls.Add(this.cboKondisiKemasan);
            this.panelInput.Controls.Add(this.lblHasilQC);
            this.panelInput.Controls.Add(this.cboHasilQC);
            this.panelInput.Controls.Add(this.lblCatatan);
            this.panelInput.Controls.Add(this.txtCatatan);
            this.panelInput.Controls.Add(this.lblSeparator);
            this.panelInput.Controls.Add(this.lblBeratKemasan);
            this.panelInput.Controls.Add(this.txtBeratKemasan);
            this.panelInput.Controls.Add(this.lblJumlahKemasan);
            this.panelInput.Controls.Add(this.txtJumlahKemasan);
            this.panelInput.Controls.Add(this.lblJenisKemasan);
            this.panelInput.Controls.Add(this.txtJenisKemasan);
            this.panelInput.Controls.Add(this.btnSimpanQC);

            // dgvQC
            this.dgvQC.Name = "dgvQC";
            this.dgvQC.Location = new System.Drawing.Point(320, 55);
            this.dgvQC.Size = new System.Drawing.Size(615, 610);
            this.dgvQC.AllowUserToAddRows = false;
            this.dgvQC.ReadOnly = true;
            this.dgvQC.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQC.BackgroundColor = System.Drawing.Color.White;
            this.dgvQC.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.dgvQC);

            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQC)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.ComboBox cboBatch;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.DateTimePicker dtpTanggalQC;
        private System.Windows.Forms.Label lblKondisiFisik;
        private System.Windows.Forms.ComboBox cboKondisiFisik;
        private System.Windows.Forms.Label lblKondisiWarna;
        private System.Windows.Forms.ComboBox cboKondisiWarna;
        private System.Windows.Forms.Label lblKondisiKemasan;
        private System.Windows.Forms.ComboBox cboKondisiKemasan;
        private System.Windows.Forms.Label lblHasilQC;
        private System.Windows.Forms.ComboBox cboHasilQC;
        private System.Windows.Forms.Label lblCatatan;
        private System.Windows.Forms.TextBox txtCatatan;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblBeratKemasan;
        private System.Windows.Forms.TextBox txtBeratKemasan;
        private System.Windows.Forms.Label lblJumlahKemasan;
        private System.Windows.Forms.TextBox txtJumlahKemasan;
        private System.Windows.Forms.Label lblJenisKemasan;
        private System.Windows.Forms.TextBox txtJenisKemasan;
        private System.Windows.Forms.Button btnSimpanQC;
        private System.Windows.Forms.DataGridView dgvQC;
    }
}