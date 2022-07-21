using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos
{
    public class PosicionOffPremiseHeaderDto
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string VicePresidencia { get; set; }
        public string Departamento { get; set; }
        public string Posicion { get; set; }
        public List<SelectListItem> Empresas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Vicepresidencias { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departamentos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Posiciones { get; set; } = new List<SelectListItem>();
    }

    public class PosicionOffPremiseHeaderViewDto
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string VicePresidencia { get; set; }
        public string Departamento { get; set; }
        public string Posicion { get; set; }
    }


    public class PosicionOffPremiseHeaderDetailDto
    {
        public int Id { get; set; }
        public string Posicion { get; set; }
        public List<PosicionOffPremiseDetailsDto> Details { get; set; } = new List<PosicionOffPremiseDetailsDto>();
    }

    public class PosicionOffPremiseDetailsDto
    {
        public int Id { get; set; }
        public int IdHeader { get; set; }
        public int CodigoEmpleado { get; set; }
        public string Nombre { get; set; }
        public bool Selected { get; set; }
    }


}
