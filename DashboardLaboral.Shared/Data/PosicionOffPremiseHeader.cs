using System.Collections.Generic;

namespace DashboarLaboral.Data
{
    public class PosicionOffPremiseHeader
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string VicePresidencia { get; set; }
        public string Departamento { get; set; }
        public string Posicion { get; set; }
        public virtual Empresa EmpresaObject { get; set; }
        public virtual ICollection<PosicionOffPremiseDetails> Details { get; set; }
    }
}
