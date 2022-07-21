using System;

#nullable disable

namespace DashboarLaboral.Data
{
    public partial class Horario
    {
        public string Empresa { get; set; }
        public string Clave { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? Corrida { get; set; }
        public int? Codigoempleado { get; set; }
        public string Nombrecompleto { get; set; }
        public string Posicion { get; set; }
        public string Departamento { get; set; }
        public string Vicepresidencia { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Aucod { get; set; }
        public DateTime? Fechaini { get; set; }
        public DateTime? Fechafin { get; set; }
        public int? Codigosupervisor { get; set; }
        public string Nombresupervisor { get; set; }
        public string Correosupervisor { get; set; }
        public string Posicionsupervisor { get; set; }
        public string Telefonosupervisor { get; set; }
        public string Codigohorario { get; set; }
        public DateTime? Horaini { get; set; }
        public DateTime? Horafin { get; set; }
        public DateTime? RealHoraIni { get; set; }
        public DateTime? Poncheentrada { get; set; }
        public DateTime? Ponchesalida { get; set; }
        public int? Trabajahoy { get; set; }
        public int? Administrativo { get; set; }
        public decimal Horasextras { get; set; }
        public decimal HorasextrasPla { get; set; }
        public decimal? Horasdescontadas { get; set; }
        public decimal? Canthoraspermiso { get; set; }
        public string Tipo2 { get; set; }
        //public string Tipo => Tipo2.ConvertindicadorToTipo(Tipo2);

        public int Prioridad { get; set; }
        public decimal CANTHoras { get; set; }
        public decimal? HorasFuera { get; set; }
        public bool OffPremise { get; set; }

        public virtual Ausentismo Ausentismo { get; set; }
    }
}
