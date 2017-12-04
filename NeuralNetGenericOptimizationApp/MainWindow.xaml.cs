using NeuralNetGenericOptimizationApp.Scripts;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeuralNetGenericOptimizationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Optimizer _optimizer = null;
        
        public MainWindow()
        {
            InitializeComponent();
            GenerateOptimizator();

        }

        private void GenerateOptimizator()
        {
            _optimizer = new Optimizer();
            return;
        }

        private void Evaluate(object sender, RoutedEventArgs e)
        {
            _optimizer.Evaluate();
        }
    }
}
