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
    [Authorize(Roles = AccessRoles.Ausentismo)]
    public class AusentismoController : Controller
    {
        private readonly IRepositoryAusentismo repository;
        private IMapper mapper;

        public AusentismoController(IRepositoryAusentismo repository, IMapper mapper)
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
            Expression<Func<Ausentismo, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.Aucod.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Audes.ToLower().Contains(filtro.SearchValue.ToLower());

            try
            {
                var response = await model.PaginatedData<AusentismoDto, Ausentismo>(await repository.GetAllAsync(), mapper, searchPredicate);
                return Json(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(string id = null)
        {
            var model = new AusentismoDto();


            if (!string.IsNullOrEmpty(id))
            {
                var entity = await repository.FindAsync(id);

                if (entity is null) return NotFound();

                model = mapper.Map<AusentismoDto>(entity);
            }

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AusentismoDto model)
        {
            if (!ModelState.IsValid)
                return PartialView("Edit", model);
                

            Func<Ausentismo, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<Ausentismo>(model);

            if (!string.IsNullOrWhiteSpace(model.Id))
                method = x => repository.UpdateAsync(x);

            await method.Invoke(entity);

            return Json($"message:Se ha actualizado esl ausentismo {model.Audes} con exito ...");
        }


    }
}
