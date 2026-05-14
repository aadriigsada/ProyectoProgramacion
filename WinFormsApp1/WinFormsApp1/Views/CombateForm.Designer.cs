namespace WinFormsApp1.Views
{
    partial class CombateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombateForm));
            VidaPj1 = new WinFormsApp1.Controls.ModernProgressBar();
            VidaPj2 = new WinFormsApp1.Controls.ModernProgressBar();
            pj1 = new PictureBox();
            pj2 = new PictureBox();
            btnAtaque = new Button();
            btnDefensa = new Button();
            btnPatada = new Button();
            btnSumision = new Button();
            rtbHistorial = new RichTextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pj1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pj2).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // VidaPj1
            // 
            VidaPj1.BackColor = SystemColors.ActiveCaptionText;
            VidaPj1.BorderColor = Color.FromArgb(80, 180, 130);
            VidaPj1.CornerRadius = 12;
            VidaPj1.FillColorEnd = Color.FromArgb(41, 163, 97);
            VidaPj1.FillColorStart = Color.FromArgb(78, 212, 120);
            VidaPj1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            VidaPj1.Location = new Point(35, 7);
            VidaPj1.Name = "VidaPj1";
            VidaPj1.Size = new Size(195, 32);
            VidaPj1.TabIndex = 0;
            VidaPj1.TextColor = Color.White;
            VidaPj1.TrackColor = Color.Transparent;
            // 
            // VidaPj2
            // 
            VidaPj2.BackColor = SystemColors.ActiveCaptionText;
            VidaPj2.BorderColor = Color.FromArgb(200, 115, 90);
            VidaPj2.CornerRadius = 12;
            VidaPj2.FillColorEnd = Color.FromArgb(210, 76, 58);
            VidaPj2.FillColorStart = Color.FromArgb(239, 117, 81);
            VidaPj2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            VidaPj2.Location = new Point(36, 7);
            VidaPj2.Name = "VidaPj2";
            VidaPj2.Size = new Size(195, 32);
            VidaPj2.TabIndex = 1;
            VidaPj2.TextColor = Color.White;
            VidaPj2.TrackColor = Color.Transparent;
            // 
            // pj1
            // 
            pj1.BackColor = Color.Transparent;
            pj1.BackgroundImageLayout = ImageLayout.None;
            pj1.Image = Properties.Resources.LuchadorIzquierda;
            pj1.Location = new Point(220, 120);
            pj1.Name = "pj1";
            pj1.Size = new Size(122, 172);
            pj1.SizeMode = PictureBoxSizeMode.StretchImage;
            pj1.TabIndex = 2;
            pj1.TabStop = false;
            pj1.Click += pictureBox1_Click;
            // 
            // pj2
            // 
            pj2.BackColor = Color.Transparent;
            pj2.Image = Properties.Resources.LuchadorDerecha;
            pj2.Location = new Point(444, 120);
            pj2.Name = "pj2";
            pj2.Size = new Size(109, 172);
            pj2.SizeMode = PictureBoxSizeMode.StretchImage;
            pj2.TabIndex = 3;
            pj2.TabStop = false;
            pj2.Click += pj2_Click;
            // 
            // btnAtaque
            // 
            btnAtaque.BackColor = Color.RoyalBlue;
            btnAtaque.FlatStyle = FlatStyle.Flat;
            btnAtaque.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAtaque.ForeColor = Color.Transparent;
            btnAtaque.Location = new Point(14, 8);
            btnAtaque.Name = "btnAtaque";
            btnAtaque.Size = new Size(121, 50);
            btnAtaque.TabIndex = 4;
            btnAtaque.Text = "Puñetazo";
            btnAtaque.UseVisualStyleBackColor = false;
            btnAtaque.Click += btnAtaque_Click;
            // 
            // btnDefensa
            // 
            btnDefensa.BackColor = Color.RoyalBlue;
            btnDefensa.FlatStyle = FlatStyle.Flat;
            btnDefensa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDefensa.ForeColor = Color.White;
            btnDefensa.Location = new Point(14, 8);
            btnDefensa.Name = "btnDefensa";
            btnDefensa.Size = new Size(120, 50);
            btnDefensa.TabIndex = 5;
            btnDefensa.Text = "Defensa";
            btnDefensa.UseVisualStyleBackColor = false;
            btnDefensa.Click += btnDefensa_Click;
            // 
            // btnPatada
            // 
            btnPatada.BackColor = Color.RoyalBlue;
            btnPatada.FlatStyle = FlatStyle.Flat;
            btnPatada.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPatada.ForeColor = Color.White;
            btnPatada.Location = new Point(14, 7);
            btnPatada.Name = "btnPatada";
            btnPatada.Size = new Size(120, 51);
            btnPatada.TabIndex = 6;
            btnPatada.Text = "Patada";
            btnPatada.UseVisualStyleBackColor = false;
            btnPatada.Click += btnPatada_Click;
            // 
            // btnSumision
            // 
            btnSumision.BackColor = Color.RoyalBlue;
            btnSumision.FlatStyle = FlatStyle.Flat;
            btnSumision.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSumision.ForeColor = Color.White;
            btnSumision.Location = new Point(14, 8);
            btnSumision.Name = "btnSumision";
            btnSumision.Size = new Size(120, 50);
            btnSumision.TabIndex = 7;
            btnSumision.Text = "Sumisión";
            btnSumision.UseVisualStyleBackColor = false;
            btnSumision.Click += btnSumision_Click;
            // 
            // rtbHistorial
            // 
            rtbHistorial.BackColor = Color.FromArgb(18, 18, 18);
            rtbHistorial.BorderStyle = BorderStyle.FixedSingle;
            rtbHistorial.ForeColor = Color.Gainsboro;
            rtbHistorial.Location = new Point(8, 14);
            rtbHistorial.Name = "rtbHistorial";
            rtbHistorial.ReadOnly = true;
            rtbHistorial.Size = new Size(163, 180);
            rtbHistorial.TabIndex = 8;
            rtbHistorial.Text = "";
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.FondoAA;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(rtbHistorial);
            panel1.Location = new Point(610, 120);
            panel1.Name = "panel1";
            panel1.Size = new Size(178, 203);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122102;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(btnAtaque);
            panel2.Location = new Point(214, 301);
            panel2.Name = "panel2";
            panel2.Size = new Size(149, 65);
            panel2.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122102;
            panel3.BackgroundImageLayout = ImageLayout.Stretch;
            panel3.Controls.Add(btnDefensa);
            panel3.Location = new Point(214, 373);
            panel3.Name = "panel3";
            panel3.Size = new Size(149, 65);
            panel3.TabIndex = 11;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122102;
            panel4.BackgroundImageLayout = ImageLayout.Stretch;
            panel4.Controls.Add(btnPatada);
            panel4.Location = new Point(430, 301);
            panel4.Name = "panel4";
            panel4.Size = new Size(149, 65);
            panel4.TabIndex = 12;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Transparent;
            panel5.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122102;
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Controls.Add(btnSumision);
            panel5.Location = new Point(430, 373);
            panel5.Name = "panel5";
            panel5.Size = new Size(149, 65);
            panel5.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122949;
            panel6.BackgroundImageLayout = ImageLayout.Stretch;
            panel6.Controls.Add(VidaPj1);
            panel6.Location = new Point(129, 12);
            panel6.Name = "panel6";
            panel6.Size = new Size(261, 47);
            panel6.TabIndex = 14;
            // 
            // panel7
            // 
            panel7.BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_05_12_122949;
            panel7.BackgroundImageLayout = ImageLayout.Stretch;
            panel7.Controls.Add(VidaPj2);
            panel7.Location = new Point(430, 12);
            panel7.Name = "panel7";
            panel7.Size = new Size(261, 47);
            panel7.TabIndex = 15;
            // 
            // CombateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Chocolate;
            BackgroundImage = Properties.Resources.FondoCombate;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pj2);
            Controls.Add(pj1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CombateForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CombateForm";
            Load += CombateForm_Load;
            ((System.ComponentModel.ISupportInitialize)pj1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pj2).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private WinFormsApp1.Controls.ModernProgressBar VidaPj1;
        private WinFormsApp1.Controls.ModernProgressBar VidaPj2;
        private PictureBox pj1;
        private PictureBox pj2;
        private Button btnAtaque;
        private Button btnDefensa;
        private Button btnPatada;
        private Button btnSumision;
        private RichTextBox rtbHistorial;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
    }
}
