using DashboardLaboral.Shared.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Models;
using DashboarLaboral.Models.Tops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DashboarLaboral.Controllers
{
    
    public partial class HomeController : Controller
    {
        private readonly IFechaService fechaService;
        private readonly IDataContext dataContext;
        private readonly IIndicadoresService indicadoresService;
        private readonly ITopService topService;
        private readonly IGraficoRender graficoRender;

        public HomeController(IFechaService fechaService, IDataContext dataContext, IIndicadoresService indicadoresService, ITopService topService, IGraficoRender graficoRender)
        {
            this.fechaService = fechaService;
            this.dataContext = dataContext;
            this.indicadoresService = indicadoresService;
            this.topService = topService;
            this.graficoRender = graficoRender;
        }

        [Authorize(Roles = AccessRoles.Dashboard)]
        public async Task<IActionResult> Index(string empresa, string orden, string rangoHora)
        {
            var model = new IndexModel()
            {
                Empresas = dataContext.ObtenerEmpresas().Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Vicepresidencias = dataContext.ObtenerVicepresidencias(empresa).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Departamentos = dataContext.ObtenerDepartamentos(empresa, orden).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                FechaEjecucion = fechaService.FechaEjecucion()
            };

            return View(await Task.FromResult(model));
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> ObtenerVicePresidencia(string empresa)
        {
            StringBuilder model = new StringBuilder();
            dataContext.ObtenerVicepresidencias(empresa)
                .ForEach(option =>
                {
                    model.AppendLine($"<option value='{option.Value}'>{option.Display}</option>");
                });
            return Json(await Task.FromResult(model.ToString()));
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> ObtenerDepartamentos(string empresa, string orden)
        {
            StringBuilder model = new StringBuilder();
            dataContext.ObtenerDepartamentos(empresa, orden)
                .ForEach(option =>
                {
                    model.AppendLine($"<option value='{option.Value}'>{option.Display}</option>");
                });
            return Json(await Task.FromResult(model.ToString()));
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> ObtenerPosiciones(string empresa, string orden, string departamento)
        {
            StringBuilder model = new StringBuilder();
            dataContext.ObtenerPosiciones(empresa, orden, departamento)
                .ForEach(option =>
                {
                    model.AppendLine($"<option value='{option.Value}'>{option.Display}</option>");
                });
            return Json(await Task.FromResult(model.ToString()));
        }

        [Authorize(Roles = AccessRoles.Dashboard)]
        public async Task<PartialViewResult> IndicadoresSimples(string empresa, string orden, string rangoHora, string colaborador)
        {
            var model = await indicadoresService.CargarIndicadoresSimples(fechaService.Hoy);
            return PartialView("_IndicadoresSimples", model);
        }

        [Authorize(Roles = AccessRoles.Dashboard)]
        public async Task<PartialViewResult> IndicadoresExtendidos(string empresa, string orden, string rangoHora, string colaborador)
        {
            var model = await indicadoresService.CargarIndicadoresExtendidos(fechaService.Hoy);
            return PartialView("_IndicadoresExtendidos", model);
        }

        [Authorize(Roles = AccessRoles.Dashboard)]
        public async Task<PartialViewResult> Tops(string empresa, string orden, string rangoHora, string colaborador)
        {
            bool hasLink = User.IsInRole(AccessRoles.Consultas);
            var model = new TopsModel()
            {
                TopModelsAreas = new List<TopModel>
                {
                    await topService.ObtenerTopMayorIncumplimientoDepartamentoAsync(fechaService.Hoy, hasLink),
                    await topService.ObtenerTopMayorHorasExtrasDepartamentoAsync(fechaService.Hoy, hasLink)
                },
                TopModelsEmpleados = new List<TopModel>
                {
                    await topService.ObtenerTopMayorIncumplimientoEmpleadosAsync(fechaService.Hoy, hasLink),
                    await topService.ObtenerTopMayorHorasExtrasEmpleadosAsync(fechaService.Hoy, hasLink)
                }
            };
            return PartialView("_Tops", model);
        }
    }
}
