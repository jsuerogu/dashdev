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
    [Authorize(Roles = AccessRoles.OffPremise)]
    public class PosicionOffPremiseController : Controller
    {
        private readonly IRepositoryPosicionOffPremise repository;
        private readonly IDataContext dataContext;
        private IMapper mapper;

        public PosicionOffPremiseController(IRepositoryPosicionOffPremise repository, IMapper mapper, IDataContext dataContext)
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
            Expression<Func<PosicionOffPremiseHeader, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.EmpresaObject.Empresa1.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.VicePresidencia.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Departamento.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Posicion.ToLower().Contains(filtro.SearchValue.ToLower());

            try
            {
                var response = await model.PaginatedData<PosicionOffPremiseHeaderViewDto, PosicionOffPremiseHeader>(await repository.GetAllAsync(), mapper, searchPredicate);
                return Json(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            PosicionOffPremiseHeaderDto model = null;


            if (id != 0)
            {
                var entity = await repository.FindAsync(id);

                if (entity is null) return NotFound();

                model = mapper.Map<PosicionOffPremiseHeaderDto>(entity);
            }
            else
            {
                model = mapper.Map<PosicionOffPremiseHeaderDto>(new PosicionOffPremiseHeader());
            }
            return PartialView(model);
        }

        public async Task<IActionResult> EditDetails(int id)
        {

            var entityHeader = await repository.GetWithIncludeAsync(id);
            if (entityHeader is null) return NotFound();

            var model = mapper.Map<PosicionOffPremiseHeaderDetailDto>(entityHeader);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PosicionOffPremiseHeaderDto model)
        {
            if (!ModelState.IsValid)
            {
                model.Posiciones = dataContext.ObtenerPosiciones(model.Empresa, model.VicePresidencia, model.Departamento, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
                model.Departamentos = dataContext.ObtenerDepartamentos(model.Empresa, model.VicePresidencia, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
                model.Empresas = dataContext.ObtenerEmpresas(emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();
                model.Vicepresidencias = dataContext.ObtenerVicepresidencias(model.Empresa, emptyLabel: "Seleccionar").Select(e => new SelectListItem(e.Display, e.Value)).ToList();

                return PartialView("Edit", model);
            }


            Func<PosicionOffPremiseHeader, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<PosicionOffPremiseHeader>(model);

            if (model.Id !=  0)
                method = x => repository.UpdateAsync(x);

            await method.Invoke(entity);

            return Json($"message:Se ha actualizado la posición {model.Posicion} con exito ...");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var entityHeader = await repository.GetWithIncludeAsync(id);
            if (entityHeader is null) return NotFound();

            await repository.DeleteAsync(entityHeader);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditDetails(PosicionOffPremiseHeaderDetailDto model)
        {

            Func<PosicionOffPremiseDetails, Task<int>> method = null;

            try
            {
                var editRows = model.Details?.Where(e => e.Selected || e.Id != 0);
                foreach (var item in editRows)
                {
                    var entity = mapper.Map<PosicionOffPremiseDetails>(item);
                    if (item.Id == 0)
                    {
                        method = x => repository.AddDetailsAsync(x);
                    }
                    else
                    {
                        method = x => repository.UpdateDetailsAsync(x);
                    }
                    await method.Invoke(entity);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return Json($"message:Se ha actualizado la posición {model.Posicion} con exito ...");
        }


    }
}
