using System;

#nullable disable

namespace DashboarLaboral.Data
{
    public partial class Ejecucione
    {
        public string Idejecucion { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public TimeSpan? HoraEjecucion { get; set; }
    }
}
