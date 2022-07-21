using DashboarLaboral.Core.Aplicacion;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Infrastructura.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DashboarLaboral.Controllers
{
    [Authorize(Roles = AccessRoles.Dashboard)]
    public class GraficosController : Controller
    {
        private readonly IGraficosService graficosService;
        private readonly IFechaService fechaService;

        public GraficosController(IGraficosService graficosService, IFechaService fechaService)
        {
            this.graficosService = graficosService;
            this.fechaService = fechaService;
        }

        [HttpGet]
        public async Task<JsonResult> GraficoArea(string empresa, string orden)
        {
            var response = await graficosService.ObtenerAreaChartDatos(fechaService.Hoy);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GraficoHorasExtras(string empresa, string orden)
        {
            DateTime fechaIni = new(fechaService.Hoy.Year, fechaService.Hoy.Month, 1);
            DateTime fechaFin = new(fechaService.Hoy.Year, fechaService.Hoy.Month, DateTime.DaysInMonth(fechaService.Hoy.Year, fechaService.Hoy.Month));

            var response = await graficosService.ObtenerGraficoHorasExtras(fechaIni, fechaFin);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GraficoBar(string empresa, string orden, string rangoHora, string colaborador)
        {
            var model = await graficosService.ObtenerRadialBarDatos(fechaService.Hoy);
            return Json(model);
        }
    }
}
