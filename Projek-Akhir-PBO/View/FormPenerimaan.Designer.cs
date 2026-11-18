namespace EDATRACK.View
{
    partial class FormPenerimaan
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
            this.lblKodeBatch = new System.Windows.Forms.Label();
            this.txtKodeBatch = new System.Windows.Forms.TextBox();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.lblAsal = new System.Windows.Forms.Label();
            this.txtAsal = new System.Windows.Forms.TextBox();
            this.lblBerat = new System.Windows.Forms.Label();
            this.txtBerat = new System.Windows.Forms.TextBox();
            this.lblKondisi = new System.Windows.Forms.Label();
            this.cboKondisi = new System.Windows.Forms.ComboBox();
            this.lblKeterangan = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dgvPenerimaan = new System.Windows.Forms.DataGridView();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPenerimaan)).BeginInit();
            this.SuspendLayout();

            
            this.Text = "Penerimaan Bahan Baku";
            this.Size = new System.Drawing.Size(850, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.FormPenerimaan_Load);

            
            this.lblJudul.Text = "Penerimaan Bahan Baku";
            this.lblJudul.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblJudul.Location = new System.Drawing.Point(250, 15);
            this.lblJudul.Size = new System.Drawing.Size(350, 30);
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            
            this.panelInput.Name = "panelInput";
            this.panelInput.Location = new System.Drawing.Point(15, 55);
            this.panelInput.Size = new System.Drawing.Size(280, 490);
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.BackColor = System.Drawing.Color.White;

            int labelX = 10, inputX = 10, gap = 55;
            int y = 10;

            
            this.lblKodeBatch.Text = "Kode Batch *";
            this.lblKodeBatch.Location = new System.Drawing.Point(labelX, y);
            this.lblKodeBatch.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.txtKodeBatch.Name = "txtKodeBatch";
            this.txtKodeBatch.Location = new System.Drawing.Point(inputX, y);
            this.txtKodeBatch.Size = new System.Drawing.Size(255, 23);
            y += gap;

            
            this.lblTanggal.Text = "Tanggal Penerimaan *";
            this.lblTanggal.Location = new System.Drawing.Point(labelX, y);
            this.lblTanggal.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Location = new System.Drawing.Point(inputX, y);
            this.dtpTanggal.Size = new System.Drawing.Size(255, 23);
            this.dtpTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            y += gap;

            
            this.lblAsal.Text = "Asal Bahan Baku *";
            this.lblAsal.Location = new System.Drawing.Point(labelX, y);
            this.lblAsal.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.txtAsal.Name = "txtAsal";
            this.txtAsal.Location = new System.Drawing.Point(inputX, y);
            this.txtAsal.Size = new System.Drawing.Size(255, 23);
            y += gap;

            
            this.lblBerat.Text = "Berat (kg) *";
            this.lblBerat.Location = new System.Drawing.Point(labelX, y);
            this.lblBerat.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.txtBerat.Name = "txtBerat";
            this.txtBerat.Location = new System.Drawing.Point(inputX, y);
            this.txtBerat.Size = new System.Drawing.Size(255, 23);
            y += gap;

            
            this.lblKondisi.Text = "Kondisi Awal *";
            this.lblKondisi.Location = new System.Drawing.Point(labelX, y);
            this.lblKondisi.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.cboKondisi.Name = "cboKondisi";
            this.cboKondisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKondisi.Location = new System.Drawing.Point(inputX, y);
            this.cboKondisi.Size = new System.Drawing.Size(255, 23);
            y += gap;

            
            this.lblKeterangan.Text = "Keterangan";
            this.lblKeterangan.Location = new System.Drawing.Point(labelX, y);
            this.lblKeterangan.Size = new System.Drawing.Size(260, 20);
            y += 22;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Location = new System.Drawing.Point(inputX, y);
            this.txtKeterangan.Size = new System.Drawing.Size(255, 23);
            y += gap;

            
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.Location = new System.Drawing.Point(inputX, y);
            this.btnSimpan.Size = new System.Drawing.Size(255, 35);
            this.btnSimpan.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSimpan.ForeColor = System.Drawing.Color.White;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            this.panelInput.Controls.Add(this.lblKodeBatch);
            this.panelInput.Controls.Add(this.txtKodeBatch);
            this.panelInput.Controls.Add(this.lblTanggal);
            this.panelInput.Controls.Add(this.dtpTanggal);
            this.panelInput.Controls.Add(this.lblAsal);
            this.panelInput.Controls.Add(this.txtAsal);
            this.panelInput.Controls.Add(this.lblBerat);
            this.panelInput.Controls.Add(this.txtBerat);
            this.panelInput.Controls.Add(this.lblKondisi);
            this.panelInput.Controls.Add(this.cboKondisi);
            this.panelInput.Controls.Add(this.lblKeterangan);
            this.panelInput.Controls.Add(this.txtKeterangan);
            this.panelInput.Controls.Add(this.btnSimpan);

            
            this.dgvPenerimaan.Name = "dgvPenerimaan";
            this.dgvPenerimaan.Location = new System.Drawing.Point(310, 55);
            this.dgvPenerimaan.Size = new System.Drawing.Size(510, 490);
            this.dgvPenerimaan.AllowUserToAddRows = false;
            this.dgvPenerimaan.ReadOnly = true;
            this.dgvPenerimaan.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPenerimaan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPenerimaan.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.dgvPenerimaan);

            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPenerimaan)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblKodeBatch;
        private System.Windows.Forms.TextBox txtKodeBatch;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.Label lblAsal;
        private System.Windows.Forms.TextBox txtAsal;
        private System.Windows.Forms.Label lblBerat;
        private System.Windows.Forms.TextBox txtBerat;
        private System.Windows.Forms.Label lblKondisi;
        private System.Windows.Forms.ComboBox cboKondisi;
        private System.Windows.Forms.Label lblKeterangan;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DataGridView dgvPenerimaan;
    }
}