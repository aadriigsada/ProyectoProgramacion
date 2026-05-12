using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class PersonajeController
    {
        private string cadenaConexion = "Server=localhost;Database=uefete_db;Uid=root;Pwd=rmkZ;SslMode=Disabled;AllowPublicKeyRetrieval=True;";

        public List<Personaje> ObtenerPersonajes()
        {
            var lista = new List<Personaje>();
            try
            {
                using (var con = new MySqlConnection(cadenaConexion))
                {
                    string query = "SELECT id, nombre, ataque, defensa, resistencia FROM personajes";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Personaje
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    Ataque = reader.GetInt32("ataque"),
                                    Defensa = reader.GetInt32("defensa"),
                                    Resistencia = reader.GetInt32("resistencia")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando personajes: " + ex.Message);
            }
            return lista;
        }
    }
}