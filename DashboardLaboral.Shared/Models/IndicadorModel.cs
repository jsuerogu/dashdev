namespace DashboarLaboral.Models
{
    public class IndicadorModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Tooltip { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAuxiliar { get; set; }
        public string Descripcion { get; set; }
        public IndicadorTipo Tipo { get; set; }
        public string Clase { get; set; }
        public string ClaseAuxiliar { get; set; }
        public string Url { get; set; }
        public string DatosClase { get; set; }
        public string FormatValor { get; set; } = "n0";
        public string FormatValorAuxiliar { get; set; } = "n0";
        public string ExtendValor { get; set; } = "";
        public string ExtendValorAuxiliar { get; set; } = "";
    }

    public enum IndicadorTipo
    {
        Simple,
        Extendido
    }
}
