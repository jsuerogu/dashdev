using DashboardLaboral.Shared.Models;

namespace DashboarLaboral.Models
{
    public class HorarioModel
    {
        //public string Indicador { get; set; }
        //public string Tipo { get; set; }
        public IndicadorDetailModel Indicador { get; set; }
        public string Departamento { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Posicion { get; set; }
        public string Horario { get; set; }
        public string Entrada { get; set; }
        public string Salida { get; set; }
        public string CantHoras { get; set; }
        public string Horasextras { get; set; }
        public string Fecha { get; set; }
        public string Nombresupervisor { get; set; }
        public string Correosupervisor { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
