using Microsoft.AspNetCore.Http;
using System;

namespace DashboarLaboral.Extensions
{
    public static class HttpExtension
    {
        internal static string ObtenerEmpresa(this IHttpContextAccessor httpContext)
        {
            var empresa = "null";
            if (httpContext.HttpContext != null)
            {
                empresa = httpContext.HttpContext.Request.Query.ContainsKey("empresa")
                   ? httpContext.HttpContext.Request.Query["empresa"].ToString()
                   : "";
            }
           
            return empresa.ToLower() != "null" ? empresa : "";
        }
        internal static string ObtenerVicePresidencia(this IHttpContextAccessor httpContext)
        {
            var vicePresidencia = "null";

            if (httpContext.HttpContext != null)
            {
                vicePresidencia = httpContext.HttpContext.Request.Query.ContainsKey("orden")
                    ? httpContext.HttpContext.Request.Query["orden"].ToString()
                    : "";
            }

            return vicePresidencia.ToLower() != "null" ? vicePresidencia : "";
        }

        internal static bool ObtenerRangoHora(this IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext != null)
                return httpContext.HttpContext.Request.Query.ContainsKey("rangoHora")
                && Convert.ToBoolean(httpContext.HttpContext.Request.Query["rangoHora"].ToString());
            else
                return false;
        }

        internal static int ObtenerColaborador(this IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext != null)
            {
                if (!httpContext.HttpContext.Request.Query.ContainsKey("colaborador"))
                    return -1;
                if (string.IsNullOrEmpty(httpContext.HttpContext.Request.Query["colaborador"]))
                    return -1;

                return Convert.ToInt32(httpContext.HttpContext.Request.Query["colaborador"].ToString());
            }
            else
                return -1;
        }

    }
}
