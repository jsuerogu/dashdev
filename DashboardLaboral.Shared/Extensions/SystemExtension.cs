using System.Globalization;
using System.IO;

namespace System
{
    public static class SystemExtension
    {
        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ToStringSeparadorComa(this decimal value, string format = "n0")
        {
            return value.ToString(format, CultureInfo.CreateSpecificCulture("en-us"));
        }

        public static DateTime DateOfDateTimeNullable(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return default;

            return dateTime.Value.Date;
        }

        public static DateTime DateOfDateTime(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime Ayer(this DateTime fecha)
        {
            return fecha.AddDays(-1);
        }

    }
}
