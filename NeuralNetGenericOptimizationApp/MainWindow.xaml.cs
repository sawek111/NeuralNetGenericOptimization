using NeuralNetGenericOptimizationApp.Scripts;
using NeuralNetGenericOptimizationApp.Scripts.Alghorithm;
using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;

namespace NeuralNetGenericOptimizationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectR(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".r";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string fileName = dlg.FileName;
                FileInfo fileInfo = new FileInfo(fileName);
                Console.WriteLine(fileInfo.Extension);
                if (fileInfo.Extension == ".R")
                {
                    string path = Common.ConvertPathToR(fileName);
                    if(RManager.rManager.InitRConnection(path))
                    {
                        _textBox.Text = path;
                        _textBox.Background = Brushes.Green;
                        CountingWindow countingWindow = new CountingWindow();
                        countingWindow.ShowDialog();
                        this.Close();

                        return;
                    }
                }
            }
            _textBox.Background = Brushes.Red;

            return;
        }

    }
}
