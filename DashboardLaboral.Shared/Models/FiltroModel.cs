using System;
using System.ComponentModel;

namespace DashboarLaboral.Models
{
    public class FiltroModel
    {
        [DisplayName("Empresa")]
        public string Empresa { get; set; }

        [DisplayName("Vice Presidencia")]
        public string Vicepresidencia { get; set; }

        [DisplayName("Departamento")]
        public string Departamento { get; set; }

        [DisplayName("Tipos")]
        public string Indicador { get; set; }

        [DisplayName("Tipo Colaborador")]
        public string Colaborador { get; set; }

        [DisplayName("Fecha Inicial")]
        public DateTime FechaInicial { get; set; }

        [DisplayName("Fecha Final")]
        public DateTime FechaFinal { get; set; }

        [DisplayName("Rango Fecha")]
        public bool RangoFecha { get; set; }

        [DisplayName("Horario")]
        public bool RangoHora { get; set; }

        public int OrderColumn { get; set; }

        public string OrderDirection { get; set; }

        public string SearchValue { get; set; }

        public override string ToString()
        {
            var fecha = RangoFecha ? $"{FechaInicial.ToShortDateString()} - {FechaFinal.ToShortDateString()}" : FechaInicial.ToLongDateString();
            return $"({fecha})/{Empresa.Trim()}/{Vicepresidencia.Trim()}/{Departamento.Trim()}/{Colaborador.Trim()}/{Indicador}";
        }

    }
}
