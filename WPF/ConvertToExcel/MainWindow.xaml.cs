using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace ConvertToExcel
{
    public partial class MainWindow : Window
    {
        Services.ExcelService _excelSrvc = new Services.ExcelService();

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
            try
            {
                foreach (string textFile in lbFiles.Items)
                {
                    var logData = _excelSrvc.ReadLog(textFile);

                    if (_excelSrvc.SaveLogExcel(logData))
                        lbConvertedFiles.Items.Add(logData.FilePath);
                    else
                        lbConvertedFiles.Items.Add("Error converting: " + logData.FilePath);
                }
                lblStatus.Content = "Done!";
            }
            catch (Exception exception)
            {
                lblStatus.Content = "Error: " + exception.Message;
            }
        }
    }
}