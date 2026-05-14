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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MostrarHistorial));
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
            lblResumen.Font = new Font("Consolas", 9.75F);
            lblResumen.ForeColor = Color.FromArgb(240, 229, 192);
            lblResumen.Location = new Point(16, 43);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(266, 15);
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
            lblTitulo.Size = new Size(220, 22);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historial de combates";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 30, 30);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.FromArgb(18, 18, 18);
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(198, 120, 12);
            dataGridViewCellStyle2.Font = new Font("Consolas", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(24, 24, 24);
            dataGridViewCellStyle3.Font = new Font("Consolas", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(239, 239, 239);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(255, 196, 67);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(70, 70, 70);
            dataGridView1.Location = new Point(0, 72);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(980, 478);
            dataGridView1.TabIndex = 1;
            // 
            // MostrarHistorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(980, 550);
            Controls.Add(dataGridView1);
            Controls.Add(panelTop);
            Font = new Font("Consolas", 9.5F);
            ForeColor = Color.FromArgb(239, 239, 239);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
