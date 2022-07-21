using AutoMapper;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class AusentismoMapper : Profile
    {
        public AusentismoMapper()
        {
            CreateMap<AusentismoDto, Ausentismo>()
                .ForMember(t => t.Id, option => option.MapFrom(s => s.Id))
                .ForMember(t => t.Aucod, option => option.MapFrom(s => s.Aucod))
                .ForMember(t => t.Audes, option => option.MapFrom(s => s.Audes))
                .ForMember(t => t.Aujus, option => option.MapFrom(s => s.Aujus))
                .ForMember(t => t.Autel, option => option.MapFrom(s => s.Autel))
                .ForMember(t => t.Riesgo, option => option.MapFrom(s => s.Riesgo ? 1 : 0))
                .ForMember(t => t.Cuarentena, option => option.MapFrom(s => s.Cuarentena ? 1 : 0));

            CreateMap<Ausentismo, AusentismoDto>()
                .ForMember(t => t.Id, option => option.MapFrom(s => s.Id.Trim()))
                .ForMember(t => t.Aucod, option => option.MapFrom(s => s.Aucod.Trim()))
                .ForMember(t => t.Audes, option => option.MapFrom(s => s.Audes.Trim()))
                .ForMember(t => t.Aujus, option => option.MapFrom(s => s.Aujus))
                .ForMember(t => t.Autel, option => option.MapFrom(s => s.Autel))
                .ForMember(t => t.Riesgo, option => option.MapFrom(s => s.Riesgo == 1 ? true : false))
                .ForMember(t => t.Cuarentena, option => option.MapFrom(s => s.Cuarentena == 1 ? true : false));

        }
    }
}
