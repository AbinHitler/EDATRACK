using EDATRACK.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace EDATRACK.View
{
    internal static class EDATrackTheme
    {
        private static readonly Color Green = Color.FromArgb(8, 117, 27);
        private static readonly Color GreenDark = Color.FromArgb(0, 98, 15);
        private static readonly Color Cream = Color.FromArgb(247, 246, 241);
        private static readonly Color Ink = Color.FromArgb(38, 51, 42);
        private static readonly Color Muted = Color.FromArgb(104, 116, 107);
        private static readonly Color Line = Color.FromArgb(232, 236, 229);
        private static readonly Color Red = Color.FromArgb(185, 32, 37);
        private static readonly Color Blue = Color.FromArgb(61, 121, 198);
        private static readonly Color Orange = Color.FromArgb(240, 160, 0);
        private static readonly Color Teal = Color.FromArgb(21, 156, 155);
        private static readonly Color Purple = Color.FromArgb(120, 96, 202);

        private sealed class FieldSpec
        {
            public string LabelName { get; }
            public string InputName { get; }

            public FieldSpec(string labelName, string inputName)
            {
                LabelName = labelName;
                InputName = inputName;
            }
        }

        public static void Apply(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));

            switch (form.GetType().Name)
            {
                case nameof(FormLogin):
                    BuildLogin(form);
                    break;
                case nameof(FormDashboardAdmin):
                    BuildDashboardAdmin(form);
                    break;
                case nameof(FormDashboardPekerja):
                    BuildDashboardPekerja(form);
                    break;
                case nameof(FormKelolaAkun):
                    BuildKelolaAkun(form);
                    break;
                case nameof(FormPenerimaan):
                    BuildPenerimaan(form);
                    break;
                case nameof(FormGrading):
                    BuildGrading(form);
                    break;
                case nameof(FormProsesProduksi):
                    BuildProsesProduksi(form);
                    break;
                case nameof(FormQC):
                    BuildQC(form);
                    break;
            }
        }

        private static T ControlOf<T>(Form form, string fieldName) where T : Control
        {
            FieldInfo? field = form.GetType().GetField(
                fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (field?.GetValue(form) is T control)
                return control;

            throw new InvalidOperationException(
                $"Kontrol '{fieldName}' tidak ditemukan pada {form.GetType().Name}. " +
                "Pastikan nama field di file Designer.cs tidak diubah.");
        }

        private static void ResetLayout(Form form, params Control[] controls)
        {
            foreach (Control control in controls)
                control.Parent?.Controls.Remove(control);

            form.Controls.Clear();
            form.SuspendLayout();
            form.Controls.Clear();
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            form.BackColor = Cream;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.MinimizeBox = true;
            form.MinimumSize = new Size(1050, 680);
            form.ClientSize = new Size(1380, 820);
            form.WindowState = FormWindowState.Maximized;
        }

        private static TableLayoutPanel CreatePageRoot(Form form, string title, Color accent, Label titleLabel)
        {
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.White,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 4));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Panel accentLine = new Panel { Dock = DockStyle.Fill, BackColor = accent };
            Panel header = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            titleLabel.ForeColor = Green;
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            titleLabel.Location = new Point(22, 7);
            titleLabel.Size = new Size(780, 42);
            titleLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            Button back = new Button
            {
                Text = "←  Dashboard",
                Size = new Size(132, 34),
                Location = new Point(0, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = accent,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            back.FlatAppearance.BorderColor = Line;
            back.FlatAppearance.MouseOverBackColor = Cream;
            back.Click += (_, _) => form.Close();
            header.Controls.Add(titleLabel);
            header.Controls.Add(back);
            header.Resize += (_, _) => back.Location = new Point(header.ClientSize.Width - back.Width - 20, 12);

            root.Controls.Add(accentLine, 0, 0);
            root.Controls.Add(header, 0, 1);
            form.Controls.Add(root);
            return root;
        }

        private static void BuildDataPage(
            Form form,
            string title,
            Color accent,
            Panel inputPanel,
            DataGridView grid,
            Button saveButton,
            List<FieldSpec> fields,
            Label? sectionLabel = null,
            int sectionAfterField = -1)
        {
            ResetLayout(form, inputPanel, grid, saveButton);
            form.Text = "EDATRACK - " + title;
            Label titleLabel = ControlOf<Label>(form, "lblJudul");
            TableLayoutPanel root = CreatePageRoot(form, title, accent, titleLabel);

            TableLayoutPanel body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(12),
                BackColor = Color.White,
                Margin = Padding.Empty
            };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            inputPanel.Controls.Clear();
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.AutoScroll = true;
            inputPanel.BackColor = Cream;
            inputPanel.BorderStyle = BorderStyle.None;
            inputPanel.Padding = new Padding(10);

            Panel card = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(14),
                Margin = Padding.Empty
            };
            inputPanel.Controls.Add(card);

            int y = 16;
            int fieldCount = 0;
            foreach (FieldSpec field in fields)
            {
                if (sectionLabel != null && fieldCount == sectionAfterField)
                {
                    sectionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    sectionLabel.ForeColor = accent;
                    sectionLabel.Location = new Point(16, y + 4);
                    sectionLabel.Size = new Size(300, 24);
                    sectionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    card.Controls.Add(sectionLabel);
                    y += 34;
                }

                Label label = ControlOf<Label>(form, field.LabelName);
                Control input = ControlOf<Control>(form, field.InputName);
                input.Name = field.InputName;
                StyleField(label, input, card, ref y);
                fieldCount++;
            }

            StyleActionButton(saveButton, accent);
            saveButton.Location = new Point(16, y + 8);
            saveButton.Size = new Size(300, 40);
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            card.Controls.Add(saveButton);
            y += 64;
            card.Height = y + 8;

            card.Resize += (_, _) => ResizeCardControls(card);
            inputPanel.Resize += (_, _) => card.Width = Math.Max(300, inputPanel.ClientSize.Width - 22);
            card.Width = Math.Max(300, inputPanel.ClientSize.Width - 22);

            Panel tableSide = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(8, 0, 0, 0)
            };
            StyleGrid(grid);
            grid.Dock = DockStyle.Fill;
            tableSide.Controls.Add(grid);

            body.Controls.Add(inputPanel, 0, 0);
            body.Controls.Add(tableSide, 1, 0);
            root.Controls.Add(body, 0, 2);

            Action updateColumns = () =>
            {
                bool showInput = inputPanel.Visible;
                body.ColumnStyles[0].Width = showInput ? 35 : 0;
                body.ColumnStyles[1].Width = showInput ? 65 : 100;
                body.SetColumn(tableSide, showInput ? 1 : 0);
                body.SetColumnSpan(tableSide, showInput ? 1 : 2);
            };
            inputPanel.VisibleChanged += (_, _) => updateColumns();
            updateColumns();

            form.AcceptButton = saveButton;
            form.ResumeLayout(true);
        }

        private static void StyleField(Label label, Control input, Panel card, ref int y)
        {
            label.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            label.ForeColor = Muted;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Location = new Point(16, y);
            label.Size = new Size(300, 19);
            label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            card.Controls.Add(label);

            bool multiline = input is TextBox &&
                (input.Name == "txtKeterangan" || input.Name == "txtCatatan");
            int inputHeight = multiline ? 62 : 32;
            input.Location = new Point(16, y + 21);
            input.Size = new Size(300, inputHeight);
            input.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input.Font = new Font("Segoe UI", 9F);
            input.ForeColor = Ink;
            input.BackColor = Color.White;

            if (input is TextBox textBox)
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
                if (multiline)
                {
                    textBox.Multiline = true;
                    textBox.ScrollBars = ScrollBars.Vertical;
                }
            }
            else if (input is ComboBox combo)
            {
                combo.FlatStyle = FlatStyle.Standard;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.ForeColor = Color.Black;
                combo.BackColor = Color.White;
            }
            else if (input is DateTimePicker datePicker)
            {
                if (input.Name == "dtpWaktuMulai" || input.Name == "dtpWaktuSelesai")
                {
                    datePicker.Format = DateTimePickerFormat.Custom;
                    datePicker.CustomFormat = "dd/MM/yyyy HH:mm";
                }
                else
                {
                    datePicker.Format = DateTimePickerFormat.Short;
                }
            }

            card.Controls.Add(input);
            y += inputHeight + 48;
        }

        private static void ResizeCardControls(Panel card)
        {
            int width = Math.Max(220, card.ClientSize.Width - 36);
            foreach (Control control in card.Controls)
            {
                if (control is Label || control is TextBox || control is ComboBox ||
                    control is DateTimePicker || control is Button)
                {
                    control.Width = width;
                }
            }
        }

        private static void StyleActionButton(Button button, Color accent)
        {
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.BackColor = accent;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(accent, 0.08F);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Line;
            grid.EnableHeadersVisualStyles = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.ColumnHeadersHeight = 42;
            grid.RowTemplate.Height = 34;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 243, 233);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Ink;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 243, 233);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Muted;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 243, 228);
            grid.DefaultCellStyle.SelectionForeColor = Ink;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 248);
        }

        private static void BuildPenerimaan(Form form)
        {
            BuildDataPage(
                form,
                "Penerimaan Bahan Baku",
                Color.FromArgb(98, 173, 69),
                ControlOf<Panel>(form, "panelInput"),
                ControlOf<DataGridView>(form, "dgvPenerimaan"),
                ControlOf<Button>(form, "btnSimpan"),
                new List<FieldSpec>
                {
                    new FieldSpec("lblKodeBatch", "txtKodeBatch"),
                    new FieldSpec("lblTanggal", "dtpTanggal"),
                    new FieldSpec("lblAsal", "txtAsal"),
                    new FieldSpec("lblBerat", "txtBerat"),
                    new FieldSpec("lblKondisi", "cboKondisi"),
                    new FieldSpec("lblKeterangan", "txtKeterangan")
                });
        }

        private static void BuildGrading(Form form)
        {
            BuildDataPage(
                form,
                "Grading dan Klasifikasi Mutu",
                Orange,
                ControlOf<Panel>(form, "panelInput"),
                ControlOf<DataGridView>(form, "dgvGrading"),
                ControlOf<Button>(form, "btnSimpanGrading"),
                new List<FieldSpec>
                {
                    new FieldSpec("lblBatch", "cboBatch"),
                    new FieldSpec("lblTanggal", "dtpTanggalGrading"),
                    new FieldSpec("lblJumlahSampel", "txtJumlahSampel"),
                    new FieldSpec("lblKondisiWarna", "cboKondisiWarna"),
                    new FieldSpec("lblKondisiFisik", "cboKondisiFisik"),
                    new FieldSpec("lblUkuranPolong", "txtUkuranPolong"),
                    new FieldSpec("lblGradeA", "txtGradeA"),
                    new FieldSpec("lblGradeB", "txtGradeB"),
                    new FieldSpec("lblReject", "txtReject")
                });
        }

        private static void BuildProsesProduksi(Form form)
        {
            BuildDataPage(
                form,
                "Monitoring Proses Produksi",
                Teal,
                ControlOf<Panel>(form, "panelInput"),
                ControlOf<DataGridView>(form, "dgvProses"),
                ControlOf<Button>(form, "btnSimpanProses"),
                new List<FieldSpec>
                {
                    new FieldSpec("lblBatch", "cboBatch"),
                    new FieldSpec("lblJenisProses", "cboJenisProses"),
                    new FieldSpec("lblSuhu", "txtSuhu"),
                    new FieldSpec("lblDurasi", "txtDurasi"),
                    new FieldSpec("lblWaktuMulai", "dtpWaktuMulai"),
                    new FieldSpec("lblWaktuSelesai", "dtpWaktuSelesai")
                });
        }

        private static void BuildQC(Form form)
        {
            BuildDataPage(
                form,
                "Quality Control dan Pengemasan",
                Purple,
                ControlOf<Panel>(form, "panelInput"),
                ControlOf<DataGridView>(form, "dgvQC"),
                ControlOf<Button>(form, "btnSimpanQC"),
                new List<FieldSpec>
                {
                    new FieldSpec("lblBatch", "cboBatch"),
                    new FieldSpec("lblTanggal", "dtpTanggalQC"),
                    new FieldSpec("lblKondisiFisik", "cboKondisiFisik"),
                    new FieldSpec("lblKondisiWarna", "cboKondisiWarna"),
                    new FieldSpec("lblKondisiKemasan", "cboKondisiKemasan"),
                    new FieldSpec("lblHasilQC", "cboHasilQC"),
                    new FieldSpec("lblCatatan", "txtCatatan"),
                    new FieldSpec("lblBeratKemasan", "txtBeratKemasan"),
                    new FieldSpec("lblJumlahKemasan", "txtJumlahKemasan"),
                    new FieldSpec("lblJenisKemasan", "txtJenisKemasan")
                },
                ControlOf<Label>(form, "lblSeparator"),
                7);
        }

        private static void BuildKelolaAkun(Form form)
        {
            Label title = ControlOf<Label>(form, "lblJudul");
            Panel panelForm = ControlOf<Panel>(form, "panelForm");
            Label lblNama = ControlOf<Label>(form, "lblNama");
            Label lblUsername = ControlOf<Label>(form, "lblUsername");
            Label lblPassword = ControlOf<Label>(form, "lblPassword");
            TextBox txtNama = ControlOf<TextBox>(form, "txtNama");
            TextBox txtUsername = ControlOf<TextBox>(form, "txtUsername");
            TextBox txtPassword = ControlOf<TextBox>(form, "txtPassword");
            Button btnTambah = ControlOf<Button>(form, "btnTambah");
            Button btnAktifkan = ControlOf<Button>(form, "btnAktifkan");
            Button btnNonaktifkan = ControlOf<Button>(form, "btnNonaktifkan");
            DataGridView grid = ControlOf<DataGridView>(form, "dgvPekerja");

            ResetLayout(form, title, panelForm, lblNama, lblUsername, lblPassword,
                txtNama, txtUsername, txtPassword, btnTambah, btnAktifkan, btnNonaktifkan, grid);
            form.Text = "EDATRACK - Kelola Akun Pekerja";
            TableLayoutPanel root = CreatePageRoot(form, "Kelola Akun Pekerja", Blue, title);

            TableLayoutPanel body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(12),
                BackColor = Color.White
            };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            panelForm.Controls.Clear();
            panelForm.Dock = DockStyle.Fill;
            panelForm.AutoScroll = true;
            panelForm.BackColor = Cream;
            panelForm.Padding = new Padding(10);
            panelForm.BorderStyle = BorderStyle.None;

            Panel card = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(14),
                Height = 310
            };
            panelForm.Controls.Add(card);
            int y = 20;
            StyleField(lblNama, txtNama, card, ref y);
            StyleField(lblUsername, txtUsername, card, ref y);
            StyleField(lblPassword, txtPassword, card, ref y);
            txtPassword.UseSystemPasswordChar = false;
            StyleActionButton(btnTambah, Blue);
            btnTambah.Text = "Tambah Akun Pekerja";
            btnTambah.Location = new Point(16, y + 8);
            btnTambah.Size = new Size(300, 40);
            btnTambah.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            card.Controls.Add(btnTambah);
            card.Height = y + 64;
            card.Resize += (_, _) => ResizeCardControls(card);
            panelForm.Resize += (_, _) => card.Width = Math.Max(300, panelForm.ClientSize.Width - 22);
            card.Width = Math.Max(300, panelForm.ClientSize.Width - 22);

            Panel tableSide = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(8, 0, 0, 0)
            };
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.White
            };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 88));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 12));
            StyleGrid(grid);
            grid.Dock = DockStyle.Fill;

            FlowLayoutPanel actionBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = Color.White,
                Padding = new Padding(0, 8, 0, 0),
                Margin = Padding.Empty
            };

            StyleActionButton(btnNonaktifkan, Red);
            btnNonaktifkan.Text = "Nonaktifkan Akun Terpilih";
            btnNonaktifkan.Size = new Size(210, 38);
            btnNonaktifkan.Margin = new Padding(10, 0, 0, 0);

            StyleActionButton(btnAktifkan, Green);
            btnAktifkan.Text = "Aktifkan Akun Terpilih";
            btnAktifkan.Size = new Size(200, 38);
            btnAktifkan.Margin = Padding.Empty;

            actionBar.Controls.Add(btnNonaktifkan);
            actionBar.Controls.Add(btnAktifkan);

            tableLayout.Controls.Add(grid, 0, 0);
            tableLayout.Controls.Add(actionBar, 0, 1);
            tableSide.Controls.Add(tableLayout);

            body.Controls.Add(panelForm, 0, 0);
            body.Controls.Add(tableSide, 1, 0);
            root.Controls.Add(body, 0, 2);
            form.ResumeLayout(true);
        }

        private static void BuildLogin(Form form)
        {
            Label title = ControlOf<Label>(form, "lblJudul");
            Label lblUsername = ControlOf<Label>(form, "lblUsername");
            Label lblPassword = ControlOf<Label>(form, "lblPassword");
            TextBox txtUsername = ControlOf<TextBox>(form, "txtUsername");
            TextBox txtPassword = ControlOf<TextBox>(form, "txtPassword");
            Button btnLogin = ControlOf<Button>(form, "btnLogin");

            ResetLayout(form, title, lblUsername, lblPassword, txtUsername, txtPassword, btnLogin);
            form.Text = "EDATRACK - Login";
            form.MinimumSize = new Size(900, 620);
            form.ClientSize = new Size(1280, 780);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));

            Panel art = new Panel { Dock = DockStyle.Fill, BackColor = GreenDark };
            art.Paint += (_, e) => DrawLoginIllustration(e.Graphics, art.ClientSize);

            Panel right = new Panel { Dock = DockStyle.Fill, BackColor = Cream };
            Panel card = new Panel
            {
                Size = new Size(440, 460),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(36)
            };
            right.Controls.Add(card);
            void CenterCard()
            {
                card.Location = new Point(
                    Math.Max(12, (right.ClientSize.Width - card.Width) / 2),
                    Math.Max(12, (right.ClientSize.Height - card.Height) / 2));
            }
            right.Resize += (_, _) => CenterCard();
            CenterCard();

            title.Text = "EDATRACK";
            title.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            title.ForeColor = Green;
            title.Location = new Point(38, 42);
            title.Size = new Size(350, 58);
            title.TextAlign = ContentAlignment.MiddleLeft;

            lblUsername.Text = "USERNAME";
            lblUsername.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblUsername.ForeColor = Muted;
            lblUsername.Location = new Point(40, 155);
            lblUsername.Size = new Size(340, 22);

            txtUsername.PlaceholderText = "Masukkan username";
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(40, 180);
            txtUsername.Size = new Size(350, 36);
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblPassword.Text = "PASSWORD";
            lblPassword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblPassword.ForeColor = Muted;
            lblPassword.Location = new Point(40, 244);
            lblPassword.Size = new Size(340, 22);

            txtPassword.PlaceholderText = "Masukkan password";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Location = new Point(40, 269);
            txtPassword.Size = new Size(350, 36);
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            StyleActionButton(btnLogin, Green);
            btnLogin.Text = "MASUK";
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.Location = new Point(40, 342);
            btnLogin.Size = new Size(350, 46);
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label subtitle = new Label
            {
                Text = "Sistem Monitoring Produksi",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Muted,
                Location = new Point(42, 102),
                Size = new Size(350, 26)
            };

            card.Controls.Add(title);
            card.Controls.Add(subtitle);
            card.Controls.Add(lblUsername);
            card.Controls.Add(txtUsername);
            card.Controls.Add(lblPassword);
            card.Controls.Add(txtPassword);
            card.Controls.Add(btnLogin);

            layout.Controls.Add(art, 0, 0);
            layout.Controls.Add(right, 1, 0);
            form.Controls.Add(layout);
            form.AcceptButton = btnLogin;
            form.ResumeLayout(true);
        }

        private static void DrawLoginIllustration(Graphics graphics, Size size)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush bg = new SolidBrush(GreenDark))
                graphics.FillRectangle(bg, new Rectangle(Point.Empty, size));

            using (SolidBrush circle1 = new SolidBrush(Color.FromArgb(22, 125, 32)))
                graphics.FillEllipse(circle1, -120, -85, 380, 380);
            using (SolidBrush circle2 = new SolidBrush(Color.FromArgb(18, 132, 29)))
                graphics.FillEllipse(circle2, size.Width - 260, size.Height - 240, 360, 360);

            DrawPeaPod(graphics, size.Width * 0.25f, size.Height * 0.36f, 1.25f, -28);
            DrawPeaPod(graphics, size.Width * 0.62f, size.Height * 0.25f, 0.72f, -42);
            DrawPeaPod(graphics, size.Width * 0.25f, size.Height * 0.72f, 0.82f, 18);

            using (Font heading = new Font("Segoe UI", 25F, FontStyle.Bold))
            using (Font caption = new Font("Segoe UI", 11F, FontStyle.Regular))
            using (SolidBrush white = new SolidBrush(Color.White))
            using (SolidBrush pale = new SolidBrush(Color.FromArgb(220, 255, 255, 255)))
            {
                graphics.DrawString("EDATRACK", heading, white, 42, size.Height - 116);
                graphics.DrawString("Sistem Monitoring Produksi", caption, pale, 46, size.Height - 70);
            }
        }

        private static void DrawPeaPod(Graphics graphics, float x, float y, float scale, float angle)
        {
            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);
            graphics.ScaleTransform(scale, scale);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 235, 74, 90, 180);
                path.AddArc(0, 0, 235, 74, 270, 180);
                path.CloseFigure();
                using (LinearGradientBrush pod = new LinearGradientBrush(
                    new Rectangle(0, 0, 235, 74),
                    Color.FromArgb(182, 231, 108),
                    Color.FromArgb(132, 193, 70),
                    20F))
                {
                    graphics.FillPath(pod, path);
                }
            }

            using (SolidBrush pea = new SolidBrush(Color.FromArgb(218, 244, 164)))
            using (SolidBrush shade = new SolidBrush(Color.FromArgb(130, 151, 65)))
            {
                for (int i = 0; i < 3; i++)
                {
                    graphics.FillEllipse(shade, 31 + i * 62, 24, 34, 34);
                    graphics.FillEllipse(pea, 28 + i * 62, 19, 34, 34);
                }
            }

            graphics.Restore(state);
        }

        private static void BuildDashboardAdmin(Form form)
        {
            Label welcome = ControlOf<Label>(form, "lblSelamatDatang");
            Label brand = ControlOf<Label>(form, "lblSubjudul");
            Button accounts = ControlOf<Button>(form, "btnKelolaAkun");
            Button penerimaan = ControlOf<Button>(form, "btnLihatPenerimaan");
            Button grading = ControlOf<Button>(form, "btnLihatGrading");
            Button produksi = ControlOf<Button>(form, "btnLihatProses");
            Button qc = ControlOf<Button>(form, "btnLihatQC");
            Button logout = ControlOf<Button>(form, "btnLogout");

            ResetLayout(form, welcome, brand, accounts, penerimaan, grading, produksi, qc, logout);
            form.Text = "EDATRACK - Dashboard Admin";
            BuildDashboard(form, brand, welcome, new[]
            {
                (accounts, "👤\n\nKelola Akun Pekerja", Blue, false),
                (penerimaan, "▣\n\nLihat Data Penerimaan", Green, false),
                (grading, "▥\n\nLihat Data Grading", Orange, false),
                (produksi, "♟\n\nLihat Data Proses Produksi", Teal, false),
                (qc, "✓\n\nLihat Data QC dan Pengemasan", Purple, false),
                (logout, "↪\n\nLogout", Red, true)
            });
        }

        private static void BuildDashboardPekerja(Form form)
        {
            Label welcome = ControlOf<Label>(form, "lblSelamatDatang");
            Label brand = ControlOf<Label>(form, "lblSubjudul");
            Button penerimaan = ControlOf<Button>(form, "btnPenerimaan");
            Button grading = ControlOf<Button>(form, "btnGrading");
            Button produksi = ControlOf<Button>(form, "btnProsesProduksi");
            Button qc = ControlOf<Button>(form, "btnQC");
            Button logout = ControlOf<Button>(form, "btnLogout");

            ResetLayout(form, welcome, brand, penerimaan, grading, produksi, qc, logout);
            form.Text = "EDATRACK - Dashboard Pekerja";
            BuildDashboard(form, brand, welcome, new[]
            {
                (penerimaan, "▣\n\nPenerimaan Bahan Baku", Green, false),
                (grading, "▥\n\nGrading dan Klasifikasi Mutu", Orange, false),
                (produksi, "♟\n\nMonitoring Proses Produksi", Teal, false),
                (qc, "✓\n\nQuality Control dan Pengemasan", Purple, false),
                (logout, "↪\n\nLogout", Red, true)
            });
        }

        private static void BuildDashboard(
            Form form,
            Label brand,
            Label welcome,
            (Button Button, string Text, Color Color, bool IsLogout)[] tiles)
        {
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Panel topbar = new Panel { Dock = DockStyle.Fill, BackColor = GreenDark };
            brand.Text = "EDATRACK";
            brand.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            brand.ForeColor = Color.White;
            brand.TextAlign = ContentAlignment.MiddleLeft;
            brand.Location = new Point(28, 9);
            brand.Size = new Size(250, 28);
            welcome.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            welcome.ForeColor = Color.FromArgb(235, 255, 235);
            welcome.TextAlign = ContentAlignment.MiddleLeft;
            welcome.Location = new Point(29, 42);
            welcome.Size = new Size(650, 25);
            topbar.Controls.Add(brand);
            topbar.Controls.Add(welcome);

            Panel dashboard = new Panel { Dock = DockStyle.Fill, BackColor = Cream, Padding = new Padding(26) };
            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                BackColor = Cream,
                Padding = new Padding(10)
            };
            for (int i = 0; i < 3; i++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            for (int i = 0; i < tiles.Length; i++)
            {
                var tile = tiles[i];
                StyleTile(tile.Button, tile.Text, tile.Color, tile.IsLogout);
                tile.Button.Dock = DockStyle.Fill;
                tile.Button.Margin = new Padding(10);
                grid.Controls.Add(tile.Button, i % 3, i / 3);
            }

            dashboard.Controls.Add(grid);
            root.Controls.Add(topbar, 0, 0);
            root.Controls.Add(dashboard, 0, 1);
            form.Controls.Add(root);
            form.ResumeLayout(true);
        }

        private static void StyleTile(Button button, string text, Color accent, bool logout)
        {
            string[] parts = text.Split(new[] { "\n\n" }, StringSplitOptions.None);
            string iconText = parts.Length > 1 ? parts[0] : "•";
            string caption = parts.Length > 1 ? parts[1] : text;

            button.Text = caption;
            button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button.TextAlign = ContentAlignment.BottomLeft;
            button.Padding = new Padding(22, 68, 16, 16);
            button.BackColor = logout ? Red : Color.White;
            button.ForeColor = logout ? Color.White : Ink;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = logout ? Red : Line;
            button.FlatAppearance.MouseOverBackColor = logout
                ? Color.FromArgb(171, 28, 33)
                : Color.FromArgb(251, 254, 249);
            button.FlatAppearance.MouseDownBackColor = logout
                ? Color.FromArgb(158, 24, 29)
                : Color.FromArgb(234, 243, 228);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            button.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle iconBounds = new Rectangle(20, 16, 40, 40);
                Color iconBack = logout
                    ? Color.FromArgb(213, 76, 80)
                    : Color.FromArgb(
                        (accent.R + 255) / 2,
                        (accent.G + 255) / 2,
                        (accent.B + 255) / 2);
                using (SolidBrush iconBrush = new SolidBrush(iconBack))
                    e.Graphics.FillEllipse(iconBrush, iconBounds);
                using (Font iconFont = new Font("Segoe UI Emoji", 16F, FontStyle.Regular))
                using (SolidBrush iconFore = new SolidBrush(logout ? Color.White : accent))
                using (StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    e.Graphics.DrawString(iconText, iconFont, iconFore, iconBounds, format);
                }
            };
        }
    }
}