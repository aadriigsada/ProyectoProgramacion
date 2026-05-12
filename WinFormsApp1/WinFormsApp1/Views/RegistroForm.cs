using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
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
            textBox2.UseSystemPasswordChar = true;

            try
            {
                string rutaCursor = Path.Combine(Application.StartupPath, "BoxingGlove.cur");
                if (File.Exists(rutaCursor))
                {
                    IntPtr cursorHandle = LoadCursorFromFile(rutaCursor);
                    if (cursorHandle != IntPtr.Zero)
                    {
                        this.Cursor = new Cursor(cursorHandle);
                    }
                }
            }
            catch { /* Error silencioso */ }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Introduce usuario y contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = username,
                Password = password,
                Email = $"{username}@gmail.com"
            };

            bool registrado = _controller.RegistrarUsuario(nuevoUsuario);

            if (registrado)
            {
                MessageBox.Show("Usuario registrado correctamente.", "Registro",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo registrar el usuario. Revisa si ya existe.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

    }
}
