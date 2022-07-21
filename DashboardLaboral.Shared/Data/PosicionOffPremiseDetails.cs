using System.Collections.Generic;

namespace DashboarLaboral.Data
{
    public class PosicionOffPremiseDetails
    {
        public int Id { get; set; }
        public int IdHeader { get; set; }
        public virtual PosicionOffPremiseHeader Header { get; set; }
        public int CodigoEmpleado { get; set; }
        public bool Selected { get; set; }
    }
}
