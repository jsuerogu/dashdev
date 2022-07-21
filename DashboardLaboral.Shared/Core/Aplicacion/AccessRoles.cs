namespace DashboarLaboral.Core.Aplicacion
{
    public static class AccessRoles
    {
        public const string Dashboard = "Dashboard.View";
        public const string Consultas = "Consultas.View";
        public const string Ausentismo = "Configuracion.Ausentismo";
        public const string Empresas = "Configuracion.Empresas";
        public const string Parametros = "Configuracion.Parametros";
        public const string CorreoAutomatico = "Configuracion.CorreoAutomatico";
        public const string OffPremise = "Configuracion.OffPremise";
        public const string Insite = "Insite.View";
        public const string PosicionesFeriados = "Configuracion.PosicionesFeriados";

        public const string Configuracion = "Configuracion.";
        public const string All = "Dashboard.View,Configuracion.Parametros,Configuracion.Ausentismo,Configuracion.Parametros,Configuracion.Empresas,Configuracion.CorreoAutomatico,Configuracion.OffPremise,Configuracion.PosicionesFeriados";
    }
}
