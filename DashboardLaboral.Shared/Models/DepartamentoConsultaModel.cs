using System.Collections.Generic;

namespace DashboarLaboral.Models
{
    public class DepartamentoConsultaModel
    {
        public int Total { get; set; }
        public List<DepartamentoModel> Departamentos { get; set; } = new();
    }

    public class DepartamentoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Total { get; set; }

        public List<HorarioModel> Data { get; set; } = new List<HorarioModel> ();
    }
}
