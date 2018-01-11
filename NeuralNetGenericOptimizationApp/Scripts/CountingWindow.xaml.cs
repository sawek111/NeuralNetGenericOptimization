using NeuralNetGenericOptimizationApp.Scripts.Utils;
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

        private Optimizer _optimizer = null;

        private bool _dataChosen = false;
        private bool _isCalculating = false;

        private string _datasetPath;

        private int[] _generationsArray = null;
        private int[] _generationSizeArray = null;
        private int[] _neighborsArray = null;
        private double[] _mutatationRateArray = null;

        private string _destinationFilePath;

        private bool _classColumnNumberFilled = false;

        public CountingWindow()
        {
            InitializeComponent();
            GenerateOptimizator();

            return;
        }

        private void Count(object sender, RoutedEventArgs e)
        {
            if (IsReady())
            {
                _isCalculating = true;
                string savingPath = (_destinationFilePath == null) ? Directory.GetCurrentDirectory() + "/results.xlsx" : _destinationFilePath;
                _optimizer.SetSavePath(savingPath);
                _optimizer.Calculate(_generationsArray, _generationSizeArray, _neighborsArray, _mutatationRateArray);
                _isCalculating = false;
            }

            return;
        }

        /// <summary>
        /// Choose excel file to save into results
        /// </summary>
        private void ChooseDestinationFile(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileInfo fileInfo = new FileInfo(filename);
                if (fileInfo.Extension == ".xlsx")
                {
                    _destinationFilePath = filename;
                    DestinationTextBox.Text = filename;
                    DestinationTextBox.Background = Brushes.Green;
                    return;
                }
            }
            DestinationTextBox.Background = Brushes.Red;
            _dataChosen = false;

            return;
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
            return _dataChosen && _classColumnNumberFilled && !_isCalculating && AreParametersSet();
        }

        private void GenerateOptimizator()
        {
            _optimizer = new Optimizer();
            return;
        }

        private bool AreParametersSet()
        {
            return _generationsArray != null && _generationSizeArray != null && _neighborsArray != null && _mutatationRateArray != null;
        }

        private void GenerationsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterArray<int>(ref _generationsArray, GenerationsTB);
            return;
        }

        private void GenerationSizeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterArray<int>(ref _generationSizeArray, GenerationSizeTB);
            return;
        }

        private void NeighborsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterArray<int>(ref _neighborsArray, NeighborsTB);
            return;
        }

        private void MutationRateTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterArray<double>(ref _mutatationRateArray, MutationRateTB);
        }

        private void UpdateParameterArray<T>(ref T[] parameterArray, TextBox textBoxToRead)
        {
            T[] values = Common.LoadValuesArrayFromTextbox<T>(textBoxToRead);
            
            if (values.Length > 0)
            {
                textBoxToRead.Background = Brushes.Green;
                parameterArray = values;
                return;
            }
            textBoxToRead.Background = Brushes.Red;
            parameterArray = null;

            return;
        }
    }
}
