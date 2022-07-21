using System;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos
{
    public class EmpresaDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public Guid RowId { get; set; }
    }
}
