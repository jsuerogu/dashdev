using System;

namespace DashboarLaboral.Data
{
    public partial class Parametro
    {
        public string Empresa { get; set; }
        public string Parametro1 { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public Guid RowId { get; set; }
        public string Mensaje { get; set; }
    }
}
