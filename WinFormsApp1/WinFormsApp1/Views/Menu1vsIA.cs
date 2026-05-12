using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public partial class Menu1vsIA : Form
    {
        private readonly string _usuario;
        private readonly Random _random = new Random();
        private readonly Image?[] _imagenesRuleta;
        private bool _sorteoEnCurso;
        private DateTime _inicioRuletaUtc;

        private const int DuracionRuletaMs = 2000;
        private const int IntervaloInicialMs = 55;
        private const int IntervaloFinalMs = 260;

        private const string ConnectionString = "server=localhost;database=uefete_db;user=root;password=rmkZ;AllowPublicKeyRetrieval=True;";
        private string nombreIAFinal = string.Empty;

        public Menu1vsIA(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            _imagenesRuleta = CargarImagenesRuleta();
            pBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                string rutaCursor = Path.Combine(Application.StartupPath, "BoxingGlove.cur");
                if (File.Exists(rutaCursor))
                {
                    IntPtr cursorHandle = LoadCursorFromFile(rutaCursor);
                    if (cursorHandle != IntPtr.Zero)
                    {
                        Cursor = new Cursor(cursorHandle);
                    }
                }
            }
            catch
            {
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (sender is not PictureBox pb)
            {
                return;
            }

            string idString = System.Text.RegularExpressions.Regex.Match(pb.Name, @"\d+").Value;
            if (!int.TryParse(idString, out int idPersonaje))
            {
                return;
            }

            if (idPersonaje <= 9)
            {
                ActualizarEstadisticas(idPersonaje, labelNombre, labelPS, labelATQ, labelDEF);
            }
        }

        private void ActualizarEstadisticas(int id, Label lblNom, Label lblPs, Label lblAtq, Label lblDef)
        {
            using MySqlConnection conexion = new MySqlConnection(ConnectionString);
            try
            {
                conexion.Open();
                int idReal = id > 9 ? id - 9 : id;

                const string query = "SELECT nombre, resistencia, ataque, defensa FROM personajes WHERE id_personaje = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", idReal);

                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return;
                }

                lblNom.Text = reader["nombre"].ToString();
                lblPs.Text = reader["resistencia"].ToString();
                lblAtq.Text = reader["ataque"].ToString();
                lblDef.Text = reader["defensa"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void labelPS_Click(object sender, EventArgs e)
        {
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        private void Combate(object sender, EventArgs e)
        {
            string p1 = labelNombre.Text.Trim();
            bool p1Vacio = string.IsNullOrEmpty(p1) || p1 == "↻ Loading..." || p1 == "? Loading..." || p1 == "Nombre";

            if (p1Vacio)
            {
                MessageBox.Show("¡Tienes que escoger un jugador!",
                                "Seleccion incompleta",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                return;
            }

            Personaje? pj1 = ObtenerPersonajePorNombre(p1);
            Personaje? pjIA = string.IsNullOrWhiteSpace(nombreIAFinal)
                ? ObtenerRivalAleatorio(p1)
                : ObtenerPersonajePorNombre(nombreIAFinal);

            if (pj1 is null || pjIA is null)
            {
                MessageBox.Show("No se pudieron cargar los datos completos del combate.",
                                "Error de datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            Hide();
            using CronicaCombateForm cronica = new CronicaCombateForm(pj1, pjIA, ModoCombate.UnoVsIA, _usuario);
            cronica.ShowDialog();
            Show();
        }

        private void btnSortear_Click(object sender, EventArgs e)
        {
            if (_sorteoEnCurso)
            {
                return;
            }

            _sorteoEnCurso = true;
            nombreIAFinal = string.Empty;
            btnSortear.Enabled = false;
            _inicioRuletaUtc = DateTime.UtcNow;

            timerIA.Stop();
            timerIA.Interval = IntervaloInicialMs;
            ActualizarImagenAleatoria();
            timerIA.Start();
        }

        private void timerIA_Tick(object sender, EventArgs e)
        {
            if (!_sorteoEnCurso)
            {
                return;
            }

            ActualizarImagenAleatoria();

            double transcurridoMs = (DateTime.UtcNow - _inicioRuletaUtc).TotalMilliseconds;
            if (transcurridoMs >= DuracionRuletaMs)
            {
                FinalizarSorteoIA();
                return;
            }

            double progreso = transcurridoMs / DuracionRuletaMs;
            int intervaloActual = IntervaloInicialMs + (int)((IntervaloFinalMs - IntervaloInicialMs) * progreso);
            timerIA.Interval = Math.Max(IntervaloInicialMs, Math.Min(IntervaloFinalMs, intervaloActual));
        }

        private void FinalizarSorteoIA()
        {
            timerIA.Stop();
            _sorteoEnCurso = false;

            int idElegido = _random.Next(1, 10);
            pBox1.Image = _imagenesRuleta[idElegido - 1];

            nombreIAFinal = ObtenerNombreIADesdeBD(idElegido);
            btnSortear.Enabled = true;
        }

        private void ActualizarImagenAleatoria()
        {
            int index = _random.Next(0, _imagenesRuleta.Length);
            pBox1.Image = _imagenesRuleta[index];
        }

        private string ObtenerNombreIADesdeBD(int idPersonaje)
        {
            using MySqlConnection conexion = new MySqlConnection(ConnectionString);
            try
            {
                conexion.Open();
                using MySqlCommand cmd = new MySqlCommand("SELECT nombre FROM personajes WHERE id_personaje = @id", conexion);
                cmd.Parameters.AddWithValue("@id", idPersonaje);
                object? resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value ? resultado.ToString() ?? string.Empty : string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener personaje IA: " + ex.Message,
                                "Error de base de datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private Image?[] CargarImagenesRuleta()
        {
            Image?[] imagenes = new Image?[9];

            for (int i = 1; i <= 9; i++)
            {
                string nombre = $"p{i}";
                object? recurso = Properties.Resources.ResourceManager.GetObject(nombre);
                if (recurso is Image imgRes)
                {
                    imagenes[i - 1] = imgRes;
                    continue;
                }

                imagenes[i - 1] = ObtenerImagenDesdeMiniatura(i);
            }

            return imagenes;
        }

        private Image? ObtenerImagenDesdeMiniatura(int id)
        {
            return id switch
            {
                1 => pictureBox1.BackgroundImage,
                2 => pictureBox2.BackgroundImage,
                3 => pictureBox3.BackgroundImage,
                4 => pictureBox4.BackgroundImage,
                5 => pictureBox5.BackgroundImage,
                6 => pictureBox6.BackgroundImage,
                7 => pictureBox7.BackgroundImage,
                8 => pictureBox8.BackgroundImage,
                9 => pictureBox9.BackgroundImage,
                _ => null
            };
        }

        private Personaje? ObtenerPersonajePorNombre(string nombre)
        {
            using MySqlConnection conexion = new MySqlConnection(ConnectionString);
            try
            {
                conexion.Open();
                const string query = "SELECT * FROM personajes WHERE nombre = @nombre LIMIT 1";
                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", nombre);

                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                int resistencia = Math.Max(1, LeerEntero(reader, "resistencia"));
                return new Personaje
                {
                    Id = LeerEntero(reader, "id", "id_personaje"),
                    Nombre = reader["nombre"]?.ToString() ?? nombre,
                    Ataque = LeerEntero(reader, "ataque", "fuerza"),
                    Defensa = LeerEntero(reader, "defensa"),
                    Tecnica = LeerEntero(reader, "tecnica"),
                    Resistencia = resistencia,
                    ResistenciaMax = resistencia * 4,
                    ResistenciaAct = resistencia * 4,
                    Defendiendo = false
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar personaje: " + ex.Message);
                return null;
            }
        }

        private Personaje? ObtenerRivalAleatorio(string nombreExcluido)
        {
            using MySqlConnection conexion = new MySqlConnection(ConnectionString);
            try
            {
                conexion.Open();
                const string query = "SELECT * FROM personajes WHERE nombre <> @nombre ORDER BY RAND() LIMIT 1";
                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", nombreExcluido);

                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                int resistencia = Math.Max(1, LeerEntero(reader, "resistencia"));
                return new Personaje
                {
                    Id = LeerEntero(reader, "id", "id_personaje"),
                    Nombre = reader["nombre"]?.ToString() ?? "IA",
                    Ataque = LeerEntero(reader, "ataque", "fuerza"),
                    Defensa = LeerEntero(reader, "defensa"),
                    Tecnica = LeerEntero(reader, "tecnica"),
                    Resistencia = resistencia,
                    ResistenciaMax = resistencia * 4,
                    ResistenciaAct = resistencia * 4,
                    Defendiendo = false
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar rival aleatorio: " + ex.Message);
                return null;
            }
        }

        private static int LeerEntero(MySqlDataReader reader, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (!TieneColumna(reader, columna))
                {
                    continue;
                }

                object valor = reader[columna];
                if (valor == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(valor);
            }

            return 0;
        }

        private static bool TieneColumna(MySqlDataReader reader, string columna)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columna, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
