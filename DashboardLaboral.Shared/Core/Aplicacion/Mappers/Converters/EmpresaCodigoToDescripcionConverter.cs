using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class EmpresaCodigoToDescripcionConverter  : IValueResolver<ParametroCorreos, ParametroCorreosViewDto, string>
    {
        private readonly IRepositoryEmpresa repository;

        public EmpresaCodigoToDescripcionConverter(IRepositoryEmpresa repository)
        {
            this.repository = repository;
        }

        public string Resolve(ParametroCorreos source, ParametroCorreosViewDto destination, string destMember, ResolutionContext context)
        {
            var entity = repository.FindAsync(source.Empresa).GetAwaiter().GetResult();
            return entity?.Empresa1 ?? "";
        }
    }
}
