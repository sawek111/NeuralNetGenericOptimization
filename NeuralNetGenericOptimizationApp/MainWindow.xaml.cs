using NeuralNetGenericOptimizationApp.Scripts;
using NeuralNetGenericOptimizationApp.Scripts.Alghorithm;
using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NeuralNetGenericOptimizationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int GENERATIONS = 5;
        public const int GENERATION_SIZE = 5;

        private Optimizer _optimizer = null;

        private bool _dataChosen = false;
        private string _datasetPath;

        private bool _classColumnNumberFilled = false;

        public MainWindow()
        {
            InitializeComponent();
            GenerateOptimizator();

        }

        private void DoMemetic(object sender, RoutedEventArgs e)
        {
            if (IsReady())
            {
                _optimizer.MemeticSearch(GENERATIONS, GENERATION_SIZE);
            }
        }

        private void DoRandom(object sender, RoutedEventArgs e)
        {
            if (IsReady())
            {
                _optimizer.RandomSearch();
            }
        }

        private void ChooseDatasetFile(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileInfo fileInfo = new FileInfo(filename);
                Console.WriteLine(fileInfo.Extension);
                if (fileInfo.Extension == ".csv")
                {
                    _datasetPath = filename;
                    DatasetText.Text = filename;
                    DatasetText.Background = Brushes.Green;
                    RManager.DatasetPath = filename;
                    _dataChosen = true;
                    return;
                }
            }
            DatasetText.Background = Brushes.Red;
            _dataChosen = false;

            return;
        }

        private void ChooseColumnNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int result;
            if (int.TryParse(textBox.Text, out result))
            {
                if (result > 0)
                {
                    _classColumnNumberFilled = true;
                    textBox.Background = Brushes.LightGreen;
                    RManager.ColumnNumber = result;
                    return;
                }
            }
            _classColumnNumberFilled = false;

            return;
        }

        private bool IsReady()
        {
            return _dataChosen && _classColumnNumberFilled;
        }

        private void GenerateOptimizator()
        {
            _optimizer = new Optimizer();
            return;
        }

    }
}
