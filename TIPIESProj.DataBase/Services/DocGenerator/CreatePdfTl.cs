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

namespace TIPIESProj.DataBase.Services.DocGenerator
{
    public class CreatePdfTl
    {
        public static void SaveTable(string nameOfFile, string nameOfDocument, List<TableColumn> columns,
              TableRow[] rows, List<TransactionLogViewModel> workers)
        {
            if (workers == null || workers.Count == 0)
                throw new Exception("list is null or empty");

            if (columns.FirstOrDefault(column => column.Name == null || column.PropertyName == null) != null)
                throw new Exception("Fill data");

            nameOfDocument = TranslitService.Tr2(nameOfDocument);
            PdfPTable table = CreateTable(columns, rows, workers);
            FileStream fs = new FileStream(nameOfFile, FileMode.Create);
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.Add(new Paragraph(nameOfDocument));
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }

        private static PdfPTable CreateTable(List<TableColumn> columns,
            TableRow[] rows, List<TransactionLogViewModel> workers)
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
            PdfPTable table = new PdfPTable(7);
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

    public class TableRow
    {
        public int Height { get; set; }
    }

    public class TableColumn
    {
        public int Width { set; get; }
        public string PropertyName { get; set; }
        private string _name;
        public string Name { set => _name = value; get => TranslitService.Tr2(_name); }
    }

    public class TranslitService
    {
        public static string Tr2(string s)
        {
            StringBuilder ret = new StringBuilder();
            string[] rus = { ",",".","-","1","2","3","4","5","6","7","8","9","0"," ","А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й",
          "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц",
          "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] eng = { ",",".","-","1","2","3","4","5","6","7","8","9","0"," ","A", "B", "V", "G", "D", "E", "E", "ZH", "Z", "I", "Y",
          "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "KH", "TS",
          "CH", "SH", "SHCH", null, "Y", null, "E", "YU", "YA" };

            for (int j = 0; j < s.Length; j++)
                for (int i = 0; i < rus.Length; i++)
                    if (s.Substring(j, 1).ToLower() == rus[i].ToLower()) ret.Append(eng[i]);
            return ret.ToString();
        }

    }
}
