using AutoMapper;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class PosicionesFeriadosMapper : Profile
    {
        public PosicionesFeriadosMapper()
        {
            CreateMap<PosicionesFeriadosDto, PosicionesFeriados>()
                .ForMember(t => t.Id, option => option.MapFrom(s => s.Id))
                .ForMember(t => t.Posicion, option => option.MapFrom(s => s.Posicion)).ReverseMap();
        }
    }
}
