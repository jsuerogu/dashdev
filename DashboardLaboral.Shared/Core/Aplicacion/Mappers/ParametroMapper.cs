using AutoMapper;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class ParametroMapper : Profile
    {
        public ParametroMapper()
        {
            CreateMap<ParametroDto, Parametro>()
                .ForMember(t => t.Empresa, option => option.MapFrom(s => s.Empresa))
                .ForMember(t => t.RowId, option => option.MapFrom(s => s.RowId))
                .ForMember(t => t.Descripcion, option => option.MapFrom(s => s.Descripcion))
                .ForMember(t => t.Valor, option => option.MapFrom(s => s.Valor))
                .ForMember(t => t.Parametro1, option => option.MapFrom(s => s.Nombre));

            CreateMap<Parametro, ParametroDto>()
                .ForMember(t => t.Empresa, option => option.MapFrom(s => s.Empresa.Trim()))
                .ForMember(t => t.RowId, option => option.MapFrom(s => s.RowId))
                .ForMember(t => t.Descripcion, option => option.MapFrom(s => s.Descripcion.Trim()))
                .ForMember(t => t.Valor, option => option.MapFrom(s => s.Valor.Trim()))
                .ForMember(t => t.Nombre, option => option.MapFrom(s => s.Parametro1.Trim()));

        }
    }
}
