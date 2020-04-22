using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LogConverterCore
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
                StartTask();
            }
            catch (Exception exception)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    lblStatus.Content = "Error: " + exception.Message;
                    icoRefresh.Visibility = Visibility.Hidden;
                }), DispatcherPriority.Background);
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

        private async void StartTask()
        {
            icoRefresh.Visibility = Visibility.Visible;
            icoRefresh.Spin = true;

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
                    await Task.Run(() => ConvertLogs(textFile));
                }
                catch
                {
                    lbErrors.Items.Add($"Error converting {textFile}");
                }
                k++;
            }

            TxtStatus.Text = $"Successfully Converted: {lbConvertedFiles.Items.Count}, Failures: {lbErrors.Items.Count}";
            icoRefresh.Visibility = Visibility.Hidden;
        }

        private void ConvertLogs(string textFile)
        {
            try
            {
                var logData = _excelSrvc.ReadLogData(textFile);
                if (_excelSrvc.SaveLogExcel(logData))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lbConvertedFiles.Items.Add(logData.FilePath);
                        lbFiles.Items.RemoveAt(lbFiles.Items.IndexOf(textFile));
                    }), DispatcherPriority.Background);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lbErrors.Items.Add($"Error converting {textFile}");
                    }), DispatcherPriority.Background);
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.ToString());
            }
        }

    }
}