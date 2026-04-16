using System;
using MySql.Data.MySqlClient;

namespace WinFormsApp1.Database
{
    public class Conexion
    {
        private readonly string _connectionString;

        public Conexion(string? connectionString = null)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? BuildConnectionStringFromEnvironment()
                : connectionString;
        }

        public bool TryOpenConnection(out MySqlConnection? connection, out string errorMessage)
        {
            connection = null;
            errorMessage = string.Empty;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                connection?.Dispose();
                connection = null;
                errorMessage = $"No se pudo conectar con MySQL ({ex.Number}): {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                connection?.Dispose();
                connection = null;
                errorMessage = $"Error inesperado al abrir la conexion: {ex.Message}";
                return false;
            }
        }

        public bool CanConnect(out string errorMessage)
        {
            if (!TryOpenConnection(out var connection, out errorMessage))
            {
                return false;
            }

            connection?.Dispose();
            return true;
        }

        private static string BuildConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("UEFETE_DB_SERVER") ?? "localhost";
            var database = Environment.GetEnvironmentVariable("UEFETE_DB_NAME") ?? "uefete";
            var user = Environment.GetEnvironmentVariable("UEFETE_DB_USER") ?? "root";
            var password = Environment.GetEnvironmentVariable("UEFETE_DB_PASSWORD") ?? string.Empty;

            var port = 3306u;
            var portRaw = Environment.GetEnvironmentVariable("UEFETE_DB_PORT");
            if (!string.IsNullOrWhiteSpace(portRaw) && uint.TryParse(portRaw, out var parsedPort))
            {
                port = parsedPort;
            }

            var builder = new MySqlConnectionStringBuilder
            {
                Server = server,
                Port = port,
                Database = database,
                UserID = user,
                Password = password,
                SslMode = MySqlSslMode.Disabled,
                AllowUserVariables = true
            };

            return builder.ConnectionString;
        }
    }
}
