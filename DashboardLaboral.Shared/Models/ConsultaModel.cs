using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DashboarLaboral.Models
{
    public class ConsultaModel
    {
        public List<SelectListItem> Empresas { get; set; }
        public List<SelectListItem> Vicepresidencias { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Indicadores { get; set; }
        public List<SelectListItem> ColaboradorTipo => new()
        {
            new SelectListItem { Text = "Todos", Value = "" },
            new SelectListItem { Text = "Administrativo", Value = "1" },
            new SelectListItem { Text = "Operativo", Value = "0" }
        };
        public FiltroModel Filtro { get; set; }
        public DepartamentoConsultaModel Data { get; set; }
    }
}
