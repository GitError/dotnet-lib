using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ConvertToExcel
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                    lbFiles.Items.Add(Path.GetFullPath(filename));
            }
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            int asInt = 0;
            DateTime asDt = DateTime.MinValue;

            foreach (string textFile in lbFiles.Items)
            {
                var df = new DataSet
                {
                    FilePath = textFile,
                    Rows = File.ReadAllLines(textFile)
                    .Skip(1)
                   .Select(x => x.Split(','))
                   .Select(x => new DataRow
                   {
                       JOB_ID = int.TryParse(x[0], out asInt) ? asInt : 0,
                       LOAD_STEP = x[1],
                       STATUS = x[2],
                       START_TS = DateTime.TryParse(x[3], out asDt) ? asDt : DateTime.MinValue,
                       END_TS = DateTime.TryParse(x[4], out asDt) ? asDt : DateTime.MinValue,
                       RUN_TIME = x[5],
                       LOCK_TIME = x[6],
                       TOTAL_TIME = x[7],
                       REFRESHED = int.TryParse(x[8], out asInt) ? asInt : 0,
                       INSERTED = int.TryParse(x[9], out asInt) ? asInt : 0,
                       UPDATED = int.TryParse(x[10], out asInt) ? asInt : 0,
                       DELETED = int.TryParse(x[11], out asInt) ? asInt : 0,
                       TOTAL_I_U_D = int.TryParse(x[12], out asInt) ? asInt : 0,
                       TOTAL_RECORDS = int.TryParse(x[13], out asInt) ? asInt : 0,
                       I_U_D_SEC = int.TryParse(x[14], out asInt) ? asInt : 0,
                       TOTAL_SEC = int.TryParse(x[15], out asInt) ? asInt : 0,
                       LOADED_SEC = int.TryParse(x[16], out asInt) ? asInt : 0
                   })
                   .Where(x => x.JOB_ID != 0)
                   .ToList()
                };

                if (Convert(df))
                    lbConvertedFiles.Items.Add(Path.GetFullPath(df.FilePath[0..^4] + ".xlsx"));
                else
                    lbConvertedFiles.Items.Add("Error converting " + Path.GetFullPath(df.FilePath[0..^4] + ".xlsx"));
            }

            lblStatus.Content = "Done!";
        }

        private bool Convert(DataSet ds)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Data");

                ws.Cell(2, 1).InsertData(ds.Rows.ToList());

                ws.Rows(1, 1).Style.Font.Bold = true;

                var header = ("JOB ID,LOAD STEP,STATUS,START TS,END TS,RUN TIME,LOCK TIME,TOTAL TIME,REFRESHED,INSERTED,UPDATED,DELETED," +
                    "TOTAL I+U+D,TOTAL RECORDS,I+U+D/SEC,TOTAL/SEC,LOADED/SEC,").Split(",").ToArray();

                for (int i = 1; i < 17; i++)
                    ws.Cell(1, i).Value = header[i - 1].ToString();

                ws.Columns().Width = 15;
                ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.SheetView.Freeze(1, 16);

                ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;

                wb.SaveAs(Path.GetFullPath(ds.FilePath[0..^4] + ".xlsx"));

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
    }

    public class DataSet
    {
        public DataSet()
        {
            Rows = new List<DataRow>();
        }

        public string FilePath { get; set; }

        public List<DataRow> Rows { get; set; }

        internal IEnumerable AsEnumerable()
        {
            throw new NotImplementedException();
        }
    }

    public class DataRow
    {
        public DataRow()
        {

        }

        public int JOB_ID { get; set; }

        public string LOAD_STEP { get; set; }

        public string STATUS { get; set; }

        public DateTime START_TS { get; set; }

        public DateTime END_TS { get; set; }

        public string RUN_TIME { get; set; }

        public string LOCK_TIME { get; set; }

        public string TOTAL_TIME { get; set; }

        public int REFRESHED { get; set; }

        public int INSERTED { get; set; }

        public int UPDATED { get; set; }

        public int DELETED { get; set; }

        public int TOTAL_I_U_D { get; set; }

        public int TOTAL_RECORDS { get; set; }

        public int I_U_D_SEC { get; set; }

        public int TOTAL_SEC { get; set; }

        public int LOADED_SEC { get; set; }
    }
}