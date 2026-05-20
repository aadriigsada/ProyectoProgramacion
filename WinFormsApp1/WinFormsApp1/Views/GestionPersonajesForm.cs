using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public partial class GestionPersonajesForm : Form
    {
        private readonly PersonajeController _controller = new PersonajeController();
        private readonly BindingSource _bindingSource = new BindingSource();

        public Personaje? PersonajeSeleccionado { get; private set; }

        public GestionPersonajesForm()
        {
            InitializeComponent();
            CursorHelper.ApplyCustomCursor(this);
            ConfigurarLogicaCrud();
            CargarPersonajesEnGrid();
        }

        private void ConfigurarLogicaCrud()
        {
            ConfigurarColumnasGrid();
            dgvPersonajes.AutoGenerateColumns = false;
            dgvPersonajes.ReadOnly = true;
            dgvPersonajes.MultiSelect = false;
            dgvPersonajes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonajes.AllowUserToAddRows = false;
            dgvPersonajes.AllowUserToDeleteRows = false;
            dgvPersonajes.DataSource = _bindingSource;
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvPersonajes.Columns.Count == 0)
            {
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre", Width = 170 });
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ataque", DataPropertyName = "Ataque", Width = 80 });
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Defensa", DataPropertyName = "Defensa", Width = 80 });
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Resistencia", DataPropertyName = "Resistencia", Width = 90 });
                dgvPersonajes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripcion", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                return;
            }

            string[] propiedades = { "Id", "Nombre", "Ataque", "Defensa", "Resistencia", "Descripcion" };
            for (int i = 0; i < dgvPersonajes.Columns.Count && i < propiedades.Length; i++)
            {
                dgvPersonajes.Columns[i].DataPropertyName = propiedades[i];
            }
        }

        private void CargarPersonajesEnGrid()
        {
            List<Personaje> personajes = _controller.ObtenerPersonajes();
            _bindingSource.DataSource = personajes;

            if (dgvPersonajes.Rows.Count > 0)
            {
                dgvPersonajes.Rows[0].Selected = true;
                ActualizarCamposDesdeSeleccion();
            }
        }

        // Este es el evento que rellena los TextBox al pinchar en la lista
        private void GridPersonajes_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarCamposDesdeSeleccion();
        }

        private void ActualizarCamposDesdeSeleccion()
        {
            if (dgvPersonajes.CurrentRow?.DataBoundItem is Personaje personaje)
            {
                txtNombre.Text = personaje.Nombre;
                txtAtaque.Text = personaje.Ataque.ToString(CultureInfo.InvariantCulture);
                txtDefensa.Text = personaje.Defensa.ToString(CultureInfo.InvariantCulture);
                txtResistencia.Text = personaje.Resistencia.ToString(CultureInfo.InvariantCulture);
                txtDescripcion.Text = personaje.Descripcion;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Escribe un nombre para el personaje.");
                return;
            }

            // Validación de stats
            if (!int.TryParse(txtAtaque.Text, out int ataque) || ataque < 0 || ataque > 100 ||
                !int.TryParse(txtDefensa.Text, out int defensa) || defensa < 0 || defensa > 100 ||
                !int.TryParse(txtResistencia.Text, out int resistencia) || resistencia < 1 || resistencia > 100)
            {
                MessageBox.Show("Ataque y defensa deben estar entre 0 y 100. Resistencia entre 1 y 100.");
                return;
            }

            var nuevo = new Personaje
            {
                Nombre = nombre,
                Ataque = ataque,
                Defensa = defensa,
                Resistencia = resistencia,
                Tecnica = ataque, // Usamos ataque como técnica por defecto
                Descripcion = txtDescripcion.Text.Trim()
            };

            if (!_controller.CrearPersonaje(nuevo, out string error))
            {
                MessageBox.Show("Error: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CargarPersonajesEnGrid();
            MessageBox.Show("¡Luchador creado!");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPersonajes.CurrentRow?.DataBoundItem is Personaje personaje)
            {
                DialogResult confirmar = MessageBox.Show($"¿Retirar a {personaje.Nombre}?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirmar == DialogResult.Yes)
                {
                    if (_controller.EliminarPersonaje(personaje.Id, out string error))
                    {
                        CargarPersonajesEnGrid();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar: " + error);
                    }
                }
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
{
    // 1. Verificar si hay un personaje seleccionado en la tabla
    if (dgvPersonajes.CurrentRow?.DataBoundItem is Personaje personaje)
    {
        // 2. REGLA DE NEGOCIO: Si el ID es 9 o menor, es predeterminado
        if (personaje.Id <= 9)
        {
            MessageBox.Show("No se pueden modificar los personajes predeterminados (ID 9 o menor).", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 3. Validar los datos ingresados en los campos de texto
        string nombre = txtNombre.Text.Trim();
        if (string.IsNullOrWhiteSpace(nombre))
        {
            MessageBox.Show("Escribe un nombre para el personaje.");
            return;
        }

        if (!int.TryParse(txtAtaque.Text, out int ataque) || ataque < 0 || ataque > 100 ||
            !int.TryParse(txtDefensa.Text, out int defensa) || defensa < 0 || defensa > 100 ||
            !int.TryParse(txtResistencia.Text, out int resistencia) || resistencia < 1 || resistencia > 100)
        {
            MessageBox.Show("Ataque y defensa deben estar entre 0 y 100. Resistencia entre 1 y 100.");
            return;
        }

        // 4. Crear el objeto con los datos modificados y mantener el mismo ID
        var personajeModificado = new Personaje
        {
            Id = personaje.Id,
            Nombre = nombre,
            Ataque = ataque,
            Defensa = defensa,
            Resistencia = resistencia,
            Tecnica = ataque, // Asigna técnica por defecto
            Descripcion = txtDescripcion.Text.Trim()
        };

        // 5. Enviar la actualización al controlador
        if (!_controller.ActualizarPersonaje(personajeModificado, out string error))
        {
            MessageBox.Show("Error al modificar: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 6. Refrescar la tabla para mostrar los cambios y avisar al usuario
        CargarPersonajesEnGrid();
        MessageBox.Show("¡Luchador modificado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else
    {
        MessageBox.Show("Selecciona un luchador de la lista primero.");
    }
}

        private void btnEscogerPj_Click(object sender, EventArgs e)
        {
            if (dgvPersonajes.CurrentRow?.DataBoundItem is Personaje personaje)
            {
                PersonajeSeleccionado = personaje;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Selecciona un luchador primero.");
            }
        }
    }
}
