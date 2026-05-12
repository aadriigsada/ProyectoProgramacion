using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class UsuarioController
    {
        private readonly string cadenaConexion = "Server=localhost;Database=uefete_db;Uid=root;Pwd=rmkZ;SslMode=Disabled;AllowPublicKeyRetrieval=True;";

        public bool RegistrarUsuario(Usuario nuevoUsuario)
        {
            try
            {
                using MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                string? columnaUsuario = ObtenerColumnaNombreUsuario(conexion);
                if (string.IsNullOrWhiteSpace(columnaUsuario))
                {
                    MessageBox.Show("No se encontró la columna de usuario en la tabla usuarios.");
                    return false;
                }

                string query = $"INSERT INTO usuarios ({columnaUsuario}, password, email) VALUES (@nombre, @pass, @email)";
                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre);
                cmd.Parameters.AddWithValue("@pass", nuevoUsuario.Password);
                cmd.Parameters.AddWithValue("@email", nuevoUsuario.Email);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo en el servidor al registrar: " + ex.Message);
                return false;
            }
        }

        public bool ValidarLogin(string usuario, string pass)
        {
            try
            {
                using MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                string? columnaUsuario = ObtenerColumnaNombreUsuario(conexion);
                if (string.IsNullOrWhiteSpace(columnaUsuario))
                {
                    MessageBox.Show("No se encontró la columna de usuario en la tabla usuarios.");
                    return false;
                }

                string query = $"SELECT COUNT(*) FROM usuarios WHERE {columnaUsuario} = @user AND password = @pass";
                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pass", pass);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar: " + ex.Message);
                return false;
            }
        }

        public bool GuardarCombate(string usuario, string nombrePersonaje, string rival, string resultado, string detalle)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return false;
            }

            try
            {
                using MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (!TablaExiste(conexion, "historial"))
                {
                    return false;
                }

                int? idUsuario = ObtenerIdUsuario(conexion, usuario);
                if (!idUsuario.HasValue)
                {
                    return false;
                }

                var columnas = new List<string>();
                var valores = new List<string>();
                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;

                if (ColumnaExiste(conexion, "historial", "id_usuario"))
                {
                    columnas.Add("id_usuario");
                    valores.Add("@id_usuario");
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario.Value);
                }

                if (ColumnaExiste(conexion, "historial", "id_personaje"))
                {
                    columnas.Add("id_personaje");
                    valores.Add("NULL");
                }

                if (ColumnaExiste(conexion, "historial", "nombre_personaje"))
                {
                    columnas.Add("nombre_personaje");
                    valores.Add("@nombre_personaje");
                    cmd.Parameters.AddWithValue("@nombre_personaje", nombrePersonaje);
                }

                if (ColumnaExiste(conexion, "historial", "rival"))
                {
                    columnas.Add("rival");
                    valores.Add("@rival");
                    cmd.Parameters.AddWithValue("@rival", rival);
                }

                if (ColumnaExiste(conexion, "historial", "resultado"))
                {
                    columnas.Add("resultado");
                    valores.Add("@resultado");
                    cmd.Parameters.AddWithValue("@resultado", resultado);
                }

                if (ColumnaExiste(conexion, "historial", "detalle"))
                {
                    columnas.Add("detalle");
                    valores.Add("@detalle");
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                }

                if (ColumnaExiste(conexion, "historial", "fecha"))
                {
                    columnas.Add("fecha");
                    valores.Add("NOW()");
                }

                if (columnas.Count == 0)
                {
                    return false;
                }

                cmd.CommandText = $"INSERT INTO historial ({string.Join(", ", columnas)}) VALUES ({string.Join(", ", valores)})";
                int filas = cmd.ExecuteNonQuery();
                return filas > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el combate en el historial: " + ex.Message);
                return false;
            }
        }

        public List<string> ObtenerHistorial(string usuario)
        {
            var lista = new List<string>();
            DataTable tabla = ObtenerHistorialTabla(usuario);

            foreach (DataRow row in tabla.Rows)
            {
                string fecha = tabla.Columns.Contains("fecha")
                    ? Convert.ToDateTime(row["fecha"]).ToString("dd/MM/yyyy HH:mm")
                    : "Sin fecha";

                string personaje = tabla.Columns.Contains("personaje") ? row["personaje"].ToString() ?? "-" : "-";
                string rival = tabla.Columns.Contains("rival") ? row["rival"].ToString() ?? "-" : "-";
                string resultado = tabla.Columns.Contains("resultado") ? row["resultado"].ToString() ?? "-" : "-";

                lista.Add($"{fecha} | {personaje} vs {rival} -> {resultado}");
            }

            return lista;
        }

        public DataTable ObtenerHistorialTabla(string usuario)
        {
            var tabla = new DataTable();

            try
            {
                using MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (!TablaExiste(conexion, "historial"))
                {
                    return tabla;
                }

                int? idUsuario = ObtenerIdUsuario(conexion, usuario);
                if (!idUsuario.HasValue)
                {
                    return tabla;
                }

                string? columnaFkUsuario = ObtenerColumnaFkHistorialUsuario(conexion);
                if (string.IsNullOrWhiteSpace(columnaFkUsuario))
                {
                    return tabla;
                }

                var columnasSelect = new List<string>();
                if (ColumnaExiste(conexion, "historial", "fecha")) columnasSelect.Add("h.fecha AS fecha");
                if (ColumnaExiste(conexion, "historial", "nombre_personaje")) columnasSelect.Add("h.nombre_personaje AS personaje");
                if (ColumnaExiste(conexion, "historial", "rival")) columnasSelect.Add("h.rival AS rival");
                if (ColumnaExiste(conexion, "historial", "resultado")) columnasSelect.Add("h.resultado AS resultado");
                if (ColumnaExiste(conexion, "historial", "detalle")) columnasSelect.Add("h.detalle AS detalle");

                if (columnasSelect.Count == 0)
                {
                    columnasSelect.Add("h.*");
                }

                string orden = ColumnaExiste(conexion, "historial", "fecha") ? "ORDER BY h.fecha DESC" : string.Empty;
                string query = $"SELECT {string.Join(", ", columnasSelect)} FROM historial h WHERE h.{columnaFkUsuario} = @idUsuario {orden}";

                using MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario.Value);

                using MySqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }

            return tabla;
        }

        public DataTable ObtenerPrimerPersonaje()
        {
            var tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    string query = "SELECT nombre, resistencia, ataque, defensa FROM personajes LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            tabla.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del personaje: " + ex.Message);
            }
            return tabla;
        }

        private int? ObtenerIdUsuario(MySqlConnection conexion, string usuario)
        {
            string? columnaUsuario = ObtenerColumnaNombreUsuario(conexion);
            string? columnaId = ObtenerColumnaIdUsuario(conexion);

            if (string.IsNullOrWhiteSpace(columnaUsuario) || string.IsNullOrWhiteSpace(columnaId))
            {
                return null;
            }

            string query = $"SELECT {columnaId} FROM usuarios WHERE {columnaUsuario} = @user LIMIT 1";
            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@user", usuario);

            object? valor = cmd.ExecuteScalar();
            if (valor is null || valor == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(valor);
        }

        private string? ObtenerColumnaNombreUsuario(MySqlConnection conexion)
        {
            if (ColumnaExiste(conexion, "usuarios", "username")) return "username";
            if (ColumnaExiste(conexion, "usuarios", "nombre")) return "nombre";
            return null;
        }

        private string? ObtenerColumnaIdUsuario(MySqlConnection conexion)
        {
            if (ColumnaExiste(conexion, "usuarios", "id_usuario")) return "id_usuario";
            if (ColumnaExiste(conexion, "usuarios", "id")) return "id";
            return null;
        }

        private string? ObtenerColumnaFkHistorialUsuario(MySqlConnection conexion)
        {
            if (ColumnaExiste(conexion, "historial", "id_usuario")) return "id_usuario";
            if (ColumnaExiste(conexion, "historial", "usuario_id")) return "usuario_id";
            if (ColumnaExiste(conexion, "historial", "id_user")) return "id_user";
            return null;
        }

        private bool TablaExiste(MySqlConnection conexion, string tabla)
        {
            const string query = @"SELECT COUNT(*)
                                   FROM INFORMATION_SCHEMA.TABLES
                                   WHERE TABLE_SCHEMA = DATABASE()
                                     AND TABLE_NAME = @tabla";

            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@tabla", tabla);
            int total = Convert.ToInt32(cmd.ExecuteScalar());
            return total > 0;
        }

        private bool ColumnaExiste(MySqlConnection conexion, string tabla, string columna)
        {
            const string query = @"SELECT COUNT(*)
                                   FROM INFORMATION_SCHEMA.COLUMNS
                                   WHERE TABLE_SCHEMA = DATABASE()
                                     AND TABLE_NAME = @tabla
                                     AND COLUMN_NAME = @columna";

            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@tabla", tabla);
            cmd.Parameters.AddWithValue("@columna", columna);
            int total = Convert.ToInt32(cmd.ExecuteScalar());
            return total > 0;
        }
    }
}
