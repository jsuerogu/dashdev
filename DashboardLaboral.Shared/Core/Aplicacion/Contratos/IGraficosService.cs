using DashboarLaboral.Models.Graficos;
using System;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IGraficosService
    {
        Task<RadialBarModel> ObtenerRadialBarDatos(DateTime fechaHoy);
        Task<AreaChartModel> ObtenerAreaChartDatos(DateTime fechaHoy);
        Task<HorasExtrasChart> ObtenerGraficoHorasExtras(DateTime fechaIni, DateTime fechaFin, int? administrativo = null, string empresa = null, string vicepresidencia = null, string depatamento = null);
    }
}
