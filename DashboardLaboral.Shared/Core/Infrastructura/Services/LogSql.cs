using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    public class LogSql : ILogSql
    {
        private readonly insitedb context;
        public LogSql(IConfiguration configuration)
        {
            context = new insitedb(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<insitedb>(),
                        configuration.GetConnectionString("connectionString")
                        ).Options);
        }

        public void Commit()
        {
            if(context.ChangeTracker.HasChanges())
                context.SaveChanges();
        }

        public void Error(string message, Exception exception)
        {

            var logEvent = new EventLog
            {
                Level = "Error",
                Message = message,
                Timestamp = DateTime.Now.FechaZonaHoraria(),
                Exception = exception.Message,
                LogEvent = ""
            };

            context.Add(logEvent);
        }

        public void Information(string message)
        {
            var logEvent = new EventLog
            {
                Level = "Information",
                Message = message,
                Timestamp = DateTime.Now.FechaZonaHoraria(),
                Exception = "",
                LogEvent = ""
            };

            context.Add(logEvent);
        }

        public void Warning(string message)
        {
            var logEvent = new EventLog
            {
                Level = "Warning",
                Message = message,
                Timestamp = DateTime.Now.FechaZonaHoraria(),
                Exception = "",
                LogEvent = ""
            };

            context.Add(logEvent);
        }
    }
}
