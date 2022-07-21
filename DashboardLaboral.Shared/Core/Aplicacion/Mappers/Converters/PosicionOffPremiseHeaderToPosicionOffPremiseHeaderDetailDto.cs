using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using System.Collections.Generic;
using System.Linq;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class PosicionOffPremiseHeaderToPosicionOffPremiseHeaderDetailDto : ITypeConverter<PosicionOffPremiseHeader, PosicionOffPremiseHeaderDetailDto>
    {
        private readonly IDataContext dataContext;
        public PosicionOffPremiseHeaderToPosicionOffPremiseHeaderDetailDto(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public PosicionOffPremiseHeaderDetailDto Convert(PosicionOffPremiseHeader source, PosicionOffPremiseHeaderDetailDto destination, ResolutionContext context)
        {
            if (destination is null)
                destination = new PosicionOffPremiseHeaderDetailDto();

            var empresa = dataContext.ObtenerEmpresas().FirstOrDefault(a => a.Value == source.Empresa);

            destination.Id = source.Id;
            destination.Posicion = $"{empresa.Display}/{source.VicePresidencia}/{source.Departamento}/{source.Posicion}";

            var listEmpleados = dataContext.ObtenerListaEmpleados(source.Empresa, source.VicePresidencia, source.Departamento, source.Posicion);

            var destinationDetails = context.Mapper.Map<List<PosicionOffPremiseDetailsDto>>(source.Details);
            foreach (var item in destinationDetails)
            {
                item.Nombre = listEmpleados.FirstOrDefault(e => int.Parse(e.Value) == item.CodigoEmpleado).Display;
            }

            destinationDetails.AddRange(listEmpleados.Where(e => !destinationDetails.Any(i => i.CodigoEmpleado == int.Parse(e.Value)))
                .Select(e => new PosicionOffPremiseDetailsDto { Id = 0,  IdHeader = source.Id, CodigoEmpleado = int.Parse(e.Value), 
                    Nombre = e.Display }));

            destination.Details = destinationDetails.OrderBy(e => e.CodigoEmpleado).ToList();
            return destination;
        }
    }
}
