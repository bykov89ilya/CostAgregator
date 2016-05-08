using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CostAgregator
{
    public partial class AgregateForm : Form
    {
        public AgregateForm()
        {
            InitializeComponent();
        }

        private void OpenTinkoffFileButton_Click(object sender, EventArgs e)
        {
            listViewTinkoff.Items.Clear();

            Stream streamTinkoff = null;
            OpenFileDialog openFileDialogTinkoff = new OpenFileDialog();

            openFileDialogTinkoff.InitialDirectory = "c:\\";
            openFileDialogTinkoff.Filter = "csv files (*.csv)|*.csv";
            openFileDialogTinkoff.FilterIndex = 1;
            openFileDialogTinkoff.RestoreDirectory = true;
            openFileDialogTinkoff.Multiselect = true;

            if (openFileDialogTinkoff.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((streamTinkoff = openFileDialogTinkoff.OpenFile()) != null)
                    {
                        using (streamTinkoff)
                        {
                            foreach (var fileName in openFileDialogTinkoff.FileNames)
                            {
                                listViewTinkoff.Items.Add(new ListViewItem(fileName));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не могу прочитать файл Тинькофф: " + ex.Message);
                }
            }
        }

        private void openCashFileButton_Click(object sender, EventArgs e)
        {
            listViewCash.Items.Clear();

            Stream streamCash = null;
            OpenFileDialog openFileDialogCash = new OpenFileDialog();

            openFileDialogCash.InitialDirectory = "c:\\";
            openFileDialogCash.Filter = "xls files (*.xls)|*.xls";
            openFileDialogCash.FilterIndex = 1;
            openFileDialogCash.RestoreDirectory = true;
            openFileDialogCash.Multiselect = false;

            if (openFileDialogCash.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((streamCash = openFileDialogCash.OpenFile()) != null)
                    {
                        using (streamCash)
                        {
                            foreach (var fileName in openFileDialogCash.FileNames)
                            {
                                listViewCash.Items.Add(new ListViewItem(fileName));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не могу прочитать файл Наличка: " + ex.Message);
                }
            }
        }

        private void CreateReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewCash.Items.Count == 1 && listViewTinkoff.Items.Count >= 1
                    && listViewTinkoff.Items.Count < 3)
                {


                    List<Report> list = new List<Report>();

                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    Excel.Range range;

                    int rCnt = 0;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(listViewCash.Items[0].Text, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    range = xlWorkSheet.UsedRange;

                    for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                    {
                        list.Add(new Report()
                        {
                            date = DateTime.FromOADate((double)(range.Cells[rCnt, 10] as Excel.Range).Value2),
                            value = (double)(range.Cells[rCnt, 6] as Excel.Range).Value2,
                            category = (string)(range.Cells[rCnt, 9] as Excel.Range).Value2,
                            description = (string)(range.Cells[rCnt, 4] as Excel.Range).Value2
                        });
                    }

                    xlWorkBook.Close(true, null, null);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    for (int i = 0; i < listViewTinkoff.Items.Count; i++)
                    {
                        foreach (var repLine in File.ReadAllLines(listViewTinkoff.Items[i].Text)
                            .Where(line => line.Split(new[] { ';' }).ElementAt(3).Replace("\"", "") == "OK"
                                && Double.Parse(line.Split(new[] { ';' }).ElementAt(4).Replace("\"", "")) < 0)
                            )
                        {
                            list.Add(new Report()
                            {
                                category = repLine.Split(new[] { ';' }).ElementAt(8).Replace("\"", ""),
                                date = DateTime.Parse(repLine.Split(new[] { ';' }).ElementAt(0).Replace("\"", "")),
                                description = repLine.Split(new[] { ';' }).ElementAt(10).Replace("\"", ""),
                                value = Double.Parse(repLine.Split(new[] { ';' }).ElementAt(4).Replace("\"", "").Replace("-", ""))
                            });
                        }
                    }

                    Excel.Application xlAppReport;
                    Excel.Workbook xlWorkBookReport;

                    xlAppReport = new Excel.Application();
                    xlWorkBookReport = xlAppReport.Workbooks.Add();
                    Excel.Worksheet xlWorkSheetReport = xlWorkBookReport.Worksheets[1];

                    var distCat = list.Select(item => item.category).Distinct().OrderBy(item => item);

                    int row = 1;
                    double summarySum = 0;

                    foreach (var cat in distCat)
                    {
                        var catList = list
                            .Where(item => item.category == cat)
                            .OrderBy(item => item.date)
                            .ThenBy(item => item.value)
                            .ThenBy(item => item.description);

                        int headerRow = row;
                        double headerSum = 0;
                        xlWorkSheetReport.Cells[row, 1] = cat.ToString();
                        row++;
                        foreach (var el in catList)
                        {
                            headerSum += el.value;
                            xlWorkSheetReport.Cells[row, 2] = el.date;
                            xlWorkSheetReport.Cells[row, 3] = el.value;
                            xlWorkSheetReport.Cells[row, 4] = el.description;
                            row++;
                        }
                        xlWorkSheetReport.Cells[headerRow, 3] = headerSum;
                        Excel.Range ran = xlWorkSheetReport.Cells[headerRow, 3];
                        ran.Font.Bold = true;
                        summarySum += headerSum;
                        if (cat == distCat.Last())
                        {
                            xlWorkSheetReport.Cells[row, 1] = "ИТОГО";
                            xlWorkSheetReport.Cells[row, 3] = summarySum;
                            (xlWorkSheetReport.Cells[row, 3]).Font.Bold = true;
                        }
                    }

                    Excel.Range r = xlWorkSheetReport.Range["B:B"];
                    r.EntireColumn.ColumnWidth = 20;

                    string path = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToLongDateString().Replace(":", "_") + ".xlsx";
                    if (File.Exists(path))
                        File.Delete(path);
                    xlWorkBookReport.SaveAs(Filename: path);
                    MessageBox.Show("Отчет " + path + " успешно создан");

                    xlWorkBookReport.Close();
                    xlAppReport.Quit();

                    releaseObject(xlWorkSheetReport);
                    releaseObject(xlWorkBookReport);
                    releaseObject(xlAppReport);
                }
                else
                {
                    MessageBox.Show("Количество отчетов Наличка не равно одному или количество отчетов Тинькофф не равно 1 или 2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании отчета: " + ex.Message);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

    struct Report
    {
        public DateTime date { get; set; }
        public double value { get; set; }
        public string category { get; set; }
        public string description { get; set; }
    }
}
