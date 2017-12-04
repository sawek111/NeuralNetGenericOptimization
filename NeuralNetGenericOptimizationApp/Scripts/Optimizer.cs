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
    public class Optimizer
    {
        public const int GENERATIONS = 100;
        public const int GENERATION_SIZE = 100;


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
            Evolve(GENERATIONS, GENERATION_SIZE, SelectionType.Rank, CrossingType.SinglePoint, true);
        }

        public void Evolve(int generations, int generationSize, SelectionType selection, CrossingType crossing, bool elitism)
        {
            Population population = new Population(generationSize);
            int generationNr = 0;

            while(generationNr < generations )
            {
                generationNr++;
                population = GeneticAlgorithm.Evolve(population, selection, crossing, elitism);
                Console.WriteLine("Generation: " + generationNr + " Fittest: " + population.GetMostAccurant(1)[0]);
                
            }

            return;
        }


    }
}
