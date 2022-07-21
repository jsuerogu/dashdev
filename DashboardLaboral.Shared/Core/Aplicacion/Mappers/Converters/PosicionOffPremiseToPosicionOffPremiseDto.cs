using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class PosicionOffPremiseToPosicionOffPremiseDto : ITypeConverter<PosicionOffPremiseHeader, PosicionOffPremiseHeaderDto>
    {
        private readonly IDataContext dataContext;

        public PosicionOffPremiseToPosicionOffPremiseDto(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public PosicionOffPremiseHeaderDto Convert(PosicionOffPremiseHeader source, PosicionOffPremiseHeaderDto destination, ResolutionContext context)
        {
            if(destination is null) destination = new PosicionOffPremiseHeaderDto(); 


            destination.Posicion = source.Posicion;
            destination.Posiciones = dataContext.ObtenerPosiciones(source.Empresa, source.VicePresidencia, source.Departamento)
                    .Select(e => new SelectListItem(e.Display, e.Value)).ToList();

            destination.Departamento = source.Departamento;
            destination.Id = source.Id;
            destination.Departamentos = dataContext.ObtenerDepartamentos(source.Empresa, source.VicePresidencia, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.Empresas = dataContext.ObtenerEmpresas(emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.Vicepresidencias = dataContext.ObtenerVicepresidencias(source.Empresa, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            destination.VicePresidencia = source.VicePresidencia;
            destination.Empresa = source.Empresa;
            return destination;
        }
    }
}
