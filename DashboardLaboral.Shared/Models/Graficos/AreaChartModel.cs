using System;
using System.Collections.Generic;

namespace DashboarLaboral.Models.Graficos
{
    public class AreaChartModel
    {
        public List<DateTime> Fechas { get; set; }
        public List<AreaChartSerieModel> Tardanzas { get; set; }
        public List<AreaChartSerieModel> Inasistencias { get; set; }
    }

    public class AreaChartSerieModel
    {
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
    }
}
