using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public enum ModoCombate
    {
        UnoVsUno = 0,
        UnoVsIA = 1
    }

    public partial class CombateForm : Form
    {
        private readonly string _usuarioActual;
        private readonly string _nombreJugador;
        private readonly string _nombreRival;
        private readonly ModoCombate _modoCombate;

        private readonly CombateController _combateController = new();
        private readonly UsuarioController _usuarioController = new();

        private Personaje? _jugador1;
        private Personaje? _rival;
        private bool _turnoJugador1 = true;
        private bool _combateTerminado;
        private bool _historialGuardado;

        private const int DelayIATurnoMs = 850;
        private static readonly Color ColorTurnoJugador1 = Color.RoyalBlue;
        private static readonly Color ColorTurnoRojo = Color.Firebrick;

        public CombateForm(string p1, string p2) : this(p1, p2, ModoCombate.UnoVsUno, string.Empty)
        {
        }

        public CombateForm(string p1, string p2, ModoCombate modoCombate) : this(p1, p2, modoCombate, string.Empty)
        {
        }

        public CombateForm(string p1, string p2, ModoCombate modoCombate, string usuarioActual)
        {
            InitializeComponent();
            _nombreJugador = p1;
            _nombreRival = p2;
            _modoCombate = modoCombate;
            _usuarioActual = usuarioActual;
        }

        public CombateForm(Personaje p1, Personaje p2, ModoCombate modoCombate, string usuarioActual)
        {
            if (p1 is null) throw new ArgumentNullException(nameof(p1));
            if (p2 is null) throw new ArgumentNullException(nameof(p2));

            InitializeComponent();
            _nombreJugador = p1.Nombre;
            _nombreRival = p2.Nombre;
            _modoCombate = modoCombate;
            _usuarioActual = usuarioActual;
            _jugador1 = PrepararPersonajeParaCombate(p1);
            _rival = PrepararPersonajeParaCombate(p2);
        }

        private void CombateForm_Load(object sender, EventArgs e)
        {
            if (_jugador1 is null || _rival is null)
            {
                if (!CargarPersonajesDesdeBD())
                {
                    Close();
                    return;
                }
            }

            if (_jugador1 is null || _rival is null)
            {
                Close();
                return;
            }

            ActualizarVidaUI();
            AplicarEstadoTurno();

            string modo = _modoCombate == ModoCombate.UnoVsIA ? "1vsIA" : "1vs1";
            EscribirEnLog($"Combate iniciado ({modo}).", Color.LightGray);
            EscribirEnLog($"Turno de {ObtenerNombreTurnoActual()}.", ColorTurnoJugador1);
        }

        private async void btnAtaque_Click(object sender, EventArgs e)
        {
            await EjecutarTurnoActualAsync(AccionCombate.Ataque);
        }

        private async void btnDefensa_Click(object sender, EventArgs e)
        {
            await EjecutarTurnoActualAsync(AccionCombate.Defensa);
        }

        private async void btnPatada_Click(object sender, EventArgs e)
        {
            await EjecutarTurnoActualAsync(AccionCombate.Patada);
        }

        private async void btnSumision_Click(object sender, EventArgs e)
        {
            await EjecutarTurnoActualAsync(AccionCombate.Sumision);
        }

        private async Task EjecutarTurnoActualAsync(AccionCombate accion)
        {
            if (!PuedeActuarHumano())
            {
                return;
            }

            if (_jugador1 is null || _rival is null)
            {
                return;
            }

            Personaje atacante = _turnoJugador1 ? _jugador1 : _rival;
            Personaje defensor = _turnoJugador1 ? _rival : _jugador1;
            Color colorTurno = _turnoJugador1 ? ColorTurnoJugador1 : ColorTurnoRojo;
            string etiquetaAtacante = ObtenerEtiquetaAtacanteActual();

            EjecutarAccion(atacante, defensor, accion, etiquetaAtacante, colorTurno);

            if (RevisarFinCombate())
            {
                return;
            }

            _turnoJugador1 = !_turnoJugador1;
            AplicarEstadoTurno();

            if (_modoCombate == ModoCombate.UnoVsIA && !_turnoJugador1)
            {
                await EjecutarTurnoIAAsync();
                return;
            }

            Color colorSiguienteTurno = _turnoJugador1 ? ColorTurnoJugador1 : ColorTurnoRojo;
            EscribirEnLog($"Turno de {ObtenerNombreTurnoActual()}.", colorSiguienteTurno);
        }

        private async Task EjecutarTurnoIAAsync()
        {
            if (_modoCombate != ModoCombate.UnoVsIA)
            {
                return;
            }

            if (_combateTerminado || _turnoJugador1 || _jugador1 is null || _rival is null)
            {
                return;
            }

            await Task.Delay(DelayIATurnoMs);

            if (_combateTerminado)
            {
                return;
            }

            AccionCombate accionIA = _combateController.ObtenerAccionAleatoria();
            EjecutarAccion(_rival, _jugador1, accionIA, "IA", ColorTurnoRojo);

            if (RevisarFinCombate())
            {
                return;
            }

            _turnoJugador1 = true;
            AplicarEstadoTurno();
            EscribirEnLog($"Turno de {ObtenerNombreTurnoActual()}.", ColorTurnoJugador1);
        }

        private ResultadoAccion EjecutarAccion(
            Personaje atacante,
            Personaje defensor,
            AccionCombate accion,
            string etiquetaAtacante,
            Color color)
        {
            ResultadoAccion resultado = _combateController.EjecutarAccion(atacante, defensor, accion);
            ActualizarVidaUI();

            string mensajeLog = ConstruirMensajeLog(etiquetaAtacante, resultado);
            EscribirEnLog(mensajeLog, color);

            Text = $"Combate: {_jugador1?.Nombre} vs {_rival?.Nombre} | Turno: {ObtenerNombreTurnoActual()}";
            return resultado;
        }

        private static string ConstruirMensajeLog(string atacante, ResultadoAccion resultado)
        {
            return resultado.Accion switch
            {
                AccionCombate.Ataque =>
                    $"{atacante} usa Ataque y hace {resultado.DanioInfligido} de dano.",

                AccionCombate.Patada => resultado.Acierto
                    ? $"{atacante} usa Patada y hace {resultado.DanioInfligido} de dano."
                    : $"{atacante} usa Patada y falla.",

                AccionCombate.Defensa =>
                    $"{atacante} usa Defensa: reduce el proximo dano y potencia su siguiente golpe.",

                AccionCombate.Sumision => resultado.InstantKill
                    ? $"{atacante} usa Sumision y logra KO instantaneo."
                    : $"{atacante} usa Sumision y falla.",

                _ => $"{atacante} realiza una accion."
            };
        }

        private string ObtenerEtiquetaAtacanteActual()
        {
            if (_turnoJugador1)
            {
                return _jugador1?.Nombre ?? "Jugador 1";
            }

            if (_modoCombate == ModoCombate.UnoVsIA)
            {
                return "IA";
            }

            return _rival?.Nombre ?? "Jugador 2";
        }

        private string ObtenerNombreTurnoActual()
        {
            if (_turnoJugador1)
            {
                return _jugador1?.Nombre ?? "Jugador 1";
            }

            return _modoCombate == ModoCombate.UnoVsIA
                ? "IA"
                : _rival?.Nombre ?? "Jugador 2";
        }

        private void EscribirEnLog(string mensaje, Color color)
        {
            if (rtbHistorial is null)
            {
                return;
            }

            rtbHistorial.SelectionStart = rtbHistorial.TextLength;
            rtbHistorial.SelectionLength = 0;
            rtbHistorial.SelectionColor = color;
            rtbHistorial.AppendText($"{mensaje}{Environment.NewLine}");
            rtbHistorial.SelectionColor = rtbHistorial.ForeColor;
            rtbHistorial.ScrollToCaret();
        }

        private bool RevisarFinCombate()
        {
            if (_jugador1 is null || _rival is null)
            {
                return true;
            }

            if (_jugador1.ResistenciaAct > 0 && _rival.ResistenciaAct > 0)
            {
                return false;
            }

            _combateTerminado = true;
            AplicarEstadoTurno();

            string ganador = _jugador1.ResistenciaAct <= 0 && _rival.ResistenciaAct <= 0
                ? "Empate"
                : _jugador1.ResistenciaAct > 0 ? _jugador1.Nombre : _rival.Nombre;

            GuardarResultadoEnHistorial(ganador);
            EscribirEnLog($"Combate finalizado. Ganador: {ganador}.", Color.Gold);
            MessageBox.Show($"Ganador: {ganador}", "Combate terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
            return true;
        }

        private void GuardarResultadoEnHistorial(string ganador)
        {
            if (_historialGuardado || _jugador1 is null || _rival is null || string.IsNullOrWhiteSpace(_usuarioActual))
            {
                return;
            }

            string resultadoUsuario = ganador switch
            {
                "Empate" => "Empate",
                var nombre when string.Equals(nombre, _jugador1.Nombre, StringComparison.OrdinalIgnoreCase) => "Victoria",
                _ => "Derrota"
            };

            string rivalNombre = _modoCombate == ModoCombate.UnoVsIA
                ? $"IA ({_rival.Nombre})"
                : _rival.Nombre;

            bool guardado = _usuarioController.GuardarCombate(
                _usuarioActual,
                _jugador1.Nombre,
                rivalNombre,
                resultadoUsuario,
                rtbHistorial?.Text ?? string.Empty);

            if (!guardado)
            {
                EscribirEnLog("No se pudo guardar el historial de este combate.", Color.OrangeRed);
                return;
            }

            _historialGuardado = true;
        }

        private bool PuedeActuarHumano()
        {
            if (_combateTerminado || _jugador1 is null || _rival is null)
            {
                return false;
            }

            if (_modoCombate == ModoCombate.UnoVsIA)
            {
                return _turnoJugador1;
            }

            return true;
        }

        private void AplicarEstadoTurno()
        {
            Color color = _turnoJugador1 ? ColorTurnoJugador1 : ColorTurnoRojo;
            bool habilitar = !_combateTerminado && (_modoCombate == ModoCombate.UnoVsUno || _turnoJugador1);

            btnAtaque.BackColor = color;
            btnDefensa.BackColor = color;
            btnPatada.BackColor = color;
            btnSumision.BackColor = color;

            btnAtaque.Enabled = habilitar;
            btnDefensa.Enabled = habilitar;
            btnPatada.Enabled = habilitar;
            btnSumision.Enabled = habilitar;

            Text = $"Combate: {_jugador1?.Nombre} vs {_rival?.Nombre} | Turno: {ObtenerNombreTurnoActual()}";
        }

        private void ActualizarVidaUI()
        {
            if (_jugador1 is not null)
            {
                VidaPj1.Maximum = Math.Max(1, _jugador1.ResistenciaMax);
                VidaPj1.Value = Math.Clamp(_jugador1.ResistenciaAct, VidaPj1.Minimum, VidaPj1.Maximum);
            }

            if (_rival is not null)
            {
                VidaPj2.Maximum = Math.Max(1, _rival.ResistenciaMax);
                VidaPj2.Value = Math.Clamp(_rival.ResistenciaAct, VidaPj2.Minimum, VidaPj2.Maximum);
            }
        }

        private bool CargarPersonajesDesdeBD()
        {
            const string cadenaConexion = "Server=localhost;Database=uefete_db;Uid=root;Pwd=rmkZ;AllowPublicKeyRetrieval=True;";

            using MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            try
            {
                conexion.Open();

                const string queryJugador = "SELECT * FROM personajes WHERE nombre = @nombre LIMIT 1";
                using (MySqlCommand cmdJugador = new MySqlCommand(queryJugador, conexion))
                {
                    cmdJugador.Parameters.AddWithValue("@nombre", _nombreJugador);
                    using MySqlDataReader readerJugador = cmdJugador.ExecuteReader();
                    if (readerJugador.Read())
                    {
                        _jugador1 = CrearPersonajeDesdeFila(readerJugador);
                        CargarImagenSiExiste(pj1, LeerTexto(readerJugador, "ruta_gif"));
                    }
                }

                if (_jugador1 is null)
                {
                    MessageBox.Show("No se encontro el personaje del jugador en la base de datos.");
                    return false;
                }

                if (_modoCombate == ModoCombate.UnoVsUno)
                {
                    CargarRivalPorNombre(conexion, _nombreRival);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_nombreRival))
                    {
                        CargarRivalPorNombre(conexion, _nombreRival);
                    }

                    if (_rival is null)
                    {
                        CargarRivalAleatorio(conexion, _nombreJugador);
                    }
                }

                if (_jugador1 is null || _rival is null)
                {
                    MessageBox.Show("No se encontraron ambos personajes en la base de datos.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de MySQL: " + ex.Message);
                return false;
            }
        }

        private static Personaje CrearPersonajeDesdeFila(MySqlDataReader reader)
        {
            int vidaBase = Math.Max(1, LeerEntero(reader, "resistencia"));
            int vidaCombate = vidaBase * 4;

            return new Personaje
            {
                Nombre = LeerTexto(reader, "nombre") ?? "Personaje",
                Ataque = LeerEntero(reader, "ataque", "fuerza"),
                Defensa = LeerEntero(reader, "defensa"),
                Tecnica = LeerEntero(reader, "tecnica"),
                Resistencia = vidaBase,
                ResistenciaMax = vidaCombate,
                ResistenciaAct = vidaCombate,
                Defendiendo = false
            };
        }

        private static Personaje PrepararPersonajeParaCombate(Personaje original)
        {
            Personaje preparado = ClonarPersonaje(original);

            int vidaBase = preparado.Resistencia > 0
                ? preparado.Resistencia
                : Math.Max(1, preparado.ResistenciaMax > 0 ? preparado.ResistenciaMax / 4 : 1);

            preparado.Resistencia = vidaBase;
            preparado.ResistenciaMax = preparado.ResistenciaMax > 0 ? preparado.ResistenciaMax : vidaBase * 4;

            if (preparado.ResistenciaAct <= 0 || preparado.ResistenciaAct > preparado.ResistenciaMax)
            {
                preparado.ResistenciaAct = preparado.ResistenciaMax;
            }

            preparado.Defendiendo = false;
            return preparado;
        }

        private static Personaje ClonarPersonaje(Personaje original)
        {
            return new Personaje
            {
                Id = original.Id,
                Nombre = original.Nombre,
                Ataque = original.Ataque,
                Defensa = original.Defensa,
                Resistencia = original.Resistencia,
                Tecnica = original.Tecnica,
                ResistenciaMax = original.ResistenciaMax,
                ResistenciaAct = original.ResistenciaAct,
                Defendiendo = original.Defendiendo
            };
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
            if (!TieneColumna(reader, columna))
            {
                return null;
            }

            object valor = reader[columna];
            if (valor == DBNull.Value)
            {
                return null;
            }

            return valor.ToString();
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

        private static void CargarImagenSiExiste(PictureBox pictureBox, string? rutaImagen)
        {
            if (string.IsNullOrWhiteSpace(rutaImagen) || !File.Exists(rutaImagen))
            {
                return;
            }

            pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void CargarRivalPorNombre(MySqlConnection conexion, string nombreRival)
        {
            const string queryRival = "SELECT * FROM personajes WHERE nombre = @nombre LIMIT 1";
            using MySqlCommand cmdRival = new MySqlCommand(queryRival, conexion);
            cmdRival.Parameters.AddWithValue("@nombre", nombreRival);

            using MySqlDataReader readerRival = cmdRival.ExecuteReader();
            if (!readerRival.Read())
            {
                return;
            }

            Personaje rival = CrearPersonajeDesdeFila(readerRival);
            _rival = ClonarPersonaje(rival);
            CargarImagenSiExiste(pj2, LeerTexto(readerRival, "ruta_gif"));
        }

        private void CargarRivalAleatorio(MySqlConnection conexion, string nombreExcluido)
        {
            const string queryAleatorio = "SELECT * FROM personajes WHERE nombre <> @nombre ORDER BY RAND() LIMIT 1";
            using MySqlCommand cmdAleatorio = new MySqlCommand(queryAleatorio, conexion);
            cmdAleatorio.Parameters.AddWithValue("@nombre", nombreExcluido);

            using MySqlDataReader readerAleatorio = cmdAleatorio.ExecuteReader();
            if (!readerAleatorio.Read())
            {
                return;
            }

            Personaje rival = CrearPersonajeDesdeFila(readerAleatorio);
            _rival = ClonarPersonaje(rival);
            CargarImagenSiExiste(pj2, LeerTexto(readerAleatorio, "ruta_gif"));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
        }

        private void VidaPj1_Click(object sender, EventArgs e)
        {
        }

        private void pj2_Click(object sender, EventArgs e)
        {
        }
    }
}

