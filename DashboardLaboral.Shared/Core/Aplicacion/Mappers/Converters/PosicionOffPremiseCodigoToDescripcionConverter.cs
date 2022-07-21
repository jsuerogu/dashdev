using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class PosicionOffPremiseCodigoToDescripcionConverter : IValueResolver<PosicionOffPremiseHeader, PosicionOffPremiseHeaderViewDto, string>
    {
        private readonly IRepositoryEmpresa repository;

        public PosicionOffPremiseCodigoToDescripcionConverter(IRepositoryEmpresa repository)
        {
            this.repository = repository;
        }

        public string Resolve(PosicionOffPremiseHeader source, PosicionOffPremiseHeaderViewDto destination, string destMember, ResolutionContext context)
        {
            var entity = repository.FindAsync(source.Empresa).GetAwaiter().GetResult();
            return entity?.Empresa1 ?? "";
        }
    }
}
