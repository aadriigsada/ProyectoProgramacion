using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public partial class Menu1vs1 : Form
    {
        private readonly string _usuario;
        private const string ConnectionString = "server=localhost;database=uefete_db;user=root;password=rmkZ;AllowPublicKeyRetrieval=True;";

        public Menu1vs1(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;

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
            else
            {
                ActualizarEstadisticas(idPersonaje, labelNombre2, labelPS2, labelATQ2, labelDEF2);
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

        private void Menu1vs1_Load(object sender, EventArgs e)
        {
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        private void Combate(object sender, EventArgs e)
        {
            string p1 = labelNombre.Text.Trim();
            string p2 = labelNombre2.Text.Trim();

            bool p1Vacio = string.IsNullOrEmpty(p1) || p1 == "↻ Loading..." || p1 == "Nombre";
            bool p2Vacio = string.IsNullOrEmpty(p2) || p2 == "↻ Loading..." || p2 == "Nombre";

            if (p1Vacio || p2Vacio)
            {
                MessageBox.Show("¡Ambos jugadores deben seleccionar un luchador!",
                    "Selección incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            Personaje? pj1 = ObtenerPersonajePorNombre(p1);
            Personaje? pj2 = ObtenerPersonajePorNombre(p2);

            if (pj1 is null || pj2 is null)
            {
                MessageBox.Show("No se pudieron cargar los datos completos de los luchadores.",
                    "Error de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Hide();
            using CronicaCombateForm cronica = new CronicaCombateForm(pj1, pj2, ModoCombate.UnoVsUno, _usuario);
            cronica.ShowDialog();
            Show();
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
