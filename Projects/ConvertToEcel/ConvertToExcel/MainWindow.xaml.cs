using Microsoft.Win32;
using System;
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
                       JOB_ID = x[0],
                       LOAD_STEP = x[1],
                       STATUS = x[2],
                       START_TS = x[3],
                       END_TS = x[4],
                       RUN_TIME = x[5],
                       LOCK_TIME = x[6],
                       TOTAL_TIME = x[7],
                       REFRESHED = x[8],
                       INSERTED = x[9],
                       UPDATED = x[10],
                       DELETED = x[11],
                       TOTAL_I_U_D = x[12],
                       TOTAL_RECORDS = x[13],
                       I_U_D_SEC = x[14],
                       TOTAL_SEC = x[15],
                       LOADED_SEC = x[16]

                   }).ToList()
                };

                lbConvertedFiles.Items.Add(Path.GetFullPath(df.FilePath + " Converted to XLSX"));
            }
            lblStatus.Content = "Complete!";
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
    }

    public class DataRow
    {
        public DataRow()
        {

        }

        public string JOB_ID { get; set; }

        public string LOAD_STEP { get; set; }

        public string STATUS { get; set; }

        public string START_TS { get; set; }

        public string END_TS { get; set; }

        public string RUN_TIME { get; set; }

        public string LOCK_TIME { get; set; }

        public string TOTAL_TIME { get; set; }

        public string REFRESHED { get; set; }

        public string INSERTED { get; set; }

        public string UPDATED { get; set; }

        public string DELETED { get; set; }

        public string TOTAL_I_U_D { get; set; }

        public string TOTAL_RECORDS { get; set; }

        public string I_U_D_SEC { get; set; }

        public string TOTAL_SEC { get; set; }

        public string LOADED_SEC { get; set; }
    }
}