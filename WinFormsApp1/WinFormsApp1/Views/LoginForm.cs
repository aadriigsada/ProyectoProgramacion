using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Views;
namespace WinFormsApp1
{
    public partial class LoginForm : Form
    {
        private readonly UsuarioController _controller = new UsuarioController();

        public LoginForm()
        {
            InitializeComponent();
            button1.Parent = this;
            button1.BackColor = Color.Transparent;
            button2.Parent = this;
            button2.BackColor = Color.Transparent;

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

        // Botón INICIAR SESIÓN
        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string pass = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(pass)
                || usuario == "Usuario" || pass == "Contraseña")
            {
                MessageBox.Show("Rellena los campos para pelear.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = _controller.ValidarLogin(usuario, pass);

            if (ok)
            {
                MenuInicio menu = new MenuInicio(usuario);
                this.Hide();
                menu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón REGISTRARSE
        private void button2_Click(object sender, EventArgs e)
        {
            RegistroForm ventanaRegistro = new RegistroForm();
            this.Hide();
            ventanaRegistro.ShowDialog();
            this.Show();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Usuario")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.White;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Usuario";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Contraseña")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.White;
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Contraseña";
                textBox2.ForeColor = Color.Silver;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string pass = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(pass)
                || usuario == "Usuario" || pass == "Contraseña")
            {
                MessageBox.Show("Rellena los campos para pelear.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = _controller.ValidarLogin(usuario, pass);

            if (ok)
            {
                MenuInicio menu = new MenuInicio(usuario);
                this.Hide();
                menu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
    }
}