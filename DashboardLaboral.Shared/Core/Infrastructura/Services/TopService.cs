using DashboarLaboral.Core.Aplicacion.Constants;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Infrastructura.Repositorios;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models.Tops;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    public class TopService : ITopService
    {
        private readonly DataSalidaFueraHorario dataSalidaFueraHorario;
        private readonly DataHorasExtras dataHorasExtras;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IFechaService fechaService;
        private readonly IRepositoryEmpresa repositoryEmpresa;

        public TopService(DataSalidaFueraHorario dataSalidaFueraHorario, DataHorasExtras dataHorasExtras, IHttpContextAccessor httpContextAccessor, IFechaService fechaService, IRepositoryEmpresa repositoryEmpresa)
        {
            this.dataSalidaFueraHorario = dataSalidaFueraHorario;
            this.dataHorasExtras = dataHorasExtras;
            this.httpContextAccessor = httpContextAccessor;
            this.fechaService = fechaService;
            this.repositoryEmpresa = repositoryEmpresa;
        }

        #region Departamentos
        public async Task<TopModel> ObtenerTopMayorIncumplimientoDepartamentoAsync(DateTime fecha, bool hasLink)
        {
            IQueryable<Horario> data = await dataSalidaFueraHorario.ConsultaData(fecha.AddDays(DateConstant.FechaMenos30Dias), fecha, false);
            var totalInclumplimiento = data.Count();

            var rangoFecha = httpContextAccessor.ObtenerRangoHora();

            var dataSeleccionada = data
                .GroupBy(d => new { d.Empresa, d.Departamento})
                .Select(g => new DataModel { ExtraData = new List<string> { g.Key.Empresa, g.Key.Departamento}, Descripcion = g.Key.Departamento, ValorMensual = g.Count(), Formato = "n2" })
                .ToList();

            foreach (var item in dataSeleccionada)
            {
                var nombreEmpresa = (await repositoryEmpresa.FindAsync(item.ExtraData[0]))?.Empresa1 ?? "";
                item.ExtraData[0] = nombreEmpresa;
                var porc = (item.ValorMensual * 100) / totalInclumplimiento;
                item.ExtraData.Add($"{porc.ToString("n0")}%");
            }


            return await Task.FromResult(
                new TopModel
                {
                    Id = 1,
                    IdParent = "Acordion1",
                    IdHeader = "Header1",
                    Titulo = "INCUMPLIMIENTO",
                    SubTitulo = "TOP 10 ÁREAS",
                    SubTitulo2 = "ÚLTIMOS 30 DÍAS",
                    Columnas = new List<ColumnaModel>
                    {
                        new ColumnaModel { Titulo = "EMPRESA"},
                        new ColumnaModel { Titulo = "ÁREA"},
                        new ColumnaModel { Titulo = "PORCENTAJE", Tooltip = "% de incumplimientos de los días trabajados p/p en los últimos 30 días."},
                        new ColumnaModel { Titulo = "CANTIDAD", Class="text-align:right", Tooltip = "Cantidad de incumplimiento"}
                    },
                    Valores = dataSeleccionada.OrderByDescending(d => d.ValorMensual).Take(10).ToList(),
                    Class = "show",
                    HasLink = hasLink
                }
                ); ;
        }

        public async Task<TopModel> ObtenerTopMayorHorasExtrasDepartamentoAsync(DateTime fecha, bool hasLink)
        {
            IQueryable<Horario> data = await dataHorasExtras.ConsultaData(fecha.AddDays(DateConstant.FechaMenos30Dias), fecha, false);

            var dataSeleccionada = data
                .GroupBy(d => new { d.Empresa, d.Departamento })
                .Select(g => new DataModel { ExtraData = new List<string> { g.Key.Empresa, g.Key.Departamento}, Descripcion = g.Key.Departamento, ValorMensual = g.Sum(d => d.Horasextras), Formato = "n2", DatosClase = "DataHorasExtras" })
                .ToList();

            foreach (var item in dataSeleccionada)
            {
                var nombreEmpresa = (await repositoryEmpresa.FindAsync(item.ExtraData[0]))?.Empresa1 ?? "";
                item.ExtraData[0] = nombreEmpresa;
            }

            return await Task.FromResult(
                new TopModel
                {
                    Id = 3,
                    IdParent = "Acordion3",
                    IdHeader = "Header3",
                    Titulo = "HORAS EXTRAS",
                    SubTitulo = "TOP 10 ÁREAS",
                    SubTitulo2 = "ÚLTIMOS 30 DÍAS",
                    Columnas = new List<ColumnaModel>
                    {
                        new ColumnaModel { Titulo = "EMPRESA"},
                        new ColumnaModel { Titulo = "ÁREA"},
                        new ColumnaModel { Titulo = "CANTIDAD", Class="text-align:right", Tooltip = "Cantidad de horas extras"}
                    },
                    Valores = dataSeleccionada.OrderByDescending(d => d.ValorMensual).Take(10).ToList(),
                    Class = "show",
                    HasLink = hasLink
                }
                ); ;
        }
        #endregion

        #region Empleados
        public async Task<TopModel> ObtenerTopMayorIncumplimientoEmpleadosAsync(DateTime fecha, bool hasLink)
        {
            IQueryable<Horario> data = await dataSalidaFueraHorario.ConsultaData(fecha.AddDays(DateConstant.FechaMenos30Dias), fecha, false);

            var rangoFecha = httpContextAccessor.ObtenerRangoHora();

            var dataSeleccionada = data
                .GroupBy(d => new { d.Empresa, d.Departamento, d.Nombrecompleto })
                .Select(g => new DataModel { ExtraData = new List<string> { g.Key.Empresa, g.Key.Departamento, g.Key.Nombrecompleto }, Descripcion = g.Key.Nombrecompleto, ValorMensual = g.Count(), Formato = "n2"})
                .ToList();
        
            return await Task.FromResult(
                new TopModel
                {
                    Id = 5,
                    IdParent = "Acordion5",
                    IdHeader = "Header5",
                    Titulo = "INCUMPLIMIENTO",
                    SubTitulo = "TOP 10 EMPLEADOS",
                    SubTitulo2 = "ÚLTIMOS 30 DÍAS",
                    Columnas = new List<ColumnaModel>
                    {
                        new ColumnaModel { Titulo = "EMPRESA"},
                        new ColumnaModel { Titulo = "DEPARTAMENTO"},
                        new ColumnaModel { Titulo = "EMPLEADO"},
                        new ColumnaModel { Titulo = "CANTIDAD", Class="text-align:right", Tooltip = "Cantidad de incumplimiento"}
                    },
                    Valores = dataSeleccionada.OrderByDescending(d => d.ValorMensual).Take(10).ToList(),
                    Class = ""
                }
                ); ;
        }
        public async Task<TopModel> ObtenerTopMayorHorasExtrasEmpleadosAsync(DateTime fecha, bool hasLink)
        {
            IQueryable<Horario> data = await dataHorasExtras.ConsultaData(fecha.AddDays(DateConstant.FechaMenos30Dias), fecha, false);

            var dataSeleccionada = data
                .GroupBy(d => new { d.Empresa, d.Departamento, d.Nombrecompleto })
                .Select(g => new DataModel { ExtraData = new List<string> { g.Key.Empresa, g.Key.Departamento, g.Key.Nombrecompleto }, Descripcion = g.Key.Nombrecompleto, ValorMensual = g.Sum(d => d.Horasextras), Formato = "n2", DatosClase = "DataHorasExtras" })
                .ToList();

            foreach (var item in dataSeleccionada)
            {
                var nombreEmpresa = (await repositoryEmpresa.FindAsync(item.ExtraData[0]))?.Empresa1 ?? "";
                item.ExtraData[0] = nombreEmpresa; 
            }

            return await Task.FromResult(
                new TopModel
                {
                    Id = 7,
                    IdParent = "Acordion7",
                    IdHeader = "Header3",
                    Titulo = "HORAS EXTRAS",
                    SubTitulo = "TOP 10 EMPLEADOS",
                    SubTitulo2 = "ÚLTIMOS 30 DÍAS",
                    Columnas = new List<ColumnaModel>
                    {
                        new ColumnaModel { Titulo = "EMPRESA"},
                        new ColumnaModel { Titulo = "DEPARTAMENTO"},
                        new ColumnaModel { Titulo = "EMPLEADO"},
                        new ColumnaModel { Titulo = "CANTIDAD", Class="text-align:right", Tooltip = "Cantidad de horas extras"}
                    },
                    Valores = dataSeleccionada.OrderByDescending(d => d.ValorMensual).Take(10).ToList(),
                    Class = ""
                }
                ); ;
        }
        
        #endregion
    }
}
