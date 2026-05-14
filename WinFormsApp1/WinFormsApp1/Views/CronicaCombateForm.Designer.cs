namespace WinFormsApp1.Views
{
    partial class CronicaCombateForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CronicaCombateForm));
            panelHeader = new Panel();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            panelContenido = new Panel();
            rtbHistoria = new RichTextBox();
            panelFooter = new Panel();
            btnSaltar = new Button();
            panelHeader.SuspendLayout();
            panelContenido.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(30, 30, 30);
            panelHeader.Controls.Add(lblSubtitulo);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(24, 18, 24, 12);
            panelHeader.Size = new Size(900, 96);
            panelHeader.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubtitulo.AutoEllipsis = true;
            lblSubtitulo.Font = new Font("Consolas", 11F);
            lblSubtitulo.ForeColor = Color.FromArgb(232, 221, 185);
            lblSubtitulo.Location = new Point(28, 57);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(848, 22);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Peleadores listos para UEFETE";
            lblSubtitulo.Click += lblSubtitulo_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Consolas", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 196, 67);
            lblTitulo.Location = new Point(24, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(224, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CRONICA PREVIA";
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.FromArgb(14, 14, 14);
            panelContenido.Controls.Add(rtbHistoria);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 96);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(24, 22, 24, 18);
            panelContenido.Size = new Size(900, 424);
            panelContenido.TabIndex = 1;
            // 
            // rtbHistoria
            // 
            rtbHistoria.BackColor = Color.FromArgb(247, 239, 218);
            rtbHistoria.BorderStyle = BorderStyle.None;
            rtbHistoria.DetectUrls = false;
            rtbHistoria.Dock = DockStyle.Fill;
            rtbHistoria.Font = new Font("Georgia", 11.25F);
            rtbHistoria.ForeColor = Color.FromArgb(32, 28, 24);
            rtbHistoria.Location = new Point(24, 22);
            rtbHistoria.Name = "rtbHistoria";
            rtbHistoria.ReadOnly = true;
            rtbHistoria.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbHistoria.Size = new Size(852, 384);
            rtbHistoria.TabIndex = 2;
            rtbHistoria.Text = "";
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.FromArgb(30, 30, 30);
            panelFooter.Controls.Add(btnSaltar);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 520);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new Padding(24, 14, 24, 14);
            panelFooter.Size = new Size(900, 70);
            panelFooter.TabIndex = 2;
            // 
            // btnSaltar
            // 
            btnSaltar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaltar.BackColor = Color.FromArgb(198, 120, 12);
            btnSaltar.FlatAppearance.BorderColor = Color.Black;
            btnSaltar.FlatAppearance.BorderSize = 2;
            btnSaltar.FlatStyle = FlatStyle.Flat;
            btnSaltar.Font = new Font("Consolas", 10.5F, FontStyle.Bold);
            btnSaltar.ForeColor = Color.Black;
            btnSaltar.Location = new Point(728, 16);
            btnSaltar.Name = "btnSaltar";
            btnSaltar.Size = new Size(148, 38);
            btnSaltar.TabIndex = 3;
            btnSaltar.Text = "IR AL COMBATE";
            btnSaltar.UseVisualStyleBackColor = false;
            btnSaltar.Click += btnSaltar_Click;
            // 
            // CronicaCombateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 14, 14);
            ClientSize = new Size(900, 590);
            Controls.Add(panelContenido);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(760, 500);
            Name = "CronicaCombateForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cronica previa";
            FormClosing += CronicaCombateForm_FormClosing;
            Load += CronicaCombateForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContenido.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Panel panelContenido;
        private RichTextBox rtbHistoria;
        private Panel panelFooter;
        private Button btnSaltar;
    }
}
