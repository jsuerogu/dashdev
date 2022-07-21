using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using System.Linq;

namespace DashboarLaboral.Core.Aplicacion.Mappers.Converters
{
    public class IndicadoresDescricionConverter : IValueResolver<ParametroCorreos, ParametroCorreosViewDto, string>
    {
        private readonly IDataContext dataContext;

        public IndicadoresDescricionConverter(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public string Resolve(ParametroCorreos source, ParametroCorreosViewDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Indicadores)) return "";

            var indicadorList = dataContext.ObtenerListaIndicadores();
            var indicadoresSelected = source.Indicadores.Split(',');


            return string.Join(',', indicadorList.Where(i => indicadoresSelected.Contains(i.Value)).Select(i => i.Display));
        }
    }
}
