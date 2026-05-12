using System;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public enum AccionCombate
    {
        Ataque = 0,
        Patada = 1,
        Defensa = 2,
        Sumision = 3
    }

    public sealed class ResultadoAccion
    {
        public AccionCombate Accion { get; init; }
        public string NombreAccion { get; init; } = string.Empty;
        public bool Acierto { get; init; }
        public int DanioInfligido { get; init; }
        public bool InstantKill { get; init; }
        public bool DefensaAplicada { get; init; }
        public double ProbabilidadExito { get; init; }
        public string Mensaje { get; init; } = string.Empty;
    }

    public class CombateController
    {
        private const double MultiplicadorDanioGlobal = 0.45;
        private readonly Random _random;

        public CombateController() : this(Random.Shared)
        {
        }

        internal CombateController(Random random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public AccionCombate ObtenerAccionAleatoria()
        {
            return (AccionCombate)_random.Next(0, 4);
        }

        public ResultadoAccion EjecutarAccion(Personaje atacante, Personaje defensor, AccionCombate accion)
        {
            if (atacante is null) throw new ArgumentNullException(nameof(atacante));
            if (defensor is null) throw new ArgumentNullException(nameof(defensor));

            return accion switch
            {
                AccionCombate.Ataque => EjecutarAtaque(atacante, defensor),
                AccionCombate.Patada => EjecutarPatada(atacante, defensor),
                AccionCombate.Defensa => EjecutarDefensa(atacante),
                AccionCombate.Sumision => EjecutarSumision(atacante, defensor),
                _ => EjecutarDefensa(atacante)
            };
        }

        private ResultadoAccion EjecutarAtaque(Personaje atacante, Personaje defensor)
        {
            int danioBase = CalcularDanioAtaqueBase(atacante, defensor);
            danioBase = AplicarBonusSiguienteGolpe(atacante, danioBase);
            int danioFinal = AplicarDanio(defensor, danioBase, consumirTurnoDefensa: true, out bool defensaAplicada);

            return new ResultadoAccion
            {
                Accion = AccionCombate.Ataque,
                NombreAccion = "Ataque",
                Acierto = true,
                DanioInfligido = danioFinal,
                DefensaAplicada = defensaAplicada,
                ProbabilidadExito = 1.0,
                Mensaje = $"{atacante.Nombre} conecta punetazo y causa {danioFinal} de dano."
            };
        }

        private ResultadoAccion EjecutarPatada(Personaje atacante, Personaje defensor)
        {
            const double probabilidadPatada = 0.70;
            bool acierta = _random.NextDouble() <= probabilidadPatada;

            if (!acierta)
            {
                if (defensor.Defendiendo)
                {
                    defensor.Defendiendo = false;
                }

                return new ResultadoAccion
                {
                    Accion = AccionCombate.Patada,
                    NombreAccion = "Patada",
                    Acierto = false,
                    DanioInfligido = 0,
                    ProbabilidadExito = probabilidadPatada,
                    Mensaje = $"{atacante.Nombre} intenta patada y falla."
                };
            }

            int danioBase = CalcularDanioPatadaBase(atacante);
            danioBase = AplicarBonusSiguienteGolpe(atacante, danioBase);
            int danioFinal = AplicarDanio(defensor, danioBase, consumirTurnoDefensa: true, out bool defensaAplicada);

            return new ResultadoAccion
            {
                Accion = AccionCombate.Patada,
                NombreAccion = "Patada",
                Acierto = true,
                DanioInfligido = danioFinal,
                DefensaAplicada = defensaAplicada,
                ProbabilidadExito = probabilidadPatada,
                Mensaje = $"{atacante.Nombre} conecta patada y causa {danioFinal} de dano."
            };
        }

        private ResultadoAccion EjecutarDefensa(Personaje atacante)
        {
            atacante.Defendiendo = true;
            atacante.BonusDanioPorcentajePendiente = _random.Next(10, 26);

            return new ResultadoAccion
            {
                Accion = AccionCombate.Defensa,
                NombreAccion = "Defensa",
                Acierto = true,
                DanioInfligido = 0,
                ProbabilidadExito = 1.0,
                Mensaje = $"{atacante.Nombre} se prepara para reducir el proximo dano en 70% y potenciar su siguiente golpe."
            };
        }

        private ResultadoAccion EjecutarSumision(Personaje atacante, Personaje defensor)
        {
            double probabilidad = CalcularProbabilidadSumision(atacante);
            bool acierta = _random.NextDouble() <= probabilidad;

            if (defensor.Defendiendo)
            {
                defensor.Defendiendo = false;
            }

            if (acierta)
            {
                int vidaAntes = Math.Max(0, defensor.ResistenciaAct);
                defensor.ResistenciaAct = 0;

                return new ResultadoAccion
                {
                    Accion = AccionCombate.Sumision,
                    NombreAccion = "Sumision",
                    Acierto = true,
                    DanioInfligido = vidaAntes,
                    InstantKill = true,
                    ProbabilidadExito = probabilidad,
                    Mensaje = $"{atacante.Nombre} ejecuta sumision y consigue KO instantaneo."
                };
            }

            return new ResultadoAccion
            {
                Accion = AccionCombate.Sumision,
                NombreAccion = "Sumision",
                Acierto = false,
                DanioInfligido = 0,
                ProbabilidadExito = probabilidad,
                Mensaje = $"{atacante.Nombre} intenta sumision y falla."
            };
        }

        private static int CalcularDanioAtaqueBase(Personaje atacante, Personaje defensor)
        {
            double danio = ((ObtenerFuerza(atacante) * 1.2) - (defensor.DefensaStat * 0.5)) * MultiplicadorDanioGlobal;
            return AEnteroPositivo(danio);
        }

        private static int CalcularDanioPatadaBase(Personaje atacante)
        {
            double danio = (ObtenerFuerza(atacante) * 2.0) * MultiplicadorDanioGlobal;
            return AEnteroPositivo(danio);
        }

        private static double CalcularProbabilidadSumision(Personaje atacante)
        {
            double probabilidad = (5.0 + (ObtenerTecnica(atacante) / 10.0)) / 100.0;
            return Math.Clamp(probabilidad, 0.0, 1.0);
        }

        private static int AplicarDanio(Personaje defensor, int danioBase, bool consumirTurnoDefensa, out bool defensaAplicada)
        {
            defensaAplicada = false;
            double danioFinal = danioBase;

            if (defensor.Defendiendo && consumirTurnoDefensa)
            {
                if (danioBase > 0)
                {
                    danioFinal *= 0.30;
                    defensaAplicada = true;
                }

                defensor.Defendiendo = false;
            }

            int danio = AEnteroPositivo(danioFinal);
            defensor.ResistenciaAct = Math.Max(0, defensor.ResistenciaAct - danio);
            return danio;
        }

        private static int ObtenerFuerza(Personaje personaje)
        {
            if (personaje.Fuerza > 0)
            {
                return personaje.Fuerza;
            }

            return personaje.AtaqueStat;
        }

        private static int ObtenerTecnica(Personaje personaje)
        {
            return Math.Max(0, personaje.Tecnica);
        }

        private static int AEnteroPositivo(double valor)
        {
            int entero = (int)Math.Round(valor, MidpointRounding.AwayFromZero);
            return Math.Max(0, entero);
        }

        private static int AplicarBonusSiguienteGolpe(Personaje atacante, int danioBase)
        {
            if (danioBase <= 0)
            {
                return 0;
            }

            int bonusPorcentaje = atacante.BonusDanioPorcentajePendiente;
            if (bonusPorcentaje <= 0)
            {
                return danioBase;
            }

            atacante.BonusDanioPorcentajePendiente = 0;
            double danioConBonus = danioBase * (1.0 + (bonusPorcentaje / 100.0));
            return AEnteroPositivo(danioConBonus);
        }
    }
}
