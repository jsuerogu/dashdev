using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos
{
    public class ParametroCorreosDto
    {
        public int Id { get; set; }
        public string Destinatario { get; set; } = "";
        public string Empresa { get; set; }
        public string Vicepresidencia { get; set; }
        public string Departamento { get; set; }
        public IndicadoresDto[] Indicadores { get; set; } = new IndicadoresDto[0];
        public List<SelectListItem> Empresas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Vicepresidencias { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departamentos { get; set; } = new List<SelectListItem>();
    }
}
