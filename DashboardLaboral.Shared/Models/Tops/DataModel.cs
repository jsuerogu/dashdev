using System.Collections.Generic;

namespace DashboarLaboral.Models.Tops
{
    public class DataModel
    {
        public List<string> ExtraData  { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorMensual { get; set; }
        public decimal ValorHoy { get; set; }
        public string Formato { get; set; } = "n0";
        public string DatosClase { get; set; }
    }
}
