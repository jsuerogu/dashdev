using AutoMapper;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class EmpresaMapper : Profile
    {
        public EmpresaMapper()
        {
            CreateMap<EmpresaDto, Empresa>()
                .ForMember(t => t.CodigoEmpresa, option => option.MapFrom(s => s.Codigo))
                .ForMember(t => t.Empresa1, option => option.MapFrom(s => s.Nombre))
                .ForMember(t => t.Color, option => option.MapFrom(s => s.Color));

            CreateMap<Empresa, EmpresaDto>()
                .ForMember(t => t.Codigo, option => option.MapFrom(s => s.CodigoEmpresa.Trim()))
                .ForMember(t => t.Nombre, option => option.MapFrom(s => s.Empresa1.Trim()))
                .ForMember(t => t.Color, option => option.MapFrom(s => s.Color.Trim()))
                .ForMember(t => t.RowId, option => option.MapFrom(s => s.RowId));

        }
    }
}
