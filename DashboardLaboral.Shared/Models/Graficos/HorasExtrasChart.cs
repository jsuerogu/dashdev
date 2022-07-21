using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashboarLaboral.Models.Graficos
{
    [Keyless]
    public class HorasExtrasChart
    {
        [Column(TypeName = "numeric(18,2)")]
        public decimal HorasExtras { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal HorasExtrasPlanificadas { get; set; }
        public string MesAno { get; set; }
    }
}
