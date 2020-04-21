using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LogConverterFramework
{
    public partial class MainWindow : Window
    {
        private readonly Services.ExcelService _excelSrvc = new Services.ExcelService();

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
                TxtStatus.Text = $"Starting...";
                Dispatcher.Invoke(() => { }, DispatcherPriority.Background);

                ShowSpinner();

                var items = new List<string>();
                foreach (var i in lbFiles.Items)
                {
                    items.Add(i.ToString());
                }

                int k = 1;
                foreach (string textFile in items)
                {
                    try
                    {
                        TxtStatus.Text = $"Processing file #{k} -- {textFile}";
                        Dispatcher.Invoke(() => { }, DispatcherPriority.Background);

                        var logData = _excelSrvc.ReadLogData(textFile);
                        if (_excelSrvc.SaveLogExcel(logData))
                        {
                            lbConvertedFiles.Items.Add(logData.FilePath);
                            lbFiles.Items.RemoveAt(lbFiles.Items.IndexOf(textFile));
                            Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
                        }
                        else
                        {
                            lbErrors.Items.Add($"Error converting {textFile}");
                            Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
                        }
                    }
                    catch
                    {
                        lbErrors.Items.Add($"Error converting {textFile}");
                        Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
                    }

                    lbFiles.Items.Refresh();
                    Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
                    k++;
                }

                HideSpinner();

                TxtStatus.Text = $"Successfully Converted: {lbConvertedFiles.Items.Count}, Failures: {lbErrors.Items.Count}";
                Dispatcher.Invoke(() => { }, DispatcherPriority.Background);

            }
            catch (Exception exception)
            {
                lblStatus.Content = "Error: " + exception.Message;
                HideSpinner();
            }
        }

        private void HideSpinner()
        {
            Dispatcher.Invoke(() =>
            {
                icoRefresh.Visibility = Visibility.Hidden;
            }, DispatcherPriority.Background);
        }

        private void ShowSpinner()
        {
            Dispatcher.Invoke(() =>
            {
                icoRefresh.Visibility = Visibility.Visible;
                icoRefresh.Spin = true;
            }, DispatcherPriority.Background);
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