using DashboardLaboral.Shared.Core.Aplicacion.Contratos;
using DashboardLaboral.Shared.Models;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DashboardLaboral.Shared.Core.Infrastructura.Services
{
    public class GraficoRender : IGraficoRender
    {
        private readonly ApexApiModel apexApiModel;
        private readonly IServiceProvider serviceProvider;
        private readonly IDataContext dataContext;
        private readonly RendeGraficosModel rendeGraficosModel;
        private readonly IGraficosService graficosService;


        public GraficoRender(IOptions<ApexApiModel> apexApiModel, IServiceProvider serviceProvider, IOptions<RendeGraficosModel> rendeGraficosModel, IDataContext dataContext, IGraficosService graficosService)
        {
            this.apexApiModel = apexApiModel.Value;
            this.serviceProvider = serviceProvider;
            this.rendeGraficosModel = rendeGraficosModel.Value;
            this.dataContext = dataContext;
            this.graficosService = graficosService;
        }

        public async Task<RenderResponse> GetBase64(FiltroModel filtro)
        {
            var indicadores = dataContext.ObtenerListaIndicadores();
            var response = new RenderResponse();

            #region inisistencia
            var request = rendeGraficosModel["pie"];
            request.config.labels.Clear();
            request.config.series.Clear();


            var indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataInasistencias", StringComparison.InvariantCultureIgnoreCase));
            filtro.Indicador = indicador.Value;

            var dataIncsistencia = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add(indicador.Display);
            request.config.series.Add(dataIncsistencia.Count());

            indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataOnPremise", StringComparison.InvariantCultureIgnoreCase));

            filtro.Indicador = indicador.Value;
            var dataOnPremise = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add("Asistencia");
            request.config.series.Add(dataOnPremise.Count());

            var model = JsonConvert.SerializeObject(request, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            response.GraficoInasistencia = await RenderGrafico(model);
            #endregion

            #region Tardanza
            request.config.labels.Clear();
            request.config.series.Clear();


            indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataTardanzas", StringComparison.InvariantCultureIgnoreCase));
            filtro.Indicador = indicador.Value;

            var dataTardanza = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add(indicador.Display);
            request.config.series.Add(dataTardanza.Count());

            indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataPresentes", StringComparison.InvariantCultureIgnoreCase));

            filtro.Indicador = indicador.Value;
            var dataATiempo = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add(indicador.Display);
            request.config.series.Add(dataATiempo.Count());

            model = JsonConvert.SerializeObject(request, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            response.GraficoTardanza = await RenderGrafico(model);
            #endregion

            #region Incumplimiento
            request.config.labels.Clear();
            request.config.series.Clear();


            indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataSalidaFueraHorario", StringComparison.InvariantCultureIgnoreCase));
            filtro.Indicador = indicador.Value;

            var dataIncumplimiento = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add(indicador.Display);
            request.config.series.Add(dataIncumplimiento.Count());

            indicador = indicadores.FirstOrDefault(i => i.Value.Equals("DataPresentes", StringComparison.InvariantCultureIgnoreCase));

            filtro.Indicador = indicador.Value;
            //var dataCumplimienmto = await filtro.DataHorario(serviceProvider);
            request.config.labels.Add("Cumplimiento Horario");
            request.config.series.Add(dataATiempo.Count());
            model = JsonConvert.SerializeObject(request, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            response.Graficoincumpolimiento = await RenderGrafico(model);
            #endregion

            #region HorasExtras
 
            filtro.Indicador = "";

            var dataHorasExtras = await graficosService.ObtenerGraficoHorasExtras(filtro.FechaInicial, filtro.FechaFinal, Convert.ToInt32(filtro.Colaborador), filtro.Empresa
                , filtro.Vicepresidencia, filtro.Departamento);
            model = $@"{{
    ""width"": 600,
    ""height"": 300,
    ""config"": {{
        ""legend"": {{
            ""position"": ""top""
        }},
        ""fill"": {{
            ""type"": ""solid"",
            ""opacity"": 1
        }},
        ""colors"": [""#BF8F61"", ""#00468B""],
        ""series"": [{{
            ""name"": ""Horas Extras Plan"",
            ""data"": [{dataHorasExtras.HorasExtrasPlanificadas}]
        }}, {{
            ""name"": ""Horas Extras Real"",
            ""data"": [{dataHorasExtras.HorasExtras}]
        }}],
        ""chart"": {{
            ""type"": ""bar"",
            ""height"": 300
        }},
        ""plotOptions"": {{
            ""bar"": {{
                ""horizontal"": true,
                ""dataLabels"": {{
                    ""position"": ""top""
                }}
            }}
        }},
        ""xaxis"": {{
            ""categories"": [""{dataHorasExtras.MesAno}""]
        }}
    }}
}}";

            response.GraficoHorasExtras = await RenderGrafico(model);
            #endregion

            return response;
        }

        private async Task<string> RenderGrafico(string data)
        {

            HttpContent body = new StringContent(data, Encoding.UTF8, "application/json");
            Stream response = null;
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(apexApiModel.UrlRenderGrafico),
                    Content = body
                };

                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStreamAsync();
                }
            }

            
            return response.ConvertToBase64();
        }
    }
}
