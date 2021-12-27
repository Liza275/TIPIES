using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TIPIESProj.DataBase.ViewModels;
using TIPIESProj.DataBase.ViewModels.Report;

namespace TIPIESProj.DataBase.Services.DocGenerator
{
    public class CreatePdfSf
    {
        public static void SaveTable(string nameOfFile, string nameOfDocument, List<TableColumn> columns,
              TableRow[] rows, List<SoldReportProductModel> workers, SoldFullReportModel model)
        {
            if (workers == null || workers.Count == 0)
                throw new Exception("list is null or empty");

            if (columns.FirstOrDefault(column => column.Name == null || column.PropertyName == null) != null)
                throw new Exception("Fill data");

            nameOfDocument = TranslitService.Tr2(nameOfDocument);
            PdfPTable table = CreateTable(columns, rows, workers, model);
            PdfPTable table2 = CreateTable(model);
            FileStream fs = new FileStream(nameOfFile, FileMode.Create);
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.Add(new Paragraph(nameOfDocument));
            document.Add(table);
            document.Add(new Paragraph("ITOGO"));
            document.Add(table2);
            document.Close();
            writer.Close();
            fs.Close();
        }

        private static PdfPTable CreateTable(SoldFullReportModel model)
        {
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

            PdfPTable table = new PdfPTable(3);
            table.AddCell(new PdfPCell(new Phrase(TranslitService.Tr2("Сумма себестоимости"), font)));
            table.AddCell(new PdfPCell(new Phrase(TranslitService.Tr2("Продано по плановой цене"), font)));
            table.AddCell(new PdfPCell(new Phrase(TranslitService.Tr2("Продано всего"), font)));
            table.AddCell(new PdfPCell(new Phrase(model.TotalSoldSum.ToString(), font)));
            table.AddCell(new PdfPCell(new Phrase(model.TotalSoldInPlannedCostPrice.ToString(), font)));
            table.AddCell(new PdfPCell(new Phrase(model.TotalSold.ToString(), font)));

            return table;
        }

        private static PdfPTable CreateTable(List<TableColumn> columns,
            TableRow[] rows, List<SoldReportProductModel> workers, SoldFullReportModel model)
        {
            float[] widths = new float[columns.Count];

            bool widthsExist = columns.FirstOrDefault(rec => rec.Width == 0) == null;

            if (widthsExist)
            {
                int index = 0;
                int sum = 0;
                foreach (var column in columns)
                {
                    widths[index] = column.Width;
                    sum += column.Width;
                    index++;
                }
            }

            //Здесь мы проверяем наличие данных о высоте колонок
            bool heightsExist = rows.FirstOrDefault(rec => rec.Height == 0) == null;
            //if (row != null /*|| rows.Length == 2*/)
            //    heightsExist = false;

            //Если есть ширина, то добавляем параметры
            PdfPTable table = new PdfPTable(4);
            if (widthsExist)
            {
                table.LockedWidth = true;
                table.SetTotalWidth(widths);
            }

            //столбцы
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
            foreach (TableColumn column in columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Name, font));
                if (heightsExist) cell.MinimumHeight = rows[0].Height;
                table.AddCell(cell);
            }

            //ячейки
            foreach (var worker in workers)
            {
                foreach (var column in columns)
                {
                    PropertyInfo propertyInfo = worker.GetType().GetProperty(column.PropertyName);
                    string value = propertyInfo.GetValue(worker) == null ? string.Empty : propertyInfo.GetValue(worker).ToString();
                    PdfPCell cell = new PdfPCell(new Phrase(TranslitService.Tr2(value)));
                    if (heightsExist) cell.MinimumHeight = rows[1].Height;
                    table.AddCell(cell);
                }
            }

            return table;
        }
    }
}
