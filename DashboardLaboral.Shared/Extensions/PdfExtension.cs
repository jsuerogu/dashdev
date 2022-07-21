using DashboarLaboral.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace DashboarLaboral.Extensions
{
    public static class PdfExtension
    {
        public static void GenerarEncabezado(this Document document, string contentRoothPath)
        {
            var imagePath = "wwwroot/new_template/images/logo/logo1.jpg";
            var rutaLogo = Path.Combine(contentRoothPath, imagePath);
            
            var logo = Image.GetInstance(rutaLogo);

            logo.ScaleAbsoluteWidth(80);
            logo.ScaleAbsoluteHeight(70);

            document.Add(logo);
        }

        public static PdfPTable Titulos(this Document document, DepartamentoModel departamento, int fila)
        {
            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 14f, Font.BOLD);
            title.Add($"{departamento.Titulo} - {departamento.Total}");

            document.Add(title);
            string[] Column = { "Indicador", "Tipo", "Nombre", "Código", "Posición", "Horario", "Entrada", "Salida", "Ctd.Horas" };
            string FontLetras = "Calibri";
            int sizeTex = 11;
            PdfPTable table = new PdfPTable(9);

            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");
            table.AddCell("");


            for (int i = 0; i < Column.Length; i++)
            {
                var TextFont = FontFactory.GetFont(FontLetras, sizeTex, new BaseColor(255, 255, 255));
                PdfPCell cell = new PdfPCell(new Phrase(new Paragraph(Column[i].ToUpper(), TextFont)));
                cell.BackgroundColor = new BaseColor(0, 76, 59);
                table.AddCell(cell);
            }

            return table;
        }

        public static void Data(this PdfPTable table, HorarioModel item, int fila)
        {
            string fontLetras = "Calibri";
            int sizeTex = 10;
            var textFont = FontFactory.GetFont(fontLetras, sizeTex, new BaseColor(0, 0, 0));
            var cell = new PdfPCell(new Phrase(new Paragraph("", textFont)));
            string nombre = item.Nombre.ToString().Substring(0, 1).ToUpper() + item.Nombre.ToString().Substring(1).ToLower();

            table.Semaforo(item.Indicador.Indicador, cell);
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Indicador.Tipo, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(nombre, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Codigo, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Posicion, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Horario, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Entrada, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.Salida, textFont)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Paragraph(item.CantHoras, textFont)));
            table.AddCell(cell);

        }

        private static void Semaforo(this PdfPTable table, string indicador, PdfPCell cell)
        {
            
            switch (indicador.ToLower())
            {
                case "presentes":
                    cell.BackgroundColor = new BaseColor(0, 240, 0);
                    break;
                case "tardanzas":
                    cell.BackgroundColor = new BaseColor(255, 255, 0);
                    break;
                case "inasistencias":
                    cell.BackgroundColor = new BaseColor(255, 0, 0);
                    break;
                case "offpremise":
                    cell.BackgroundColor = new BaseColor(0, 166, 214);
                    break;
                case "ausenciajust":
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    break;
                case "condriesgo":
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    break;
                case "cuarentena":
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    break;
                case "notocatrabajar":
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    break;
                default:
                    break;
            }
        }
    }
}
