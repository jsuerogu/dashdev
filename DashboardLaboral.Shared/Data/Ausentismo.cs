#nullable disable

using System.Collections.Generic;

namespace DashboarLaboral.Data
{
    public partial class Ausentismo
    {
        public string Id { get; set; }
        public string Aucod { get; set; }
        public string Audes { get; set; }
        public bool Aujus { get; set; }
        public bool Autel { get; set; }
        public int Riesgo { get; set; }
        public int Cuarentena { get; set; }

        public virtual ICollection<Horario> Horarios { get; set; }
    }
}
