using System.Collections.Generic;

namespace DashboardLaboral.Shared.Models.Graficos
{

    public class RendeRequest
    {
        public int width { get; set; }
        public int height { get; set; }
        public Config config { get; set; }
    }

    public class Config
    {
        public Legend legend { get; set; }
        public Fill fill { get; set; }
        public List<string> colors { get; set; } = new List<string>();
        public List<string> labels { get; set; } = new List<string>();
        public List<object> series { get; set; } = new List<object>();
        public Chart chart { get; set; }
        public Plotoptions plotOptions { get; set; }
        public Xaxis xaxis { get; set; }
    }

    public class Legend
    {
        public string position { get; set; }
    }

    public class Fill
    {
        public string type { get; set; }
        public int opacity { get; set; }
    }

    public class Chart
    {
        public string type { get; set; }
        public int height { get; set; }
    }

    public class Plotoptions
    {
        public Bar bar { get; set; }
    }

    public class Bar
    {
        public bool horizontal { get; set; }
        public Datalabels dataLabels { get; set; }
    }

    public class Datalabels
    {
        public string position { get; set; }
    }

    public class Xaxis
    {
        public List<string> categories { get; set; } = new List<string>();
    }

    public class Series
    {
        public string name { get; set; }
        public object[] data { get; set; }
    }

}
