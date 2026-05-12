using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1.Views
{
    public partial class CronicaCombateForm : Form
    {
        private const string GroqEndpoint = "https://api.groq.com/openai/v1/chat/completions";
        // Pega aqui tu API key de Groq o usa la variable de entorno GROQ_API_KEY.
        private const string GroqApiKey = "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";

        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly Personaje _p1;
        private readonly Personaje _p2;
        private readonly ModoCombate _modoCombate;
        private readonly string _usuarioActual;
        private readonly Random _random = new Random();

        private bool _combateAbierto;
        private bool _cierrePermitido;

        public CronicaCombateForm(Personaje p1, Personaje p2, ModoCombate modoCombate, string usuarioActual)
        {
            InitializeComponent();
            _p1 = p1 ?? throw new ArgumentNullException(nameof(p1));
            _p2 = p2 ?? throw new ArgumentNullException(nameof(p2));
            _modoCombate = modoCombate;
            _usuarioActual = usuarioActual;

            lblSubtitulo.Text = $"{_p1.Nombre} VS {_p2.Nombre}";
            Text = $"Cronica previa - {_p1.Nombre} vs {_p2.Nombre}";
        }

        private async void CronicaCombateForm_Load(object sender, EventArgs e)
        {
            await GenerarHistoriaIA();
        }

        private async Task GenerarHistoriaIA()
        {
            rtbHistoria.Text = "Obteniendo cronica de la pelea...";

            string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? GroqApiKey;
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Contains("PEGA_AQUI_TU_API_KEY_GROQ", StringComparison.OrdinalIgnoreCase))
            {
                rtbHistoria.Text = "[MODO RESPALDO: API KEY NO CONFIGURADA]" + Environment.NewLine + Environment.NewLine + GenerarHistoriaGenerica();
                return;
            }

            string prompt = ConstruirPromptDinamico();

            var payload = new
            {
                model = "llama-3.1-8b-instant", // Usamos la versión más nueva y rápida
                messages = new[]
    {
        new
        {
            role = "system",
            content = "Eres un cronista profesional de MMA. Evita repetir plantillas y estructura entre respuestas. Cada cronica debe sentirse unica."
        },
        new { role = "user", content = prompt }
    },
                // Eliminamos temperature, top_p y penalties para que Groq no se queje
                max_tokens = 900
            };

            try
            {
                string jsonPayload = JsonSerializer.Serialize(payload);

                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, GroqEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await HttpClient.SendAsync(request);
                string responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // Ahora responseText nos chivará exactamente qué le molesta a Groq
                    rtbHistoria.Text = $"[ERROR GROQ {(int)response.StatusCode}]: {responseText}" + Environment.NewLine + Environment.NewLine + GenerarHistoriaGenerica();
                    return;
                }

                using JsonDocument document = JsonDocument.Parse(responseText);
                string? historia = document
                    .RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                rtbHistoria.Text = string.IsNullOrWhiteSpace(historia)
                    ? "[MODO RESPALDO: RESPUESTA VACIA]" + Environment.NewLine + Environment.NewLine + GenerarHistoriaGenerica()
                    : historia.Trim() + Environment.NewLine + Environment.NewLine + "[FUENTE: IA GROQ]";
            }
            catch
            {
                rtbHistoria.Text = "[MODO RESPALDO: EXCEPCION EN LLAMADA API]" + Environment.NewLine + Environment.NewLine + GenerarHistoriaGenerica();
            }
        }

        private string ConstruirPromptDinamico()
        {
            string enfoque = ElegirUno(new[]
            {
                "un enfoque de guerra psicologica y ruedas de prensa tensas",
                "un enfoque de choque de estilos y respeto roto entre gimnasios",
                "un enfoque de orgullo competitivo por ranking y legado",
                "un enfoque de rivalidad por una polemica arbitral reciente"
            });

            string detonante = ElegirUno(new[]
            {
                "una declaracion provocadora despues de un cara a cara",
                "un entrenamiento abierto con cruce verbal delante de la prensa",
                "una entrevista viral que encendio a los dos equipos",
                "un pesaje previo con empujones y advertencias"
            });

            string cierre = ElegirUno(new[]
            {
                "cerrando con una sensacion de peligro inminente",
                "cerrando con una promesa de violencia tecnica en el octagono",
                "cerrando con una expectativa de pelea tactica al limite",
                "cerrando con un ambiente de cuentas pendientes"
            });

            string lecturaP1 = DescribirPerfil(_p1);
            string lecturaP2 = DescribirPerfil(_p2);
            string comparativa = ConstruirComparativa();
            string marcaUnica = $"{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}";

            return
                $"Eres un cronista de MMA. Escribe una historia de rivalidad realista y tensa en 4 o 5 parrafos cortos entre {_p1.Nombre} y {_p2.Nombre} antes de su pelea en el octagono de UEFETE. " +
                $"Usa {enfoque}. El detonante principal fue {detonante}. " +
                $"Usa solo estas estadisticas y no inventes otras: {_p1.Nombre} (Fuerza {_p1.Fuerza}, Defensa {_p1.Defensa}, Resistencia {_p1.Resistencia}) y {_p2.Nombre} (Fuerza {_p2.Fuerza}, Defensa {_p2.Defensa}, Resistencia {_p2.Resistencia}). " +
                $"Lectura de {_p1.Nombre}: {lecturaP1}. Lectura de {_p2.Nombre}: {lecturaP2}. Comparativa clave: {comparativa}. " +
                $"Evita frases repetidas entre respuestas, evita arrancar siempre igual y da detalles concretos de contexto de campamento, prensa o vestuario. " +
                $"Termina {cierre}. Referencia interna para variar salida: {marcaUnica}.";
        }

        private string GenerarHistoriaGenerica()
        {
            string incidente = ElegirUno(new[]
            {
                "una rueda de prensa termino con acusaciones cruzadas",
                "una entrevista encendio a los dos campamentos",
                "un careo dejo miradas de desafio y amenazas veladas",
                "un entrenamiento abierto elevo la tension mas de lo esperado"
            });

            string cierre = ElegirUno(new[]
            {
                "Todo apunta a una batalla de desgaste, golpe a golpe.",
                "El ambiente es de todo o nada: nadie quiere ceder terreno.",
                "La pelea promete ser una guerra de ritmo y control.",
                "Cuando se cierre la jaula, hablara el que imponga su plan."
            });

            return
                $"{_p1.Nombre} y {_p2.Nombre} llegan a UEFETE con la tension al maximo: {incidente}. Lo que parecia una rivalidad deportiva normal se transformo en una disputa personal por imponerse frente a todo el mundo." + Environment.NewLine + Environment.NewLine +
                $"{_p1.Nombre} presenta Fuerza {_p1.Fuerza}, Defensa {_p1.Defensa} y Resistencia {_p1.Resistencia}. Su equipo busca un arranque fuerte para romper la confianza del rival desde temprano y marcar el ritmo del combate." + Environment.NewLine + Environment.NewLine +
                $"{_p2.Nombre} responde con Fuerza {_p2.Fuerza}, Defensa {_p2.Defensa} y Resistencia {_p2.Resistencia}. La estrategia pasa por sostener el castigo, castigar errores y llevar la pelea al terreno donde se siente mas comodo." + Environment.NewLine + Environment.NewLine +
                $"{ConstruirComparativa()} {cierre}";
        }

        private string ConstruirComparativa()
        {
            List<string> claves = new List<string>();

            int difF = _p1.Fuerza - _p2.Fuerza;
            int difD = _p1.Defensa - _p2.Defensa;
            int difR = _p1.Resistencia - _p2.Resistencia;

            if (Math.Abs(difF) >= 3)
            {
                claves.Add(difF > 0
                    ? $"{_p1.Nombre} tiene ventaja clara de potencia fisica"
                    : $"{_p2.Nombre} tiene ventaja clara de potencia fisica");
            }

            if (Math.Abs(difD) >= 3)
            {
                claves.Add(difD > 0
                    ? $"{_p1.Nombre} parece mejor preparado para absorber castigo"
                    : $"{_p2.Nombre} parece mejor preparado para absorber castigo");
            }

            if (Math.Abs(difR) >= 3)
            {
                claves.Add(difR > 0
                    ? $"{_p1.Nombre} llega con mejor fondo para rounds largos"
                    : $"{_p2.Nombre} llega con mejor fondo para rounds largos");
            }

            if (claves.Count == 0)
            {
                return "Las estadisticas estan muy equilibradas y cualquier detalle tactico puede inclinar la pelea.";
            }

            return string.Join(", ", claves) + ".";
        }

        private static string DescribirPerfil(Personaje p)
        {
            if (p.Fuerza >= p.Defensa && p.Fuerza >= p.Resistencia)
            {
                return "perfil de presion ofensiva y golpeo pesado";
            }

            if (p.Defensa >= p.Fuerza && p.Defensa >= p.Resistencia)
            {
                return "perfil de lectura defensiva y control de distancia";
            }

            return "perfil de resistencia y desgaste progresivo";
        }

        private string ElegirUno(string[] opciones)
        {
            if (opciones.Length == 0)
            {
                return string.Empty;
            }

            return opciones[_random.Next(opciones.Length)];
        }

        private void btnSaltar_Click(object sender, EventArgs e)
        {
            AbrirCombate();
        }

        private void CronicaCombateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cierrePermitido || _combateAbierto)
            {
                return;
            }

            e.Cancel = true;
            AbrirCombate();
        }

        private void AbrirCombate()
        {
            if (_combateAbierto)
            {
                return;
            }

            _combateAbierto = true;
            Hide();

            using CombateForm combateForm = new CombateForm(_p1, _p2, _modoCombate, _usuarioActual);
            combateForm.ShowDialog(this);

            _cierrePermitido = true;
            Close();
        }
    }
}
