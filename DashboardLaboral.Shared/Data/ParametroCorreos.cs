namespace DashboarLaboral.Data
{
    public class ParametroCorreos
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string Empresa { get; set; }
        public string Vicepresidencia { get; set; }
        public string Departamento { get; set; }
        public string Indicadores { get; set; }
        public virtual Empresa EmpresaObject { get; set; }
    }
}
