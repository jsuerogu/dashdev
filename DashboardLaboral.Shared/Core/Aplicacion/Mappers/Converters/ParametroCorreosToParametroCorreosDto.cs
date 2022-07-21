using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class ParametroCorreosToParametroCorreosDto : ITypeConverter<ParametroCorreos, ParametroCorreosDto>
    {
        private readonly IDataContext dataContext;

        public ParametroCorreosToParametroCorreosDto(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ParametroCorreosDto Convert(ParametroCorreos source, ParametroCorreosDto destination, ResolutionContext context)
        {
            if(destination is null) destination = new ParametroCorreosDto(); 

            var selectedindicadores = source.Indicadores?.Split(',') ?? new string[] { };

            destination.Destinatario = source.Destinatario ?? "";
            destination.Indicadores = dataContext.ObtenerListaIndicadores().Where(x => !string.IsNullOrWhiteSpace(x.Value) ).Select(i => new IndicadoresDto { Indicador = i.Display, Name = i.Value, Selected = selectedindicadores.Any(x => x == i.Value) }).ToArray();
            destination.Departamento = source.Departamento;
            destination.Id = source.Id;
            destination.Departamentos = dataContext.ObtenerDepartamentos(source.Empresa, source.Vicepresidencia, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.Empresas = dataContext.ObtenerEmpresas(emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.Vicepresidencias = dataContext.ObtenerVicepresidencias(source.Empresa, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.Vicepresidencia = source.Vicepresidencia;
            destination.Empresa = source.Empresa;
            return destination;
        }
    }
}
