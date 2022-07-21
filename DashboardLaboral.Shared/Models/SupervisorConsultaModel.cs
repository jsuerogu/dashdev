using System.Collections.Generic;

namespace DashboarLaboral.Models
{
    public class SupervisorConsultaModel
    {
        public string CorreoSupervisor { get; set; }
        public int Total { get; set; }
        public List<DepartamentoModel> Departamentos { get; set; } = new();
    }
}
