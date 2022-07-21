using DashboarLaboral.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DashboarLaboral.Models
{
    public abstract class Indicador
    {
        protected readonly IQueryable<Horario> data;
        protected readonly IQueryable<Horario> dataAyer;
        public Expression<Func<Horario, bool>> Filter { get; protected set; }
        public Indicador(IQueryable<Horario> data, IQueryable<Horario> dataAyer = null)
        {
            this.data = data;
            this.dataAyer = dataAyer;
        }
        public string Nombre { get; set; }
        public abstract IndicadorModel Model();
    }
}
