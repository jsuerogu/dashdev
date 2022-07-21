using System.Collections.Generic;

namespace DashboarLaboral.Models.Tops
{
    public class TopModel
    {
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string SubTitulo2 { get; set; }
        public int Id { get; set; }
        public string IdHeader { get; set; }
        public string IdParent { get; set; }
        public List<ColumnaModel> Columnas { get; set; }
        public List<DataModel> Valores { get; set; }
        public string Class { get; set; }
        public bool  HasLink { get; set; }
    }
}
