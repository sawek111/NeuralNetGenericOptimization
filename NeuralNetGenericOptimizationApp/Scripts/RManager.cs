using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    public class RManager
    {
     //   public static string rFilePath = @"C:/Users/Saper/Documents/NeuralNetGenericOptimization/NeuralNetGenericOptimizationApp/nnt.R";
        public static string DatasetPath;

        public static int ColumnNumber;

        public static RManager rManager = new RManager();

        private REngine _engine = null;
        private Dictionary<object[], double[]> _countingHistory = new Dictionary<object[], double[]>();

        public RManager()
        {
            if(rManager != null && rManager != this)
            {
                rManager = this;
            }

            return;
        }

        public void ClearHistory()
        {
            _countingHistory.Clear();
            return;
        }

        public bool InitRConnection(string rPath)
        {
            bool testFunctionCalledProperly = false;
            try
            {
                REngine.SetEnvironmentVariables();
                REngine engine = REngine.GetInstance();
                engine.Evaluate("source('" + rPath + "')");
                if(engine!=null)
                {
                    _engine = engine;
                }
                testFunctionCalledProperly = CallTestFunction();

                return true;
            }
            catch(Exception exception)
            {
                Console.WriteLine("ERRROR " + exception.Message);
                return false;
            }
        }

        public double[] GetResult(Individual individual)
        {
            AutoFixPath();
            object[] key = GenerateKey(
                individual.GetChromosome(ChromosomeType.HIDDEN_LAYER_SIZE).GetGeneDecimalValue(),
                individual.GetChromosome(ChromosomeType.MAX_ITERATIONS).GetGeneDecimalValue(),
                individual.GetChromosome(ChromosomeType.MAX_ITERATIONS).GetGeneFlotValue());


            if (_countingHistory.ContainsKey(key))
            {
                return _countingHistory[key];
            }
            string functionCall = GenerateFunctionCall((int)key[0], (int)key[1], GetProperFormatFloat(key[2]));
            Console.WriteLine(functionCall);
            double[] result = _engine.Evaluate(functionCall).AsNumeric().ToArray();
            _countingHistory.Add(key, result);
            
            return result;
        }

        private object[] GenerateKey(params object[] arguments)
        {
            return arguments;
        }

        private float GetProperFormatFloat(object floatSavedAsObject)
        {
            string asText = floatSavedAsObject.ToString();
            float tmpFloat;
            float.TryParse(asText.Replace(',', '.'), out tmpFloat);

            return tmpFloat;
        }

        private bool CallTestFunction()
        {
            return (_engine.Evaluate("test()").AsNumeric().ToArray()[0] == 1.0);
        }

        private string GenerateFunctionCall(int size, int maxIterations, float decayValue)
        {
            string functionCallString = "classifyWithNNt(" + "'" + DatasetPath + "'" + "," 
                + ColumnNumber.ToString() + ","
                + size + ","
                + maxIterations + ","
                + decayValue + ")";

            return functionCallString;
        }

        private void AutoFixPath()
        {
            if (DatasetPath.Contains('\\'))
            {
                DatasetPath = Common.ConvertPathToR(DatasetPath);
            }

            return;
        }
    }
}
