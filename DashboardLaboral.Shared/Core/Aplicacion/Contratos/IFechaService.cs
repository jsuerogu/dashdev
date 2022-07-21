using System;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IFechaService
    {
        DateTime Hoy { get; }
        DateTime FechaEjecucion();
        DateTime FechaHora { get; }
    }
}
