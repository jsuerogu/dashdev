using System;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface ILogSql
    {
        void Information(string message);
        void Warning(string message);
        void Error(string message, Exception exception);
        void Commit();
    }
}
