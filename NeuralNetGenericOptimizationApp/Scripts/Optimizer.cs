using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    public class Optimizer
    {
        public void Evaluate()
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            string path = @"C:/Users/Saper/Documents/NeuralNetGenericOptimization/NeuralNetGenericOptimizationApp/nnt.R";
            engine.Evaluate("source('" + path + "')");
            string[] text = engine.Evaluate("lol('saas')").AsCharacter().ToArray();
            Console.ReadLine();
            Console.WriteLine(text[0]);
            //engine.Initialize()
            //Console.WriteLine("ALL OK");
        }


    }
}
