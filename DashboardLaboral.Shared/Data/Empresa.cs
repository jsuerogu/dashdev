#nullable disable

using System;
using System.Collections.Generic;

namespace DashboarLaboral.Data
{
    public partial class Empresa
    {
        public string CodigoEmpresa { get; set; }
        public string Empresa1 { get; set; }
        public string Color { get; set; }
        public Guid RowId { get; set; }
        public ICollection<ParametroCorreos> ParametroCorreos { get; set; }
        public ICollection<PosicionOffPremiseHeader> PosicionOffPremises { get; set; }
    }
}
