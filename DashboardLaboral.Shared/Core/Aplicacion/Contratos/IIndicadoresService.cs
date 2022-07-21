using DashboarLaboral.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos.Repositorios
{
    public interface IIndicadoresService
    {
        Task<Dictionary<string, IndicadorModel>> CargarIndicadoresSimples(DateTime fechaHoy);
        Task<Dictionary<string, IndicadorModel>> CargarIndicadoresExtendidos(DateTime fechaHoy);

    }
}
