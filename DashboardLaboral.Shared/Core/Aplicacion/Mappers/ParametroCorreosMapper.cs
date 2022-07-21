using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Mappers.Converters;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using System.Linq;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class ParametroCorreosMapper : Profile
    {
        public ParametroCorreosMapper()
        {
            CreateMap<ParametroCorreos, ParametroCorreosViewDto>()
                .ForMember(d => d.Id, options => options.MapFrom(s => s.Id))
                .ForMember(d => d.Destinatario, options => options.MapFrom(s => s.Destinatario))
                .ForMember(d => d.Indicadores, options => options.MapFrom<IndicadoresDescricionConverter>())
                .ForMember(d => d.Empresa, options => options.MapFrom<EmpresaCodigoToDescripcionConverter>())
                .ForMember(d => d.Departamento, options => options.MapFrom(s => s.Departamento))
                .ForMember(d => d.Vicepresidencia, options => options.MapFrom(s => s.Vicepresidencia));
            

            CreateMap<ParametroCorreosDto, ParametroCorreos>()
                .ForMember(d => d.Id, options => options.MapFrom(s => s.Id))
                .ForMember(d => d.Destinatario, options => options.MapFrom(s => s.Destinatario))
                .ForMember(d => d.Indicadores, options => options.MapFrom(s => string.Join(',', s.Indicadores.Where(i => i.Selected).Select(i => i.Name))))
                .ForMember(d => d.Empresa, options => options.MapFrom(s => s.Empresa))
                .ForMember(d => d.Departamento, options => options.MapFrom(s => s.Departamento))
                .ForMember(d => d.Vicepresidencia, options => options.MapFrom(s => s.Vicepresidencia));

            CreateMap<ParametroCorreos, ParametroCorreosDto>().ConvertUsing<ParametroCorreosToParametroCorreosDto>();
        }
    }
}
