namespace WinFormsApp1.Views
{
    partial class MostrarHistorial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnRefrescar = new Button();
            lblResumen = new Label();
            lblTitulo = new Label();
            dataGridView1 = new DataGridView();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(34, 34, 34);
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnRefrescar);
            panelTop.Controls.Add(lblResumen);
            panelTop.Controls.Add(lblTitulo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(980, 72);
            panelTop.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            btnRefrescar.BackColor = Color.FromArgb(198, 120, 12);
            btnRefrescar.FlatAppearance.BorderColor = Color.Black;
            btnRefrescar.FlatAppearance.BorderSize = 2;
            btnRefrescar.FlatStyle = FlatStyle.Flat;
            btnRefrescar.Font = new Font("Consolas", 10F, FontStyle.Bold);
            btnRefrescar.ForeColor = Color.Black;
            btnRefrescar.Location = new Point(856, 23);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(112, 30);
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = false;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Consolas", 9.75F, FontStyle.Regular);
            lblResumen.ForeColor = Color.FromArgb(240, 229, 192);
            lblResumen.Location = new Point(16, 43);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(188, 15);
            lblResumen.TabIndex = 1;
            lblResumen.Text = "Total: 0 | Victorias: 0 | Derrotas: 0";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Consolas", 13F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 196, 67);
            lblTitulo.Location = new Point(12, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(185, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historial de combates";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = Color.FromArgb(18, 18, 18);
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.GridColor = Color.FromArgb(70, 70, 70);
            dataGridView1.Location = new Point(0, 72);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(980, 478);
            dataGridView1.TabIndex = 1;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 120, 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(24, 24, 24);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(239, 239, 239);
            dataGridView1.DefaultCellStyle.Font = new Font("Consolas", 9.5F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 196, 67);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            // 
            // MostrarHistorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(980, 550);
            Controls.Add(dataGridView1);
            Controls.Add(panelTop);
            Font = new Font("Consolas", 9.5F, FontStyle.Regular);
            ForeColor = Color.FromArgb(239, 239, 239);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MostrarHistorial";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Historial de Combates";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnRefrescar;
        private Label lblResumen;
        private Label lblTitulo;
        private DataGridView dataGridView1;
    }
}
