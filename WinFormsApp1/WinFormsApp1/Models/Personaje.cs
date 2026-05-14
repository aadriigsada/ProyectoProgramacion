namespace WinFormsApp1.Models
{
    public class Personaje
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Resistencia { get; set; }
        public int Tecnica { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        // Estado en combate
        public int HP { get; set; }
        public bool Defendiendo { get; set; }
        public int UsosEspecial { get; set; } = 3;
        public int BonusDanioPorcentajePendiente { get; set; }

        // Alias para compatibilidad con esquemas que usan "fuerza".
        public int Fuerza
        {
            get => Ataque;
            set => Ataque = value;
        }

        // Compatibilidad con nombres legacy usados por el motor de combate.
        public int AtaqueStat
        {
            get => Ataque;
            set => Ataque = value;
        }

        public int DefensaStat
        {
            get => Defensa;
            set => Defensa = value;
        }

        public int ResistenciaAct
        {
            get => HP;
            set => HP = value;
        }

        public int ResistenciaMax { get; set; }

        public void InicializarHP()
        {
            ResistenciaMax = Resistencia * 10;
            HP = ResistenciaMax;
        }
    }
}
