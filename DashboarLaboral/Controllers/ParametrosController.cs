using AutoMapper;
using DashboarLaboral.Core.Aplicacion;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.UseCases.Dtos;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DashboarLaboral.Controllers
{
    [Authorize(Roles = AccessRoles.Parametros)]
    public class ParametrosController : Controller
    {
        private readonly IRepositoryParametro repository;
        private IMapper mapper;

        public ParametrosController(IRepositoryParametro repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CargarData(ConsultaDataRequestModel model)
        {
            var filtro = JsonConvert.DeserializeObject<FiltroModel>(model.ExtraData);
            Expression<Func<Parametro, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.Parametro1.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Valor.ToLower().Contains(filtro.SearchValue.ToLower());


            var response = await model.PaginatedData<ParametroDto, Parametro>(await repository.GetAllAsync(), mapper, searchPredicate);

            return Json(response);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new ParametroDto();

            var entity = await repository.FindAsync(id);

            if (entity is null) return NotFound();

            model = mapper.Map<ParametroDto>(entity);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParametroDto model)
        {
            if (!ModelState.IsValid)
                return PartialView("Edit", model);


            Func<Parametro, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<Parametro>(model);

            if (model.RowId != Guid.Empty)
                method = x => repository.UpdateAsync(x);
            else
                entity.RowId = Guid.NewGuid();
            try
            {
                await method.Invoke(entity);
            }
            catch (Exception)
            {

                throw;
            }

            var message = string.IsNullOrWhiteSpace(model.Mensaje) ?
                $"message:Se ha actualizado el parámetro {model.Nombre} con exito ..."
                : $"message:{model.Mensaje}";
            return Json(message);
        }
    }
}
