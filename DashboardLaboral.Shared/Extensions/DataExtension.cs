using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Linq.Expressions;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace DashboarLaboral.Extensions
{
    public record OrigenDatos(Expression<Func<Horario, bool>> FiltroIndicador, FiltroModel Filtro);

    public static class DataExtension
    {

        internal static async Task<IQueryable<Horario>> ObtenerDataConsulta(this insitedb contex)
        {
            return await Task.FromResult(contex.Horarios.Include(e => e.Ausentismo).AsQueryable());
        }
        internal static string ConvertindicadorToTipo(this string indicador, string tipo2)
        {
            switch (indicador)
            {
                case "notocatrabajar":
                    return "No les toca trabajar";
                case "presentes":
                    return "A tiempo";
                case "tardanzas":
                    return "Tardanza";
                case "inasistencias":
                    return "Inasistencia";
                case "offpremise":
                    return "Off-Premise";
                case "ausenciajust":
                    return tipo2;
                default:
                    return "Definir";
            }
        }
        internal static OrigenDatos ObtenerDataindicador(this IServiceProvider serviceProvider, FiltroModel filtro)
        {
            Dictionary<string, IndicadorDataAttribute> indicadores = serviceProvider.GetService<Dictionary<string, IndicadorDataAttribute>>();
            IData indicador = null;

            if (!string.IsNullOrEmpty(filtro.Indicador))
                indicador = (IData)serviceProvider.GetService(indicadores[filtro.Indicador].DataType);

            return new OrigenDatos(indicador?.Filtro, filtro);
        }

        private static async Task<IQueryable<Horario>> GenerateData(this OrigenDatos origenDatos, IServiceProvider serviceProvider)
        {
            IQueryable<Horario> result = null;
            IDataContext dataContext = serviceProvider.GetService<IDataContext>();
            var context = serviceProvider.GetService<insitedb>();

            result = await context.ObtenerDataConsulta();
            if (origenDatos.FiltroIndicador != null)
                result = result.Where(origenDatos.FiltroIndicador);

            result = await result.FiltroData(origenDatos.Filtro, serviceProvider);

            if (!string.IsNullOrEmpty(origenDatos.Filtro.SearchValue))
                result = result.Where(d =>
                    d.Codigoempleado.ToString().ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Departamento.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Codigoempleado.ToString().ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Nombrecompleto.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Posicion.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Nombresupervisor.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Correosupervisor.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Telefono.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower())
                    || d.Correo.ToLower().Contains(origenDatos.Filtro.SearchValue.ToLower()));

            return result;
        }

        public static async Task<List<string>> ObtenerSupervisores(this FiltroModel filtro, IServiceProvider serviceProvider)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(filtro);
            IQueryable<Horario> source = await origenDatos.GenerateData(serviceProvider);
            List<SupervisorConsultaModel> supervisorConsultaModels = new();

            return source.Where(d => !string.IsNullOrEmpty(d.Correosupervisor) && d.Correosupervisor.Contains("@"))
                .GroupBy(d => d.Correosupervisor)
                .Select(g => g.Key)
                .ToList();

        }

        public static async Task<SupervisorConsultaModel> GenerarResultadoSupervisor(this FiltroModel filtro, IServiceProvider serviceProvider, string correoSupervisor)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(filtro);


            IQueryable<Horario> source = await origenDatos.GenerateData(serviceProvider);
            source = source.Where(d => d.Correosupervisor == correoSupervisor);

            SupervisorConsultaModel model = new SupervisorConsultaModel { CorreoSupervisor = correoSupervisor, Total = source.Count() };

            model.Departamentos = source.GroupBy(g => g.Departamento)
                 .Select(g => new DepartamentoModel { Titulo = g.Key, Total = g.Count() })
                 .ToList();

            int id = 1;

            model.Departamentos.ForEach(d =>
            {
                d.Id = id++;
            });
            return model;
        }

        public static async Task<DepartamentoConsultaModel> GenerarResultado(this FiltroModel filtro, IServiceProvider serviceProvider)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(filtro);


            IQueryable<Horario> source = await origenDatos.GenerateData(serviceProvider);

            DepartamentoConsultaModel model = new DepartamentoConsultaModel { Total = source.Count() };

            model.Departamentos = source.GroupBy(g => g.Departamento)
                 .Select(g => new DepartamentoModel { Titulo = g.Key, Total = g.Count() })
                 .ToList();

            int id = 1;

            model.Departamentos.ForEach(d =>
            {
                d.Id = id++;
            });
            return model;
        }

        public static async Task<IQueryable<Horario>> FiltroData(this IQueryable<Horario> data, FiltroModel filtro, IServiceProvider serviceProvider)
        {
            if (!filtro.RangoFecha)
                filtro.FechaFinal = filtro.FechaInicial;

            data = data.Where(h => h.Fecha >= filtro.FechaInicial.Date && h.Fecha <= filtro.FechaFinal.Date);

            if (!string.IsNullOrEmpty(filtro.Empresa))
            {
                data = data.Where(d => d.Empresa == filtro.Empresa);

            }

            if (!string.IsNullOrEmpty(filtro.Vicepresidencia))
            {
                data = data.Where(d => d.Vicepresidencia == filtro.Vicepresidencia);

            }
            if (!string.IsNullOrEmpty(filtro.Departamento))
            {
                data = data.Where(d => d.Departamento == filtro.Departamento);
            }

            var fechaService = serviceProvider.GetService<IFechaService>();
            if (!filtro.RangoFecha)
            {
                if (filtro.RangoHora)
                {
                    if (fechaService != null)
                        data = data.Where(d => d.Horaini.Value <= fechaService.FechaHora && d.Horafin >= fechaService.FechaHora);
                }
                else
                {
                    if (fechaService != null)
                        data = data.Where(d => d.Horaini.Value <= fechaService.FechaHora);
                }
            }

            if (!string.IsNullOrEmpty(filtro.Colaborador))
                data = data.Where(d => d.Administrativo == Convert.ToInt32(filtro.Colaborador));

            return await Task.FromResult(data);

        }

        public static async Task<IQueryable<Horario>> Filtro(this IData dataSource, FiltroModel filtro)
        {

            IQueryable<Horario> data = default;

            if (filtro.RangoFecha)
                data = await dataSource.ConsultaData(filtro.FechaInicial, filtro.FechaFinal);
            else
                data = await dataSource.ConsultaData(filtro.FechaInicial, filtro.FechaInicial);


            if (!string.IsNullOrEmpty(filtro.Empresa))
            {
                data = data.Where(d => d.Empresa == filtro.Empresa);

            }

            if (!string.IsNullOrEmpty(filtro.Vicepresidencia))
            {
                data = data.Where(d => d.Vicepresidencia == filtro.Vicepresidencia);

            }
            if (!string.IsNullOrEmpty(filtro.Departamento))
            {
                data = data.Where(d => d.Departamento == filtro.Departamento);
            }
            return data;
        }

        public static Task<PaginateDataResponseModel<T>> PaginatedData<T, S>(this ConsultaDataRequestModel request, IQueryable<S> sourceData, IMapper mapper, Expression<Func<S, bool>> searchPredicate = null)
        {
            var totalRegistros = sourceData?.Count() ?? 0;
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / request.Length);

            try
            {

                if (searchPredicate is not null)
                    sourceData = sourceData.Where(searchPredicate);


                var result = totalRegistros > 0
                    ? sourceData
                    .Skip(request.Start)
                    .Take(request.Length)
                    .ToList()
                    : new List<S>();


                return Task.FromResult(new PaginateDataResponseModel<T>()
                {
                    RecordsTotal = totalRegistros,
                    RecordsFiltered = totalRegistros,
                    Draw = request.Draw,
                    Data = mapper.Map<List<T>>(result)
                });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<PaginateDataResponseModel<HorarioModel>> PaginateDataHorario(this ConsultaDataRequestModel request, IServiceProvider serviceProvider)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(request.Filtro);
            IMapper mapper = serviceProvider.GetService<IMapper>();

            IQueryable<Horario> result = await origenDatos.GenerateData(serviceProvider);
            var totalRegistros = result?.Count() ?? 0;
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / request.Length);
            IQueryable<Horario> source = null;
            source = result.Orderable(request.Filtro)
                    .ThenBy(o => o.Fecha)
                    .Skip(request.Start)
                    .Take(request.Length);

            var dataList = source.ToList();
            var data = mapper.Map<List<HorarioModel>>(dataList);
            
            return new PaginateDataResponseModel<HorarioModel>()
            {
                RecordsTotal = totalRegistros,
                RecordsFiltered = totalRegistros,
                Draw = request.Draw,
                Data = data
            };
        }


        public static async Task<List<HorarioModel>> DataHorario(this FiltroModel filtro, IServiceProvider serviceProvider, Expression<Func<Horario, bool>> extraFilter = null)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(filtro);
            IMapper mapper = serviceProvider.GetService<IMapper>();

            IQueryable<Horario> result = await origenDatos.GenerateData(serviceProvider);

            if (extraFilter != null)
                result = result.Where(extraFilter);

            var source = result
                .OrderBy(d => d.Codigoempleado)
                .ThenBy(d => d.Fecha)
                .ToList();



            return mapper.Map<List<HorarioModel>>(source);
        }

        public static async Task<List<HorarioModel>> DataHorarioSupervisor(this FiltroModel filtro, IServiceProvider serviceProvider, string correoSupervisor)
        {
            OrigenDatos origenDatos = serviceProvider.ObtenerDataindicador(filtro);
            IMapper mapper = serviceProvider.GetService<IMapper>();

            IQueryable<Horario> result = await origenDatos.GenerateData(serviceProvider);

            var source = result
                .Where(d => d.Correosupervisor == correoSupervisor)
                .OrderBy(d => d.Codigoempleado)
                .ThenBy(d => d.Fecha)
                .ToList();



            return mapper.Map<List<HorarioModel>>(source);
        }
        private static IOrderedQueryable<Horario> Orderable(this IQueryable<Horario> source, FiltroModel filtro)
        {
            Expression<Func<Horario, object>> order = d => d.Nombrecompleto;

            switch (filtro.OrderColumn)
            {
                case 1:
                    //order = d => d.Tipo;
                    break;
                case 2:
                    order = d => d.Departamento;
                    break;
                case 3:
                    order = d => d.Codigoempleado;
                    break;
                case 4:
                    order = d => d.Nombrecompleto;
                    break;
                case 5:
                    order = d => d.Posicion;
                    break;
                case 6:
                    order = d => d.Horaini;
                    break;
                case 7:
                    order = d => d.Poncheentrada;
                    break;
                case 8:
                    order = d => d.Ponchesalida;
                    break;
                case 9:
                    order = d => d.Horasextras;
                    break;
                case 10:
                    order = d => d.Nombresupervisor;
                    break;
                case 11:
                    order = d => d.Correosupervisor;
                    break;
                case 12:
                    order = d => d.Telefono;
                    break;
                case 13:
                    order = d => d.Correo;
                    break;
                case 14:
                    order = d => d.Fecha;
                    break;
                default:
                    break;
            }
            return filtro.OrderDirection == "asc" ? source.OrderBy(order)
                : source.OrderByDescending(order);
        }
    }
}
