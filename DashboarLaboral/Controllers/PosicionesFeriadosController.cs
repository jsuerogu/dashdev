using AutoMapper;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DashboarLaboral.Controllers
{
    //[Authorize(Roles = AccessRoles.PosicionesFeriados)]
    public class PosicionesFeriadosController : Controller
    {
        private readonly IRepositoryPosicionesFeriados repository;
        private readonly IDataContext dataContext;
        private IMapper mapper;

        public PosicionesFeriadosController(IRepositoryPosicionesFeriados repository, IMapper mapper, IDataContext dataContext)
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
            Expression<Func<PosicionesFeriados, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.Posicion.ToLower().Contains(filtro.SearchValue.ToLower());

            try
            {
                var response = await model.PaginatedData<PosicionesFeriadosDto, PosicionesFeriados>(await repository.GetAllAsync(), mapper, searchPredicate);
                return Json(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(int id)
        {

            PosicionesFeriadosDto model = null;


            if (id != 0)
            {
                var entity = await repository.FindAsync(id);

                if (entity is null) return NotFound();

                model = mapper.Map<PosicionesFeriadosDto>(entity);
            }
            else
            {
                model = mapper.Map<PosicionesFeriadosDto>(new PosicionesFeriados());
            }

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PosicionesFeriadosDto model)
        {
            if (!ModelState.IsValid)
                return PartialView("Edit", model);

            Func<PosicionesFeriados, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<PosicionesFeriados>(model);

            if (model.Id != 0)
                method = x => repository.UpdateAsync(x);

            await method.Invoke(entity);

            return Json($"message:Se ha actualizado la posición feriado con exito ...");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var entityHeader = await repository.FindAsync(id);
            if (entityHeader is null) return NotFound();

            await repository.DeleteAsync(entityHeader);
            return RedirectToAction("Index", "PosicionesFeriados");
        }

    }
}
