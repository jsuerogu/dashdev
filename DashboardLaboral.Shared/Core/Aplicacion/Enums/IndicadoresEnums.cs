namespace DashboardLaboral.Shared.Core.Aplicacion.Enums
{
    public static class IndicadoresEnum
    {
        #region Indicadores Evaluación
        public const string NoTocaTrabajar = "notocatrabajar";
        public const string AusenciaJustificada = "ausenciajust";
        public const string Inasistencias = "inasistencias";
        public const string OffPremise = "offpremise";
        public const string Presentes = "presentes";
        public const string Tardanzas = "tardanzas";
        #endregion

        #region Indicadores Auxiliares
        //public const string NoLaboran = "nolaboranhoy";
        public const string Cuarentena = "cuarentena";
        public const string CondicionRiesgo = "condicionriesgo";
        public const string OnPremise = "onpremise";
        #endregion


        public const string TotalEmpleados = "totalempl";
        public const string Laboran = "laboran";
        public const string NoLaboran = "nolaboran";
        public const string IncumplimientoHorario = "incumplimiento";
        public const string AsistenciaNoJustificada = "asistencianojustificada";
        public const string HorasExtras = "horasextras";
    }
}
