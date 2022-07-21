using AutoMapper;
using DashboarLaboral.Core.Aplicacion;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DashboarLaboral.Controllers
{
    [Authorize(Roles = AccessRoles.CorreoAutomatico)]
    public class ParametroCorreosController : Controller
    {
        private readonly IRepositoryParametroCorreos repository;
        private readonly IDataContext dataContext;
        private IMapper mapper;

        public ParametroCorreosController(IRepositoryParametroCorreos repository, IMapper mapper, IDataContext dataContext)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CargarData(ConsultaDataRequestModel model)
        {
            var filtro = JsonConvert.DeserializeObject<FiltroModel>(model.ExtraData);
            Expression<Func<ParametroCorreos, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.Destinatario.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.EmpresaObject.Empresa1.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Departamento.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Vicepresidencia.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Indicadores.ToLower().Contains(filtro.SearchValue.ToLower());

            try
            {
                var response = await model.PaginatedData<ParametroCorreosViewDto, ParametroCorreos>(await repository.GetAllAsync(), mapper, searchPredicate);
                return Json(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(int id)
        {

            ParametroCorreosDto model = null;


            if (id != 0)
            {
                var entity = await repository.FindAsync(id);

                if (entity is null) return NotFound();

                model = mapper.Map<ParametroCorreosDto>(entity);
            }
            else
            {
                model = mapper.Map<ParametroCorreosDto>(new ParametroCorreos());
            }

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ParametroCorreosDto model)
        {
            if (!ModelState.IsValid)
            {
                model.Departamentos = dataContext.ObtenerDepartamentos(model.Empresa, model.Vicepresidencia, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
                model.Empresas = dataContext.ObtenerEmpresas(emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
                model.Vicepresidencias = dataContext.ObtenerVicepresidencias(model.Empresa, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();

                return PartialView("Edit", model);
            }
                
                

            Func<ParametroCorreos, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<ParametroCorreos>(model);

            if (model.Id != 0)
                method = x => repository.UpdateAsync(x);

            await method.Invoke(entity);

            return Json($"message:Se ha actualizado el parámetro correo automático con exito ...");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var entityHeader = await repository.FindAsync(id);
            if (entityHeader is null) return NotFound();

            await repository.DeleteAsync(entityHeader);
            return RedirectToAction("Index", "ParametroCorreos");
        }
    }
}
