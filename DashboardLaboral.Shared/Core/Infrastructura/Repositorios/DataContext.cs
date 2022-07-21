using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using DashboarLaboral.Models.Graficos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboarLaboral.Core.Infrastructura.Repositorios
{
    public class DataContext : IDataContext
    {
        private readonly insitedb context;
        private readonly string empresa;
        private readonly string vicepresidencia;
        private readonly bool rangoHora;
        private readonly int colaborador;
        private readonly Dictionary<string, IndicadorDataAttribute> indicadores;
        private readonly IFechaService fechaService;

        public DataContext(insitedb context, IHttpContextAccessor httpContext, Dictionary<string, IndicadorDataAttribute> indicadores, IFechaService fechaService)
        {

            empresa = httpContext.ObtenerEmpresa();
            vicepresidencia = httpContext.ObtenerVicePresidencia();
            rangoHora = httpContext.ObtenerRangoHora();
            colaborador = httpContext.ObtenerColaborador();
            this.context = context;
            this.indicadores = indicadores;
            this.fechaService = fechaService;
        }

        public IQueryable<Horario> ObtenerData(bool isHourFilter = true)
        {
            IQueryable<Horario> data = context.ObtenerDataConsulta().GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(empresa))
            {
                data = data.Where(h => h.Empresa == empresa);
                if(!string.IsNullOrEmpty(vicepresidencia)  )
                {
                    data = data.Where(h => h.Vicepresidencia == vicepresidencia);
                }
            }

            if (isHourFilter)
            {
                if (rangoHora)
                {
                    var currentHora = fechaService.FechaHora;
                    data = data.Where(p => p.Horaini.Value <= currentHora && p.Horafin >= currentHora);
                }
            }

            if (colaborador >= 0)
                data = data.Where(p => p.Administrativo == colaborador);

            return data;
        }

        public int MinutosMaxAusencia()
        {
            int minutosMaximoAusencia = 0;
            var valor = context.Parametros.SingleOrDefault(p => p.Parametro1 == "MINUT0_MAX_AUSEMCIA")?.Valor ?? "0";

            if (!int.TryParse(valor, out minutosMaximoAusencia)) return 0;

            return minutosMaximoAusencia;
        }

        public decimal HorasAlmuerzoAdm()
        {
            decimal horasAlmuerzoAdm = 0;
            var valor = context.Parametros.SingleOrDefault(p => p.Parametro1 == "HORAS_ALMUERZO_ADM")?.Valor ?? "0";

            if (!decimal.TryParse(valor, out horasAlmuerzoAdm)) return 0;

            return horasAlmuerzoAdm;
        }

        public int MinutosMaxTardanza()
        {
            int minutosMaximoTardanza = 0;
            var valor = context.Parametros.SingleOrDefault(p => p.Parametro1 == "MINUTO_MAX_TARDANZA")?.Valor ?? "0";

            if (!int.TryParse(valor, out minutosMaximoTardanza)) return 0;

            return minutosMaximoTardanza;
        }


        public IList<string> ObtenerAusentismo()
        {
            return context.Ausentismos.Where(a => !a.Aujus).Select(a => a.Aucod).ToList();
        }

        public IList<string> ObtenerAusenciaJustificadas()
        {
            return context.Ausentismos.Where(a => a.Aujus && a.Cuarentena != 1 && a.Riesgo != 1).Select(p => p.Aucod).ToList();
        }

        public IList<string> ObtenerCondicionRiesgo()
        {
            return context.Ausentismos.Where(a => a.Riesgo == 1).Select(p => p.Aucod).ToList();
        }

        public IList<string> ObtenerCuarentena()
        {
            return context.Ausentismos.Where(a => a.Cuarentena == 1).Select(p => p.Aucod).ToList();
        }

        public List<ValueDisplayModel> ObtenerEmpresas(string emptyLabel = "Todas")
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>()
            {
                new ValueDisplayModel{ Value = "", Display = emptyLabel }
            };

            items.AddRange(context.Empresas.OrderBy(e => e.CodigoEmpresa).Select(e => new ValueDisplayModel { Value = e.CodigoEmpresa.Trim(), Display = e.Empresa1.Trim() }));
            return items;
        }

        public List<ValueDisplayModel> ObtenerVicepresidencias(string empresa, string emptyLabel = "Todas")
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>()
            {
                new ValueDisplayModel{ Value = "", Display = emptyLabel }
            };

            if (string.IsNullOrEmpty(empresa)) return items;

            items.AddRange(context.Horarios.Where(h => h.Empresa == empresa)
                .OrderBy(h => h.Vicepresidencia)
                .GroupBy(h => h.Vicepresidencia)
                .Select(g => new ValueDisplayModel { Value = g.Key.Trim(), Display = g.Key.Trim() })
                .ToList());

            return items;
        }

        public int AdminHorasLab()
        {
            int adminHorasLab = 0;
            var valor = context.Parametros.SingleOrDefault(p => p.Parametro1 == "ADMIN_HORAS_LAB")?.Valor ?? "0";

            if (!int.TryParse(valor, out adminHorasLab)) return 0;

            return adminHorasLab;
        }

        public List<ValueDisplayModel> ObtenerDepartamentos(string empresa, string vicepresidencia, bool isHourFilter = true, string emptyLabel = "Todos")
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>()
            {
                new ValueDisplayModel{ Value = "", Display = emptyLabel }
            };

            IQueryable<Horario> source = context.Horarios.AsQueryable();

            if (!string.IsNullOrEmpty(empresa))
            {
                source = source.Where(p => p.Empresa == empresa);
            }

            if (!string.IsNullOrEmpty(vicepresidencia))
            {
                source = source.Where(p => p.Vicepresidencia == vicepresidencia);
            }

            if (isHourFilter)
            {
                if (rangoHora)
                {
                    var currentHora = fechaService.FechaHora;
                    source = source.Where(p => p.Horaini.Value <= currentHora && p.Horafin >= currentHora);
                }
            }

            items.AddRange(source
                .OrderBy(h => h.Departamento)
                .GroupBy(h => h.Departamento)
                .Select(g => new ValueDisplayModel { Value = g.Key.Trim(), Display = g.Key.Trim() })
                .ToList()
                .OrderBy(d => d.Display).ToList());

            return items;
        }

        public List<ValueDisplayModel> ObtenerListaIndicadores()
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>()
            {
                new ValueDisplayModel{ Value = "", Display = "Todos" }
            };

            items.AddRange(indicadores.Select(e => new ValueDisplayModel { Value = e.Key, Display = e.Value.Titulo }));
            return items;
        }

        public IQueryable<Ausentismo> DbTableAusentismo()
        {
            return context.Ausentismos.AsQueryable();
        }

        public Parametro ObtenerParametros(string parameterName)
        {
            return context.Parametros.FirstOrDefault(p => p.Parametro1.ToLower() == parameterName.ToLower());
        }

        public List<ParametroCorreoModel> ObtenerParametrosCorreo()
        {
            var data = context.ParametroCorreos.Where(p => !string.IsNullOrEmpty(p.Destinatario) && !string.IsNullOrEmpty(p.Empresa) && !string.IsNullOrEmpty(p.Vicepresidencia) && !string.IsNullOrEmpty(p.Departamento) && !string.IsNullOrEmpty(p.Indicadores)).OrderBy(p => p.Destinatario).ToList();

            var destinatarios = data.SelectMany(d => d.Destinatario.Split(',', System.StringSplitOptions.TrimEntries))
                .GroupBy(d => d.ToLower())
                .Select(g => g.Key);

            List<ParametroCorreos> dataDestinatario = new();

            dataDestinatario = destinatarios.Aggregate(dataDestinatario, (current, destinatario) =>
            {
                var tempData = data.Where(d => d.Destinatario.ToLower().Contains(destinatario))
                .Select(x => new ParametroCorreos()
                {
                    Destinatario = destinatario,
                    Departamento = x.Departamento,
                    Empresa = x.Empresa,
                    Id = x.Id,
                    Indicadores = x.Indicadores,
                    Vicepresidencia = x.Vicepresidencia
                })
                .ToList();

                current.AddRange(tempData);

                return current;
            });


            var model = dataDestinatario.GroupBy(p => new { p.Destinatario })
                .Select(g => new ParametroCorreoModel()
                {
                    Destinatario = g.Key.Destinatario,
                    Empresas = g.GroupBy(e => e.Empresa).Select(e => new ParametroCorreoEmpresaModel(e.Key, e.ToList())).ToList(),
                    Indicadores = string.Join(',', g.GroupBy(x => x.Indicadores).Select(i => i.Key))
                }).ToList();

            return model;
        }

        public Empresa ObtenerDetalleEmpresa(string code)
        {
            return context.Empresas.FirstOrDefault(e => e.CodigoEmpresa == code);
        }

        public List<ValueDisplayModel> ObtenerPosiciones(string empresa, string vicepresidencia, string departamento, string emptyLabel = "Seleccionar")
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>()
            {
                new ValueDisplayModel{ Value = "", Display = emptyLabel }
            };

            if (string.IsNullOrEmpty(empresa)
                || string.IsNullOrEmpty(vicepresidencia)
                || string.IsNullOrEmpty(departamento)) return items;

            items.AddRange(context.Horarios.Where(h => h.Empresa == empresa 
                && h.Vicepresidencia == vicepresidencia
                && h.Departamento == departamento)
                .OrderBy(h => h.Posicion)
                .GroupBy(h => h.Posicion)
                .Select(g => new ValueDisplayModel { Value = g.Key.Trim(), Display = g.Key.Trim() })
                .ToList());

            return items;
        }

        public List<ValueDisplayModel> ObtenerListaEmpleados(string empresa, string vicepresidencia, string departamento, string posicion)
        {
            List<ValueDisplayModel> items = new List<ValueDisplayModel>();

            if (string.IsNullOrEmpty(empresa)
                || string.IsNullOrEmpty(vicepresidencia)
                || string.IsNullOrEmpty(departamento)
                || string.IsNullOrEmpty(posicion)) return items;

            items.AddRange(context.Horarios.Where(h => h.Empresa == empresa
                && h.Vicepresidencia == vicepresidencia
                && h.Departamento == departamento
                && h.Posicion == posicion)
                .OrderBy(h => h.Codigoempleado)
                .GroupBy(h => new { h.Codigoempleado, h.Nombrecompleto })
                .Select(g => new ValueDisplayModel { Value = g.Key.Codigoempleado.ToString(), Display = g.Key.Nombrecompleto })
                .ToList());

            return items;
        }

        public IEnumerable<int> IsPosicionOffPremise()
        {

            return context.PosicionOffpremiseDetails
                .Where(d => d.Selected)
                .Select(d => d.CodigoEmpleado)
                .AsEnumerable();
        }
    }
}
