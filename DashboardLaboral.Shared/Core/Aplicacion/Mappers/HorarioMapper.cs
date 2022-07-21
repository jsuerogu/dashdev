using AutoMapper;
using DashboardLaboral.Shared.Core.Aplicacion.Mappers.Converters;
using DashboardLaboral.Shared.Models;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using System;

namespace DashboarLaboral.Core.Aplicacion.Mappers
{
    public class HorarioMapper : Profile
    {

        public HorarioMapper()
        {
            CreateMap<Horario, HorarioModel>()
                .ForMember(d => d.Fecha, opt => opt.MapFrom(s => s.Fecha.ToShortDateString()))
                .ForMember(d => d.CantHoras, opt => opt.MapFrom(s => s.CANTHoras.ToString("n2")))
                .ForMember(d => d.Codigo, opt => opt.MapFrom(s => s.Codigoempleado.HasValue ? s.Codigoempleado.Value.ToString() : "0"))
                .ForMember(d => d.Departamento, opt => opt.MapFrom(s => s.Departamento))
                .ForMember(d => d.Entrada, opt => opt.MapFrom(s => s.Poncheentrada.HasValue ? s.Poncheentrada.Value.ToShortTimeString() : ""))
                .ForMember(d => d.Horario, opt => opt.MapFrom(s => s.Horaini.HasValue && s.Horafin.HasValue ? $"{s.Horaini.Value.ToShortTimeString()} - {s.Horafin.Value.ToShortTimeString()}" : "Sin horario"))
                .ForMember(d => d.Indicador, opt => opt.MapFrom<IndicadorConverter>())
                .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.Nombrecompleto))
                .ForMember(d => d.Posicion, opt => opt.MapFrom(s => s.Posicion))
                .ForMember(d => d.Salida, opt => opt.MapFrom(s => s.Ponchesalida.HasValue ? s.Ponchesalida.Value.ToShortTimeString() : ""))
                .ForMember(d => d.Horasextras, opt => opt.MapFrom(s => s.Horasextras.ToString("n2")));

        }
    }
}
