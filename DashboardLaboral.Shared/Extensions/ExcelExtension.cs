using ClosedXML.Excel;
using DashboarLaboral.Models;
using System;
using System.IO;

namespace DashboarLaboral.Extensions
{
    public static class ExcelExtension
    {
        public static IXLWorksheet GenerarEncabezado(this XLWorkbook wb, string contentRoothPath)
        {
            var worksheet = wb.Worksheets.Add("Tabla Datos");
            var imagePath = "wwwroot/new_template/images/logo/logo1.jpg";

            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 15;
            worksheet.Column(3).Width = 20;
            worksheet.Column(4).Width = 20;
            worksheet.Column(5).Width = 20;
            worksheet.Column(6).Width = 20;
            worksheet.Column(7).Width = 20;
            worksheet.Column(8).Width = 20;
            worksheet.Column(9).Width = 20;
            worksheet.Column(10).Width = 20;
            worksheet.Column(11).Width = 20;
            worksheet.Column(12).Width = 20;
            worksheet.Column(13).Width = 20;
            worksheet.Column(14).Width = 20;
            worksheet.Row(1).Height = 50;

            string docPath = Path.Combine(contentRoothPath, imagePath);
            var image = worksheet.AddPicture(docPath);
            image.Name = "Logo";
            worksheet.Range("A2").SetValue("Fecha")
                             .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left)
                             .Font.SetBold(true);

            worksheet.Cell("B2").Value = DateTime.Now.ToString("dd/MM/yyyy");

            return worksheet;
        }

        public static int Titulos(this IXLWorksheet worksheet, DepartamentoModel departamento, int fila)
        {
            worksheet.Range("A" + fila + ":N" + fila)
                .Merge()
                .SetValue($"{departamento.Titulo} - {departamento.Total}")
                .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left)
                .Font.SetFontSize(14)
                .Font.SetBold(true);

            fila += 1;

            worksheet.Range("A" + fila + ":N" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            worksheet.Range("A" + fila + ":N" + fila).Style
                     .Font.SetFontSize(13)
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.White)
                     .Fill.SetBackgroundColor(XLColor.Gray);

            worksheet.Cell("A" + fila).Value = "Indicador";
            worksheet.Cell("B" + fila).Value = "Tipo";
            worksheet.Cell("C" + fila).Value = "Departamento";
            worksheet.Cell("D" + fila).Value = "Nombre";
            worksheet.Cell("F" + fila).Value = "Código";
            worksheet.Cell("E" + fila).Value = "Posición";
            worksheet.Cell("G" + fila).Value = "Horario";
            worksheet.Cell("H" + fila).Value = "Entrada";
            worksheet.Cell("I" + fila).Value = "Salida";
            worksheet.Cell("J" + fila).Value = "Ctd.Horas";
            worksheet.Cell("K" + fila).Value = "Supervisor";
            worksheet.Cell("L" + fila).Value = "Correo supervisor";
            worksheet.Cell("M" + fila).Value = "Teléfono";
            worksheet.Cell("N" + fila).Value = "Correo Empleado";

            return fila;
        }

        private static void Semaforo(this IXLWorksheet worksheet, string indicador, int fila)
        {
            switch (indicador.ToLower())
            {
                case "presentes":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.Green);
                    break;
                case "tardanzas":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.Yellow);
                    break;
                case "inasistencias":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.Red);
                    break;
                case "offpremise":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.Blue);
                    break;
                case "ausenciajust":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.Gray)
                        .Fill.SetBackgroundColor(XLColor.Gray);
                    break;
                case "condriesgo":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.Gray)
                        .Fill.SetBackgroundColor(XLColor.Gray);
                    break;
                case "cuarentena":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.Gray)
                        .Fill.SetBackgroundColor(XLColor.Gray);
                    break;
                case "notocatrabajar":
                    worksheet.Range("A" + fila).Style
                        .Font.SetFontSize(13)
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.Gray)
                        .Fill.SetBackgroundColor(XLColor.Gray);
                    break;
                default:
                    break;
            }
        }

        public static void Data(this IXLWorksheet worksheet, HorarioModel item, int fila)
        {
            worksheet.Semaforo(item.Indicador.Indicador, fila);
            worksheet.Cell("A" + fila).Value = "";
            worksheet.Cell("B" + fila).Value = item.Indicador.Tipo;
            worksheet.Cell("C" + fila).Value = item.Departamento;
            worksheet.Cell("D" + fila).Value = item.Nombre;
            worksheet.Cell("E" + fila).Value = item.Codigo;
            worksheet.Cell("F" + fila).Value = item.Posicion;
            worksheet.Cell("G" + fila).Value = item.Horario;
            worksheet.Cell("H" + fila).Value = item.Entrada;
            worksheet.Cell("I" + fila).Value = item.Salida;
            worksheet.Cell("J" + fila).Value = item.CantHoras;
            worksheet.Cell("K" + fila).Value = item.Nombresupervisor;
            worksheet.Cell("L" + fila).Value = item.Correosupervisor;
            worksheet.Cell("M" + fila).Value = item.Telefono;
            worksheet.Cell("N" + fila).Value = item.Correo;

        }
    }
}
