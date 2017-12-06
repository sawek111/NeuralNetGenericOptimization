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
        public static string rFilePath = @"C:/Users/Saper/Documents/NeuralNetGenericOptimization/NeuralNetGenericOptimizationApp/nnt.R";

        public static string DatasetPath;

        public static int ColumnNumber;

        public static RManager rManager = new RManager();

        private REngine _engine = null;
        private Dictionary<float[], double[]> _countingHistory = new Dictionary<float[], double[]>();

        public RManager()
        {
            if(rManager != null && rManager != this)
            {
                rManager = this;
            }
            _engine = InitRConnection(rFilePath);

            return;
        }

        public double[] Count()
        {
            if(DatasetPath.Contains('\\'))
            {
                DatasetPath = Common.ConvertPathToR(DatasetPath);
            }
            double[] result = _engine.Evaluate(GenerateFunctionCall()).AsNumeric().ToArray();

            return result;
        }


        private REngine InitRConnection(string rPath)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Evaluate("source('" + rPath + "')");

            return engine;
        }

        private string GenerateFunctionCall()
        {
            string functionCallString = "classifyWithNNt(" + "'" + DatasetPath + "'" + "," 
                + ColumnNumber.ToString() + ","
                + "10,100,0.8)";
            return functionCallString;
        }
    }
}
