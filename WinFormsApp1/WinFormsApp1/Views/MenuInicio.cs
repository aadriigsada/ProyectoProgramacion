using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

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
            CursorHelper.ApplyCustomCursor(this);
        }

        // Botón 1vs1
        private void btn1vs1_Click(object sender, EventArgs e)
        {
            using Menu1vs1 menu = new Menu1vs1(_usuarioActual);
            menu.ShowDialog(this);
        }

        private void btn1vsIA_Click(object sender, EventArgs e)
        {
            using Menu1vsIA menu = new Menu1vsIA(_usuarioActual);
            menu.ShowDialog(this);
        }

        // Botón Historial
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            using MostrarHistorial historial = new MostrarHistorial(_usuarioActual);
            historial.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using MostrarHistorial historial = new MostrarHistorial(_usuarioActual);
            historial.ShowDialog(this);
        }
    }
}
