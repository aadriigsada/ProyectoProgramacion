using System;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public partial class RegistroForm : Form
    {
        private readonly UsuarioController _controller = new UsuarioController();

        public RegistroForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Validación básica
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Rellena todos los campos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = nombre + "@uefete.com",  // ⚠️ Ver nota abajo
                Password = password
            };

            bool ok = _controller.RegistrarUsuario(nuevoUsuario);

            if (ok)
            {
                MessageBox.Show("¡Registro exitoso! Ya puedes iniciar sesión.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();  // Cierra el registro → vuelve al Login automáticamente
            }
        }
    }
}