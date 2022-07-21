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
    [Authorize(Roles = AccessRoles.Empresas)]
    public class EmpresasController : Controller
    {
        private readonly IRepositoryEmpresa repository;
        private IMapper mapper;

        public EmpresasController(IRepositoryEmpresa repository, IMapper mapper)
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
            Expression<Func<Empresa, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(filtro?.SearchValue))
                searchPredicate = e => e.CodigoEmpresa.ToLower().Contains(filtro.SearchValue.ToLower())
                || e.Empresa1.ToLower().Contains(filtro.SearchValue.ToLower());

            try
            {
                var response = await model.PaginatedData<EmpresaDto, Empresa>(await repository.GetAllAsync(), mapper, searchPredicate);
                return Json(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(string id = null)
        {
            var model = new EmpresaDto();


            if (!string.IsNullOrEmpty(id))
            {
                var entity = await repository.FindAsync(id);

                if (entity is null) return NotFound();

                model = mapper.Map<EmpresaDto>(entity);
            }

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmpresaDto model)
        {
            if (!ModelState.IsValid)
                return PartialView("Edit", model);
                

            Func<Empresa, Task<int>> method = x => repository.AddAsync(x);

            var entity = mapper.Map<Empresa>(model);

            if (model.RowId != Guid.Empty)
                method = x => repository.UpdateAsync(x);
            else
                entity.RowId = Guid.NewGuid();

            await method.Invoke(entity);

            return Json($"message:Se ha actualizado la empresa {model.Nombre} con exito ...");
        }


    }
}
