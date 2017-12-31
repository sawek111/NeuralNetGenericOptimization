using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    /// <summary>
    /// Interaction logic for CountingWindow.xaml
    /// </summary>
    public partial class CountingWindow : Window
    {
        public const int GENERATIONS = 5;
        public const int GENERATION_SIZE = 5;
        public const int NEIGHBORHOOD_SIZE = 5;

        private Optimizer _optimizer = null;

        private bool _dataChosen = false;
        private bool _isCalculating = false;
        private string _datasetPath;

        private bool _classColumnNumberFilled = false;

        public CountingWindow()
        {
            InitializeComponent();
            GenerateOptimizator();

            return;
        }

        private void DoMemetic(object sender, RoutedEventArgs e)
        {
            if (IsReady())
            {
                _isCalculating = true;
                _optimizer.MemeticSearch(GENERATIONS, GENERATION_SIZE, NEIGHBORHOOD_SIZE);
                _isCalculating = false;
            }

            return;
        }

        private void DoRandom(object sender, RoutedEventArgs e)
        {
            if (IsReady())
            {
                _isCalculating = true;
                _optimizer.RandomSearch();
                _isCalculating = false;
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
            return _dataChosen && _classColumnNumberFilled && !_isCalculating;
        }

        private void GenerateOptimizator()
        {
            _optimizer = new Optimizer();
            return;
        }
    }
}
