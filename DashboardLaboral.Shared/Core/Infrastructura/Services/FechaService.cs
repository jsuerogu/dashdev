using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using System;
using System.Linq;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    record FechaEjecucion(DateTime Fecha, TimeSpan Hora);
    public class FechaService : IFechaService
    {

        private readonly insitedb context;

        public FechaService(insitedb context)
        {
            this.context = context;
        }

        public DateTime Hoy => new DateTime(2022, 07, 14) /*DateTime.Now.FechaZonaHoraria()*/;

        public DateTime FechaHora => new DateTime(Hoy.Year, Hoy.Month, Hoy.Day, 22, 30, 0) /*Hoy*/;

        public DateTime FechaEjecucion()
        {
            var ejecuciones = context.Ejecuciones.Where(f => f.FechaEjecucion != null).Select(f => new  FechaEjecucion(f.FechaEjecucion.Value, f.HoraEjecucion.Value))
                .ToList();

            return ejecuciones.Select(e =>
                    e.Fecha
                        .AddHours(e.Hora.Hours)
                        .AddMinutes(e.Hora.Minutes)
                        .AddSeconds(e.Hora.Seconds))
                .OrderByDescending(f => f)
                .FirstOrDefault();
        }

        public TimeSpan HoraEjecucion()
        {
            throw new NotImplementedException();
        }
    }
}
