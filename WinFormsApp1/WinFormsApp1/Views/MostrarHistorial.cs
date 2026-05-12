using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.Views
{
    public partial class MostrarHistorial : Form
    {
        private readonly string _usuario;
        private readonly UsuarioController _controller = new UsuarioController();
        private const string ResumenVacio = "Sin combates registrados todavia.";
        private readonly Color _colorResumenNormal = Color.FromArgb(240, 229, 192);
        private readonly Color _colorExito = Color.FromArgb(120, 230, 140);

        public MostrarHistorial(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            Text = $"Historial de {usuario}";
            lblTitulo.Text = $"Historial de combates - {usuario}";

            ConfigurarGrid();
            CargarHistorial();
        }

        private void ConfigurarGrid()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void CargarHistorial()
        {
            DataTable tabla = _controller.ObtenerHistorialTabla(_usuario);
            dataGridView1.DataSource = tabla;

            if (tabla.Rows.Count == 0)
            {
                lblResumen.Text = ResumenVacio;
                return;
            }

            RenombrarColumnas();
            ActualizarResumen(tabla);
        }

        private void RenombrarColumnas()
        {
            if (dataGridView1.Columns["fecha"] is DataGridViewColumn colFecha)
            {
                colFecha.HeaderText = "Fecha";
                colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                colFecha.FillWeight = 25;
            }

            if (dataGridView1.Columns["personaje"] is DataGridViewColumn colPersonaje)
            {
                colPersonaje.HeaderText = "Personaje";
                colPersonaje.FillWeight = 20;
            }

            if (dataGridView1.Columns["rival"] is DataGridViewColumn colRival)
            {
                colRival.HeaderText = "Rival";
                colRival.FillWeight = 20;
            }

            if (dataGridView1.Columns["resultado"] is DataGridViewColumn colResultado)
            {
                colResultado.HeaderText = "Resultado";
                colResultado.FillWeight = 15;
            }

            if (dataGridView1.Columns["detalle"] is DataGridViewColumn colDetalle)
            {
                colDetalle.HeaderText = "Detalle";
                colDetalle.FillWeight = 40;
            }
        }

        private void ActualizarResumen(DataTable tabla)
        {
            int total = tabla.Rows.Count;
            int victorias = 0;
            int derrotas = 0;
            int empates = 0;

            foreach (DataRow row in tabla.Rows)
            {
                string resultado = tabla.Columns.Contains("resultado")
                    ? (row["resultado"]?.ToString() ?? string.Empty)
                    : string.Empty;

                if (resultado.Equals("Victoria", StringComparison.OrdinalIgnoreCase))
                {
                    victorias++;
                }
                else if (resultado.Equals("Derrota", StringComparison.OrdinalIgnoreCase))
                {
                    derrotas++;
                }
                else if (resultado.Equals("Empate", StringComparison.OrdinalIgnoreCase))
                {
                    empates++;
                }
            }

            lblResumen.Text = $"Total: {total} | Victorias: {victorias} | Derrotas: {derrotas} | Empates: {empates}";
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            btnRefrescar.Enabled = false;
            string resumenAnterior = lblResumen.Text;
            lblResumen.ForeColor = _colorResumenNormal;
            lblResumen.Text = "Recargando historial...";

            CargarHistorial();
            await MostrarFeedbackRecargaAsync(resumenAnterior);
            btnRefrescar.Enabled = true;
        }

        private async Task MostrarFeedbackRecargaAsync(string resumenAnterior)
        {
            bool sinCambios = lblResumen.Text == resumenAnterior;
            string resumenActual = lblResumen.Text;

            lblResumen.ForeColor = _colorExito;
            lblResumen.Text = sinCambios
                ? $"{resumenActual}  |  Sin cambios"
                : $"{resumenActual}  |  Historial actualizado";

            await Task.Delay(1200);
            lblResumen.ForeColor = _colorResumenNormal;
            lblResumen.Text = resumenActual;
        }
    }
}
