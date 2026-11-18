namespace EDATRACK.View
{
    partial class FormGrading
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
            this.dtpTanggalGrading = new System.Windows.Forms.DateTimePicker();
            this.lblJumlahSampel = new System.Windows.Forms.Label();
            this.txtJumlahSampel = new System.Windows.Forms.TextBox();
            this.lblKondisiWarna = new System.Windows.Forms.Label();
            this.cboKondisiWarna = new System.Windows.Forms.ComboBox();
            this.lblKondisiFisik = new System.Windows.Forms.Label();
            this.cboKondisiFisik = new System.Windows.Forms.ComboBox();
            this.lblUkuranPolong = new System.Windows.Forms.Label();
            this.txtUkuranPolong = new System.Windows.Forms.TextBox();
            this.lblGradeA = new System.Windows.Forms.Label();
            this.txtGradeA = new System.Windows.Forms.TextBox();
            this.lblGradeB = new System.Windows.Forms.Label();
            this.txtGradeB = new System.Windows.Forms.TextBox();
            this.lblReject = new System.Windows.Forms.Label();
            this.txtReject = new System.Windows.Forms.TextBox();
            this.btnSimpanGrading = new System.Windows.Forms.Button();
            this.dgvGrading = new System.Windows.Forms.DataGridView();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrading)).BeginInit();
            this.SuspendLayout();

            this.Text = "Grading dan Klasifikasi Mutu";
            this.Size = new System.Drawing.Size(900, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.FormGrading_Load);

            this.lblJudul.Text = "Grading dan Klasifikasi Mutu";
            this.lblJudul.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblJudul.Location = new System.Drawing.Point(230, 15);
            this.lblJudul.Size = new System.Drawing.Size(400, 30);
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelInput.Name = "panelInput";
            this.panelInput.Location = new System.Drawing.Point(15, 55);
            this.panelInput.Size = new System.Drawing.Size(290, 580);
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.BackColor = System.Drawing.Color.White;

            int lX = 10, iX = 10, gap = 50;
            int y = 10;

            void TambahLabel(System.Windows.Forms.Label lbl, string teks)
            {
                lbl.Text = teks;
                lbl.Location = new System.Drawing.Point(lX, y);
                lbl.Size = new System.Drawing.Size(265, 18);
                y += 20;
            }

            void TambahInput(System.Windows.Forms.Control ctrl)
            {
                ctrl.Location = new System.Drawing.Point(iX, y);
                ctrl.Size = new System.Drawing.Size(265, 23);
                y += gap;
            }

            TambahLabel(this.lblBatch, "Kode Batch *");
            this.cboBatch.Name = "cboBatch";
            this.cboBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboBatch);

            TambahLabel(this.lblTanggal, "Tanggal Grading *");
            this.dtpTanggalGrading.Name = "dtpTanggalGrading";
            this.dtpTanggalGrading.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            TambahInput(this.dtpTanggalGrading);

            TambahLabel(this.lblJumlahSampel, "Jumlah Sampel *");
            this.txtJumlahSampel.Name = "txtJumlahSampel";
            TambahInput(this.txtJumlahSampel);

            TambahLabel(this.lblKondisiWarna, "Kondisi Warna *");
            this.cboKondisiWarna.Name = "cboKondisiWarna";
            this.cboKondisiWarna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboKondisiWarna);

            TambahLabel(this.lblKondisiFisik, "Kondisi Fisik *");
            this.cboKondisiFisik.Name = "cboKondisiFisik";
            this.cboKondisiFisik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TambahInput(this.cboKondisiFisik);

            TambahLabel(this.lblUkuranPolong, "Ukuran Polong");
            this.txtUkuranPolong.Name = "txtUkuranPolong";
            TambahInput(this.txtUkuranPolong);

            TambahLabel(this.lblGradeA, "Berat Grade A (kg) *");
            this.txtGradeA.Name = "txtGradeA";
            TambahInput(this.txtGradeA);

            TambahLabel(this.lblGradeB, "Berat Grade B (kg) *");
            this.txtGradeB.Name = "txtGradeB";
            TambahInput(this.txtGradeB);

            TambahLabel(this.lblReject, "Berat Reject (kg) *");
            this.txtReject.Name = "txtReject";
            TambahInput(this.txtReject);

            this.btnSimpanGrading.Text = "Simpan Grading";
            this.btnSimpanGrading.Location = new System.Drawing.Point(iX, y);
            this.btnSimpanGrading.Size = new System.Drawing.Size(265, 35);
            this.btnSimpanGrading.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSimpanGrading.ForeColor = System.Drawing.Color.White;
            this.btnSimpanGrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanGrading.Click += new System.EventHandler(this.btnSimpanGrading_Click);

            this.panelInput.Controls.Add(this.lblBatch);
            this.panelInput.Controls.Add(this.cboBatch);
            this.panelInput.Controls.Add(this.lblTanggal);
            this.panelInput.Controls.Add(this.dtpTanggalGrading);
            this.panelInput.Controls.Add(this.lblJumlahSampel);
            this.panelInput.Controls.Add(this.txtJumlahSampel);
            this.panelInput.Controls.Add(this.lblKondisiWarna);
            this.panelInput.Controls.Add(this.cboKondisiWarna);
            this.panelInput.Controls.Add(this.lblKondisiFisik);
            this.panelInput.Controls.Add(this.cboKondisiFisik);
            this.panelInput.Controls.Add(this.lblUkuranPolong);
            this.panelInput.Controls.Add(this.txtUkuranPolong);
            this.panelInput.Controls.Add(this.lblGradeA);
            this.panelInput.Controls.Add(this.txtGradeA);
            this.panelInput.Controls.Add(this.lblGradeB);
            this.panelInput.Controls.Add(this.txtGradeB);
            this.panelInput.Controls.Add(this.lblReject);
            this.panelInput.Controls.Add(this.txtReject);
            this.panelInput.Controls.Add(this.btnSimpanGrading);

            this.dgvGrading.Name = "dgvGrading";
            this.dgvGrading.Location = new System.Drawing.Point(320, 55);
            this.dgvGrading.Size = new System.Drawing.Size(555, 580);
            this.dgvGrading.AllowUserToAddRows = false;
            this.dgvGrading.ReadOnly = true;
            this.dgvGrading.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrading.BackgroundColor = System.Drawing.Color.White;
            this.dgvGrading.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.dgvGrading);

            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrading)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.ComboBox cboBatch;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.DateTimePicker dtpTanggalGrading;
        private System.Windows.Forms.Label lblJumlahSampel;
        private System.Windows.Forms.TextBox txtJumlahSampel;
        private System.Windows.Forms.Label lblKondisiWarna;
        private System.Windows.Forms.ComboBox cboKondisiWarna;
        private System.Windows.Forms.Label lblKondisiFisik;
        private System.Windows.Forms.ComboBox cboKondisiFisik;
        private System.Windows.Forms.Label lblUkuranPolong;
        private System.Windows.Forms.TextBox txtUkuranPolong;
        private System.Windows.Forms.Label lblGradeA;
        private System.Windows.Forms.TextBox txtGradeA;
        private System.Windows.Forms.Label lblGradeB;
        private System.Windows.Forms.TextBox txtGradeB;
        private System.Windows.Forms.Label lblReject;
        private System.Windows.Forms.TextBox txtReject;
        private System.Windows.Forms.Button btnSimpanGrading;
        private System.Windows.Forms.DataGridView dgvGrading;
    }
}