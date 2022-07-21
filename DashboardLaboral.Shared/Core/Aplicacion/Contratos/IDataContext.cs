using DashboarLaboral.Data;
using DashboarLaboral.Models;
using DashboarLaboral.Models.Graficos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboarLaboral.Core.Aplicacion.Contratos.Repositorios
{
    public interface IDataContext
    {
        IQueryable<Horario> ObtenerData(bool isHourFilter = true);
        int MinutosMaxTardanza();
        int MinutosMaxAusencia();
        int AdminHorasLab();
        decimal HorasAlmuerzoAdm();
        IList<string> ObtenerAusentismo();
        IList<string> ObtenerAusenciaJustificadas();
        IList<string> ObtenerCondicionRiesgo();
        IList<string> ObtenerCuarentena();
        List<ValueDisplayModel> ObtenerEmpresas(string emptyLabel = "Todas");
        List<ValueDisplayModel> ObtenerListaIndicadores();
        List<ValueDisplayModel> ObtenerListaEmpleados(string empresa, string vicepresidencia, string departamento, string posicion);
        List<ValueDisplayModel> ObtenerVicepresidencias(string empresa, string emptyLabel = "Todas");
        List<ValueDisplayModel> ObtenerDepartamentos(string empresa, string vicepresidencia, bool isHourFilter = true, string emptyLabel = "Todas");
        List<ValueDisplayModel> ObtenerPosiciones(string empresa, string vicepresidencia, string departamento, string emptyLabel = "Seleccionar");
        IQueryable<Ausentismo> DbTableAusentismo();
        Parametro ObtenerParametros(string parameterName);
        List<ParametroCorreoModel> ObtenerParametrosCorreo();
        IEnumerable<int> IsPosicionOffPremise();
        Empresa ObtenerDetalleEmpresa(string code);
    }
}
