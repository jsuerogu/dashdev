using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Mappers.Converters;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class PosicionOffPremiseMapper : Profile
    {
        public PosicionOffPremiseMapper()
        {
            CreateMap<PosicionOffPremiseHeader, PosicionOffPremiseHeaderViewDto>()
                .ForMember(d => d.Id, options => options.MapFrom(s => s.Id))
                .ForMember(d => d.Posicion, options => options.MapFrom(s => s.Posicion))
                .ForMember(d => d.Empresa, options => options.MapFrom<PosicionOffPremiseCodigoToDescripcionConverter>())
                .ForMember(d => d.Departamento, options => options.MapFrom(s => s.Departamento))
                .ForMember(d => d.VicePresidencia, options => options.MapFrom(s => s.VicePresidencia));

            CreateMap<PosicionOffPremiseHeader,PosicionOffPremiseHeaderDto>().ConvertUsing<PosicionOffPremiseToPosicionOffPremiseDto>();
            CreateMap<PosicionOffPremiseHeader, PosicionOffPremiseHeaderDetailDto>().ConvertUsing<PosicionOffPremiseHeaderToPosicionOffPremiseHeaderDetailDto>();
            CreateMap<PosicionOffPremiseHeaderDto, PosicionOffPremiseHeader>();
            CreateMap<PosicionOffPremiseDetailsDto, PosicionOffPremiseDetails>().ReverseMap();
        }
    }
}
