using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.Views
{
    public partial class MenuInicio : Form
    {
        private readonly string _usuarioActual;
        private readonly UsuarioController _controller = new UsuarioController();

        public MenuInicio(string usuarioActual)
        {
            InitializeComponent();
            _usuarioActual = usuarioActual;
            lblBienvenida.Text = $"¡Bienvenido, {usuarioActual}!";

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

        // Botón 1vs1
        private void btn1vs1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu1vs1 menu = new Menu1vs1(_usuarioActual);
            menu.ShowDialog();
        }

        private void btn1vsIA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu1vsIA menu = new Menu1vsIA(_usuarioActual);
            menu.ShowDialog();
        }

        // Botón Historial
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            MostrarHistorial historial = new MostrarHistorial(_usuarioActual);
            historial.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MostrarHistorial historial = new MostrarHistorial(_usuarioActual);
            historial.ShowDialog();
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
    }
}