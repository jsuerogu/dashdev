using DashboarLaboral.Data;
using DashboarLaboral.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IData
    {
        Expression<Func<Horario, bool>> Filtro { get; }
        Task<IndicadorModel> ObtenerModel(DateTime fecha);
        Task<IQueryable<Horario>> ConsultaData(DateTime fechaInicial, DateTime fechaFinal, bool isHourFilter = true);
        Task<int> Contar(DateTime fecha);
    }
}
