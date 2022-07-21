using System;

namespace DashboarLaboral.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IndicadorDataAttribute : Attribute
    {
        public string Nombre { get; private set; }
        public string Titulo { get; private set; }
        public string Color { get; set; }
        public string[] Relacionados { get; set; }
        public Type DataType { get; set; }
        public int Prioridad { get; set; }

        public IndicadorDataAttribute(string nombre, string titulo, int prioridad = 0, params string[] relacionados)
        {
            Nombre = nombre;
            Titulo = titulo;
            Prioridad = prioridad;
            Relacionados = relacionados;
        }
    }
}
