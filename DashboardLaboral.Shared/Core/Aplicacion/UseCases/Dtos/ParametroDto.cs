using System;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos
{
    public class ParametroDto
    {
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public Guid RowId { get; set; }
        public string Mensaje { get; set; }
    }
}
