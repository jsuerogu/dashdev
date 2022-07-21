using DashboarLaboral.Data;
using System.Collections.Generic;
using System.Linq;

namespace DashboarLaboral.Models
{

    public record ParametroCorreoVicePresidenciaModel
    {
        public string Vicepresidencia { get; set; }
        public List<string> Departamentos { get; set; }

        public ParametroCorreoVicePresidenciaModel(string vicepresidencia, List<ParametroCorreos> list)
        {
            Vicepresidencia = vicepresidencia;
            Departamentos = list.Select(p => p.Departamento).ToList();
        }
    }

    public class ParametroCorreoEmpresaModel
    {
        public string Empresa { get; set; }
        public List<ParametroCorreoVicePresidenciaModel> VicePresidencias { get; set; }
        public ParametroCorreoEmpresaModel(string empresa, List<ParametroCorreos> list)
        {
            Empresa = empresa;
            VicePresidencias = list.GroupBy(x => x.Vicepresidencia).Select(g => new ParametroCorreoVicePresidenciaModel(g.Key, g.ToList())).ToList();
        }


    }
    public class ParametroCorreoModel
    {
        public string Destinatario { get; set; }
        public List<ParametroCorreoEmpresaModel> Empresas { get; set; }
        public string Departamento { get; set; }
        public string Indicadores { get; set; }

    }
}
