using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Views;

namespace WinFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Transparencias para que se vea el fondo de los luchadores
            button1.Parent = this;
            button1.BackColor = Color.Transparent;
            button2.Parent = this;
            button2.BackColor = Color.Transparent;
        }

        // Lógica para Iniciar Sesión
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Rellena los campos para pelear.");
            }
            else
            {
                MessageBox.Show("Validando usuario...");
                // Aquí irá el controlador más adelante
            }
        }

        // Lógica para ir a Registro
        private void button2_Click(object sender, EventArgs e)
        {
            // Creamos la ventana de registro
            RegistroForm ventanaRegistro = new RegistroForm();

            this.Hide(); // Escondemos el login
            ventanaRegistro.ShowDialog(); // Abrimos registro
            this.Show(); // Al volver, mostramos login
        }
        // --- PLACEHOLDER PARA USUARIO ---
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Usuario")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.White; // Cambia a blanco cuando el usuario escribe
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

        // --- PLACEHOLDER PARA CONTRASEÑA ---
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Contraseña")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.White;
                textBox2.UseSystemPasswordChar = true; // Empieza a ocultar los caracteres
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Contraseña";
                textBox2.ForeColor = Color.Silver;
                textBox2.UseSystemPasswordChar = false; // Muestra la palabra "Contraseña" otra vez
            }
        }
    }
}