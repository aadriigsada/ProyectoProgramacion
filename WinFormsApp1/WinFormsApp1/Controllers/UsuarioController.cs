using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class UsuarioController
    {
        // Pon aquí tus datos reales de MySQL. He puesto la base de datos "uefete_db" que me pasaste.
        private string cadenaConexion = "Server=localhost;Database=uefete_db;Uid=root;Pwd=;";

        public bool RegistrarUsuario(Usuario nuevoUsuario)
        {
            try
            {
                // El bloque 'using' crea la conexión y la cierra sola al terminar. ¡Cero errores!
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    // Añadimos el email a la consulta porque tu BD lo exige
                    string query = "INSERT INTO usuarios (nombre, email, password) VALUES (@nombre, @email, @pass)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre);
                        cmd.Parameters.AddWithValue("@email", nuevoUsuario.Email);
                        cmd.Parameters.AddWithValue("@pass", nuevoUsuario.Password);

                        conexion.Open(); // Abrimos conexión
                        int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecutamos el guardado

                        return filasAfectadas > 0; // Si afectó a 1 fila, es que se guardó bien
                    }
                }
            }
            catch (Exception ex)
            {
                // Si MySQL se queja (por ejemplo, si el usuario ya existe), nos lo dirá aquí
                MessageBox.Show("Fallo en el servidor: " + ex.Message);
                return false;
            }
        }
    }
}