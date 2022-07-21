using DashboarLaboral.Models.Tops;
using System;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface ITopService
    {
        Task<TopModel> ObtenerTopMayorIncumplimientoDepartamentoAsync(DateTime fecha, bool hasLink);
        Task<TopModel> ObtenerTopMayorHorasExtrasDepartamentoAsync(DateTime fecha, bool hasLink);
        Task<TopModel> ObtenerTopMayorIncumplimientoEmpleadosAsync(DateTime fecha, bool hasLink);
        Task<TopModel> ObtenerTopMayorHorasExtrasEmpleadosAsync(DateTime fecha, bool hasLink);
    }
}
