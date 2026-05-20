using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class PersonajeController
    {
        private readonly string cadenaConexion = "Server=localhost;Database=uefete_db;Uid=root;Pwd=rmkZ;SslMode=Disabled;AllowPublicKeyRetrieval=True;";

        public List<Personaje> ObtenerPersonajes()
        {
            var lista = new List<Personaje>();
            try
            {
                using MySqlConnection con = new MySqlConnection(cadenaConexion);
                con.Open();

                if (!TablaExiste(con, "personajes"))
                {
                    return lista;
                }

                string? colId = ResolverColumna(con, "personajes", "id_personaje", "id");
                string? colNombre = ResolverColumna(con, "personajes", "nombre");
                string? colAtaque = ResolverColumna(con, "personajes", "ataque", "fuerza");
                string? colDefensa = ResolverColumna(con, "personajes", "defensa");
                string? colResistencia = ResolverColumna(con, "personajes", "resistencia");
                string? colTecnica = ResolverColumna(con, "personajes", "tecnica");
                string? colDescripcion = ResolverColumna(con, "personajes", "descripcion", "descripcion_personaje");

                if (string.IsNullOrWhiteSpace(colNombre))
                {
                    return lista;
                }

                var columnas = new List<string>();
                if (!string.IsNullOrWhiteSpace(colId)) columnas.Add($"{colId} AS id_personaje");
                columnas.Add($"{colNombre} AS nombre");
                if (!string.IsNullOrWhiteSpace(colAtaque)) columnas.Add($"{colAtaque} AS ataque");
                if (!string.IsNullOrWhiteSpace(colDefensa)) columnas.Add($"{colDefensa} AS defensa");
                if (!string.IsNullOrWhiteSpace(colResistencia)) columnas.Add($"{colResistencia} AS resistencia");
                if (!string.IsNullOrWhiteSpace(colTecnica)) columnas.Add($"{colTecnica} AS tecnica");
                if (!string.IsNullOrWhiteSpace(colDescripcion)) columnas.Add($"{colDescripcion} AS descripcion");

                string orden = !string.IsNullOrWhiteSpace(colId) ? colId : colNombre;
                string query = $"SELECT {string.Join(", ", columnas)} FROM personajes ORDER BY {orden} DESC";

                using MySqlCommand cmd = new MySqlCommand(query, con);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Personaje
                    {
                        Id = LeerEntero(reader, "id_personaje", "id"),
                        Nombre = reader["nombre"]?.ToString() ?? string.Empty,
                        Ataque = LeerEntero(reader, "ataque"),
                        Defensa = LeerEntero(reader, "defensa"),
                        Resistencia = LeerEntero(reader, "resistencia"),
                        Tecnica = LeerEntero(reader, "tecnica"),
                        Descripcion = LeerTexto(reader, "descripcion") ?? string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando personajes: " + ex.Message);
            }

            return lista;
        }

        public bool CrearPersonaje(Personaje personaje, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (personaje.Ataque < 0 || personaje.Ataque > 100 ||
                personaje.Defensa < 0 || personaje.Defensa > 100 ||
                personaje.Resistencia < 1 || personaje.Resistencia > 100)
            {
                errorMessage = "Ataque y defensa deben estar entre 0 y 100. Resistencia entre 1 y 100.";
                return false;
            }

            try
            {
                using MySqlConnection con = new MySqlConnection(cadenaConexion);
                con.Open();

                if (!TablaExiste(con, "personajes"))
                {
                    errorMessage = "No existe la tabla personajes en la base de datos.";
                    return false;
                }

                string? colNombre = ResolverColumna(con, "personajes", "nombre");
                string? colAtaque = ResolverColumna(con, "personajes", "ataque", "fuerza");
                string? colDefensa = ResolverColumna(con, "personajes", "defensa");
                string? colResistencia = ResolverColumna(con, "personajes", "resistencia");
                string? colTecnica = ResolverColumna(con, "personajes", "tecnica");
                string? colVelocidad = ResolverColumna(con, "personajes", "velocidad");
                string? colDescripcion = ResolverColumna(con, "personajes", "descripcion", "descripcion_personaje");
                string? colPredefinido = ResolverColumna(con, "personajes", "es_predefinido");
                string? colPropietario = ResolverColumna(con, "personajes", "id_propietario");
                string? colIdPersonaje = ResolverColumna(con, "personajes", "id_personaje");

                if (string.IsNullOrWhiteSpace(colNombre))
                {
                    errorMessage = "La tabla personajes no tiene columna de nombre.";
                    return false;
                }

                var columnas = new List<string> { colNombre };
                var valores = new List<string> { "@nombre" };

                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@nombre", personaje.Nombre.Trim());

                if (!string.IsNullOrWhiteSpace(colAtaque))
                {
                    columnas.Add(colAtaque);
                    valores.Add("@ataque");
                    cmd.Parameters.AddWithValue("@ataque", personaje.Ataque);
                }

                if (!string.IsNullOrWhiteSpace(colDefensa))
                {
                    columnas.Add(colDefensa);
                    valores.Add("@defensa");
                    cmd.Parameters.AddWithValue("@defensa", personaje.Defensa);
                }

                if (!string.IsNullOrWhiteSpace(colResistencia))
                {
                    columnas.Add(colResistencia);
                    valores.Add("@resistencia");
                    cmd.Parameters.AddWithValue("@resistencia", personaje.Resistencia);
                }

                if (!string.IsNullOrWhiteSpace(colTecnica))
                {
                    columnas.Add(colTecnica);
                    valores.Add("@tecnica");
                    cmd.Parameters.AddWithValue("@tecnica", personaje.Tecnica <= 0 ? personaje.Ataque : personaje.Tecnica);
                }

                if (!string.IsNullOrWhiteSpace(colVelocidad))
                {
                    columnas.Add(colVelocidad);
                    valores.Add("@velocidad");
                    cmd.Parameters.AddWithValue("@velocidad", 50);
                }

                if (!string.IsNullOrWhiteSpace(colDescripcion))
                {
                    columnas.Add(colDescripcion);
                    valores.Add("@descripcion");
                    cmd.Parameters.AddWithValue("@descripcion", personaje.Descripcion ?? string.Empty);
                }

                if (!string.IsNullOrWhiteSpace(colPredefinido))
                {
                    columnas.Add(colPredefinido);
                    valores.Add("0");
                }

                if (!string.IsNullOrWhiteSpace(colPropietario))
                {
                    columnas.Add(colPropietario);
                    valores.Add("NULL");
                }

                if (!string.IsNullOrWhiteSpace(colIdPersonaje) &&
                    DebeAsignarIdManual(con, "personajes", colIdPersonaje))
                {
                    int siguienteId = ObtenerSiguienteId(con, "personajes", colIdPersonaje);
                    columnas.Add(colIdPersonaje);
                    valores.Add("@id_personaje");
                    cmd.Parameters.AddWithValue("@id_personaje", siguienteId);
                }

                cmd.CommandText = $"INSERT INTO personajes ({string.Join(", ", columnas)}) VALUES ({string.Join(", ", valores)})";
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool EliminarPersonaje(int idPersonaje, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using MySqlConnection con = new MySqlConnection(cadenaConexion);
                con.Open();

                string? colId = ResolverColumna(con, "personajes", "id_personaje", "id");
                if (string.IsNullOrWhiteSpace(colId))
                {
                    errorMessage = "No se encontró la columna ID en personajes.";
                    return false;
                }

                using MySqlCommand cmd = new MySqlCommand($"DELETE FROM personajes WHERE {colId} = @id", con);
                cmd.Parameters.AddWithValue("@id", idPersonaje);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
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

        private static string? LeerTexto(MySqlDataReader reader, string columna)
        {
            if (!TieneColumna(reader, columna) || reader[columna] == DBNull.Value)
            {
                return null;
            }

            return reader[columna]?.ToString();
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

        private string? ResolverColumna(MySqlConnection conexion, string tabla, params string[] candidatas)
        {
            foreach (string candidata in candidatas)
            {
                if (ColumnaExiste(conexion, tabla, candidata))
                {
                    return candidata;
                }
            }

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

        private bool DebeAsignarIdManual(MySqlConnection conexion, string tabla, string columna)
        {
            const string query = @"SELECT IS_NULLABLE, COLUMN_DEFAULT, EXTRA
                                   FROM INFORMATION_SCHEMA.COLUMNS
                                   WHERE TABLE_SCHEMA = DATABASE()
                                     AND TABLE_NAME = @tabla
                                     AND COLUMN_NAME = @columna
                                   LIMIT 1";

            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@tabla", tabla);
            cmd.Parameters.AddWithValue("@columna", columna);

            using MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return false;
            }

            string isNullable = reader["IS_NULLABLE"]?.ToString() ?? "YES";
            string extra = reader["EXTRA"]?.ToString() ?? string.Empty;
            bool tieneDefault = reader["COLUMN_DEFAULT"] != DBNull.Value;

            bool esAutoIncrement = extra.IndexOf("auto_increment", StringComparison.OrdinalIgnoreCase) >= 0;
            bool esNotNullSinDefault = string.Equals(isNullable, "NO", StringComparison.OrdinalIgnoreCase) && !tieneDefault;

            return !esAutoIncrement && esNotNullSinDefault;
        }

        private int ObtenerSiguienteId(MySqlConnection conexion, string tabla, string columna)
        {
            string query = $"SELECT COALESCE(MAX({columna}), 0) + 1 FROM {tabla}";
            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            object? valor = cmd.ExecuteScalar();
            return Convert.ToInt32(valor);
        }

        public bool ActualizarPersonaje(Personaje personaje, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validación extra de seguridad (ID > 9)
            if (personaje.Id <= 9)
            {
                errorMessage = "No se pueden modificar personajes predeterminados.";
                return false;
            }

            try
            {
                using MySqlConnection con = new MySqlConnection(cadenaConexion);
                con.Open();

                // Resolvemos dinámicamente los nombres de las columnas en tu base de datos
                string? colId = ResolverColumna(con, "personajes", "id_personaje", "id");
                string? colNombre = ResolverColumna(con, "personajes", "nombre");
                string? colAtaque = ResolverColumna(con, "personajes", "ataque", "fuerza");
                string? colDefensa = ResolverColumna(con, "personajes", "defensa");
                string? colResistencia = ResolverColumna(con, "personajes", "resistencia");
                string? colTecnica = ResolverColumna(con, "personajes", "tecnica");
                string? colDescripcion = ResolverColumna(con, "personajes", "descripcion", "descripcion_personaje");

                if (string.IsNullOrWhiteSpace(colId))
                {
                    errorMessage = "No se encontró la columna ID en la tabla de personajes.";
                    return false;
                }

                // Construcción dinámica del query de actualización
                var sets = new List<string>();
                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;

                if (!string.IsNullOrWhiteSpace(colNombre))
                {
                    sets.Add($"{colNombre} = @nombre");
                    cmd.Parameters.AddWithValue("@nombre", personaje.Nombre.Trim());
                }
                if (!string.IsNullOrWhiteSpace(colAtaque))
                {
                    sets.Add($"{colAtaque} = @ataque");
                    cmd.Parameters.AddWithValue("@ataque", personaje.Ataque);
                }
                if (!string.IsNullOrWhiteSpace(colDefensa))
                {
                    sets.Add($"{colDefensa} = @defensa");
                    cmd.Parameters.AddWithValue("@defensa", personaje.Defensa);
                }
                if (!string.IsNullOrWhiteSpace(colResistencia))
                {
                    sets.Add($"{colResistencia} = @resistencia");
                    cmd.Parameters.AddWithValue("@resistencia", personaje.Resistencia);
                }
                if (!string.IsNullOrWhiteSpace(colTecnica))
                {
                    sets.Add($"{colTecnica} = @tecnica");
                    cmd.Parameters.AddWithValue("@tecnica", personaje.Tecnica);
                }
                if (!string.IsNullOrWhiteSpace(colDescripcion))
                {
                    sets.Add($"{colDescripcion} = @descripcion");
                    cmd.Parameters.AddWithValue("@descripcion", personaje.Descripcion ?? string.Empty);
                }

                if (sets.Count == 0)
                {
                    return true;
                }

                cmd.CommandText = $"UPDATE personajes SET {string.Join(", ", sets)} WHERE {colId} = @id";
                cmd.Parameters.AddWithValue("@id", personaje.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}

