using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace LogConverterCore
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
                RestoreDirectory = true
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
                var items = new List<string>();
                foreach (var i in lbFiles.Items)
                {
                    items.Add(i.ToString());
                }

                foreach (string textFile in items)
                {
                    try
                    {
                        var logData = _excelSrvc.ReadLogData(textFile);

                        TxtStatus.Text = $"Processing... {logData.FilePath}";

                        if (_excelSrvc.SaveLogExcel(logData))
                        {
                            lbConvertedFiles.Items.Add(logData.FilePath);
                            lbFiles.Items.RemoveAt(lbFiles.Items.IndexOf(textFile));
                        }
                        else
                        {
                            lbErrors.Items.Add($"Error converting {textFile}");
                        }
                    }
                    catch
                    {
                        lbErrors.Items.Add($"Error converting {textFile}");
                    }
                    lbFiles.Items.Refresh();
                }
                TxtStatus.Text = $"Done! Successfully Converted: {lbConvertedFiles.Items.Count}, Failures: {lbErrors.Items.Count}";
            }
            catch (Exception exception)
            {
                lblStatus.Content = "Error: " + exception.Message;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}