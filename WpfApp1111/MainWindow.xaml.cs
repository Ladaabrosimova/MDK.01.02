using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp1111
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Payments1Entities1 _context = new Payments1Entities1();
        public MainWindow()
        {
            InitializeComponent();
            ChartPayments.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };
            ChartPayments.Series.Add(currentSeries);

            ComboUsers.ItemsSource = _context.User.ToList();
            ComboCharTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUsers.SelectedItem is User currentUser &&
                    ComboCharTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartPayments.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categorieslist = _context.Category.ToList();
                foreach (var category in categorieslist)
                {
                    currentSeries.Points.AddXY(category.Name,
                        _context.Payment.ToList().Where(p => p.User == currentUser
                        && p.Category == category).Sum(propa => propa.Price * propa.Num));
                }
            }

        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = _context.User.ToList().OrderBy(p => p.FIO).ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allUsers.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;

            for (int i = 0; i < allUsers.Count; i++) 
            {
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allUsers[i].FIO;

                worksheet.Cells[1][startRowIndex] = "Дата платежа";
                worksheet.Cells[2][startRowIndex] = "Название";
                worksheet.Cells[3][startRowIndex] = "Стоимость";
                worksheet.Cells[4][startRowIndex] = "Количество";
                worksheet.Cells[5][startRowIndex] = "Сумма";

                startRowIndex++;

                var usersCategories = allUsers[i].Payment.OrderBy(p => p.Date).GroupBy(p => p.Category).OrderBy(p => p.Key.Name);

                foreach (var groupCategory in usersCategories) 
                {
                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[startRowIndex]];
                    headerRange.Merge();
                    headerRange.Value = groupCategory.Key.Name;
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerRange.Font.Italic = true;

                    startRowIndex++;

                    foreach (var payment in groupCategory)
                    {

                        worksheet.Cells[1][startRowIndex] = payment.Date.Value.ToString("dd.MM.yyyy  HH:mm");
                        worksheet.Cells[2][startRowIndex] = payment.Name;
                        worksheet.Cells[3][startRowIndex] = payment.Price;
                        worksheet.Cells[4][startRowIndex] = payment.Num;

                        worksheet.Cells[5][startRowIndex].Formula = $"=C{startRowIndex}*D{startRowIndex}";

                        worksheet.Cells[3][startRowIndex].NumberFormat =
                            worksheet.Cells[3][startRowIndex].NumberFormat = "#,###.00";

                        startRowIndex++;
                    }

                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "ИТОГО:";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    worksheet.Cells[5][startRowIndex].Formula = $"=SUM(E{startRowIndex - groupCategory.Count()}:" +
                        $"E{startRowIndex - 1})";

                    sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].Font.Bold = true;
                    worksheet.Cells[5][startRowIndex].NumberFormat = "#,###.00";

                    startRowIndex++;

                    Excel.Range randeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                    randeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    randeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    randeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    randeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    randeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    randeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    worksheet.Columns.AutoFit();
                }
                application.Visible = true;
            }
        }

       private void BtnExportToWord_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = _context.User.ToList();
            var allCategories = _context.Category.ToList();
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            foreach (var user in allUsers)
            {
                Word.Paragraph userParagrapth = document.Paragraphs.Add();
                Word.Range userRange = userParagrapth.Range;
                userRange.Text = user.FIO;
                userParagrapth.set_Style("Title");
                userRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentsTable = document.Tables.Add(tableRange, allCategories.Count() + 1, 3);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                     = Word.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Иконка";
                cellRange = paymentsTable.Cell(1, 2).Range;
                cellRange.Text = "Категория";
                cellRange = paymentsTable.Cell(1, 3).Range;
                cellRange.Text = "Сумма расходов";

                paymentsTable.Rows[1].Range.Bold = 1;
                paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < allCategories.Count; i++)
                {
                    var currentCategory = allCategories[i];

                    cellRange = paymentsTable.Cell(1 + 2, 1).Range;
                    Word.InlineShape imageShape = cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory
                        + "..\\..\\" + currentCategory.Icon);
                    imageShape.Width = imageShape.Height = 40;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = paymentsTable.Cell(i + 2, 2).Range;
                    cellRange.Text = currentCategory.Name;

                    cellRange = paymentsTable.Cell(i + 2, 3).Range;
                    cellRange.Text = user.Payment.ToList()
                        .Where(p => p.Category == currentCategory).Sum(p => p.Num * p.Price).Value.ToString("N2") + " руб. ";


                }


                Payment maxPayment = user.Payment
                    .OrderByDescending(p => p.Price * p.Num).FirstOrDefault();
                if (maxPayment != null)
                {
                    Word.Paragraph maxPaymentParagraph = document.Paragraphs.Add();
                    Word.Range maxPaymentRange = maxPaymentParagraph.Range;
                    maxPaymentRange.Text = $"Самый дорогой платеж - {maxPayment.Name} за {(maxPayment.Price * maxPayment.Num).Value.ToString("N2")} " +
                        $"руб. от {maxPayment.Date.Value.ToString("dd.MM.yyyy HH:mm")}";
                    maxPaymentParagraph.set_Style("Intence Quote");
                    maxPaymentRange.Font.Color = Word.WdColor.wdColorDarkRed;
                    maxPaymentRange.InsertParagraphAfter();
                }

                Payment minPayment = user.Payment
                    .OrderByDescending(p => p.Price * p.Num).FirstOrDefault();
                if (minPayment != null)
                {
                    Word.Paragraph minPaymentParagraph = document.Paragraphs.Add();
                    Word.Range minPaymentRange = minPaymentParagraph.Range;
                    minPaymentRange.Text = $"Самый дешевый платеж - {minPayment.Name} за {(minPayment.Price * minPayment.Num).Value.ToString("N2")} " +
                        $"руб. от {minPayment.Date.Value.ToString("dd.MM.yyyy HH:mm")}";
                    minPaymentParagraph.set_Style("Intence Quote");
                    minPaymentRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                    minPaymentRange.InsertParagraphAfter();
                }
                if (user != allUsers.LastOrDefault())
                {
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                }
                application.Visible = true;

                document.SaveAs2(@"D:\Test.docx");
                document.SaveAs2(@"D:\Test.pdf", Word.WdExportFormat.wdExportFormatPDF);
            }
       }
    }
}
