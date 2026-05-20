namespace WinFormsApp1.Views
{
    partial class GestionPersonajesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionPersonajesForm));
            crear = new PictureBox();
            eliminar = new PictureBox();
            refrescar = new PictureBox();
            escogerPj = new PictureBox();
            panelCrud = new Panel();
            txtDescripcion = new TextBox();
            txtResistencia = new TextBox();
            txtDefensa = new TextBox();
            txtAtaque = new TextBox();
            txtNombre = new TextBox();
            dgvPersonajes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)crear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eliminar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)refrescar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)escogerPj).BeginInit();
            panelCrud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonajes).BeginInit();
            SuspendLayout();
            // 
            // crear
            // 
            crear.BackColor = Color.Transparent;
            crear.Location = new Point(354, 31);
            crear.Name = "crear";
            crear.Size = new Size(74, 41);
            crear.TabIndex = 1;
            crear.TabStop = false;
            crear.Click += btnCrear_Click;
            // 
            // eliminar
            // 
            eliminar.BackColor = Color.Transparent;
            eliminar.Location = new Point(446, 31);
            eliminar.Name = "eliminar";
            eliminar.Size = new Size(83, 41);
            eliminar.TabIndex = 2;
            eliminar.TabStop = false;
            eliminar.Click += btnEliminar_Click;
            // 
            // refrescar
            // 
            refrescar.BackColor = Color.Transparent;
            refrescar.Location = new Point(545, 31);
            refrescar.Name = "refrescar";
            refrescar.Size = new Size(91, 41);
            refrescar.TabIndex = 3;
            refrescar.TabStop = false;
            refrescar.Click += btnRefrescar_Click;
            // 
            // escogerPj
            // 
            escogerPj.BackColor = Color.Transparent;
            escogerPj.BackgroundImage = Properties.Resources.BotonEscoger;
            escogerPj.BackgroundImageLayout = ImageLayout.Stretch;
            escogerPj.Location = new Point(659, 24);
            escogerPj.Name = "escogerPj";
            escogerPj.Size = new Size(103, 55);
            escogerPj.TabIndex = 4;
            escogerPj.TabStop = false;
            escogerPj.Click += btnEscogerPj_Click;
            // 
            // panelCrud
            // 
            panelCrud.BackColor = Color.Transparent;
            panelCrud.Controls.Add(txtDescripcion);
            panelCrud.Controls.Add(txtResistencia);
            panelCrud.Controls.Add(txtDefensa);
            panelCrud.Controls.Add(txtAtaque);
            panelCrud.Controls.Add(txtNombre);
            panelCrud.Controls.Add(dgvPersonajes);
            panelCrud.Location = new Point(22, 119);
            panelCrud.Name = "panelCrud";
            panelCrud.Size = new Size(755, 302);
            panelCrud.TabIndex = 0;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(523, 3);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(229, 23);
            txtDescripcion.TabIndex = 9;
            // 
            // txtResistencia
            // 
            txtResistencia.Location = new Point(433, 3);
            txtResistencia.Name = "txtResistencia";
            txtResistencia.Size = new Size(100, 23);
            txtResistencia.TabIndex = 8;
            // 
            // txtDefensa
            // 
            txtDefensa.Location = new Point(322, 3);
            txtDefensa.Name = "txtDefensa";
            txtDefensa.Size = new Size(116, 23);
            txtDefensa.TabIndex = 7;
            // 
            // txtAtaque
            // 
            txtAtaque.Location = new Point(195, 3);
            txtAtaque.Name = "txtAtaque";
            txtAtaque.Size = new Size(130, 23);
            txtAtaque.TabIndex = 6;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(3, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(218, 23);
            txtNombre.TabIndex = 5;
            // 
            // dgvPersonajes
            // 
            dgvPersonajes.BackgroundColor = SystemColors.ControlDarkDark;
            dgvPersonajes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonajes.Location = new Point(0, 32);
            dgvPersonajes.Name = "dgvPersonajes";
            dgvPersonajes.Size = new Size(755, 270);
            dgvPersonajes.TabIndex = 0;
            dgvPersonajes.SelectionChanged += GridPersonajes_SelectionChanged;
            // 
            // GestionPersonajesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.FondoGestionPersonajes;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(panelCrud);
            Controls.Add(escogerPj);
            Controls.Add(refrescar);
            Controls.Add(eliminar);
            Controls.Add(crear);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GestionPersonajesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GestionPersonajesForm";
            ((System.ComponentModel.ISupportInitialize)crear).EndInit();
            ((System.ComponentModel.ISupportInitialize)eliminar).EndInit();
            ((System.ComponentModel.ISupportInitialize)refrescar).EndInit();
            ((System.ComponentModel.ISupportInitialize)escogerPj).EndInit();
            panelCrud.ResumeLayout(false);
            panelCrud.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonajes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox crear;
        private PictureBox eliminar;
        private PictureBox refrescar;
        private PictureBox escogerPj;
        private Panel panelCrud;
        private TextBox txtDescripcion;
        private TextBox txtResistencia;
        private TextBox txtDefensa;
        private TextBox txtAtaque;
        private TextBox txtNombre;
        private DataGridView dgvPersonajes;
    }
}
