using System.Collections.Generic;

namespace DashboarLaboral.Models.Tops
{
    public class TopsModel
    {
        public List<TopModel> TopModelsAreas { get; set; } = new();
        public List<TopModel> TopModelsEmpleados { get; set; } = new();

    }
}
