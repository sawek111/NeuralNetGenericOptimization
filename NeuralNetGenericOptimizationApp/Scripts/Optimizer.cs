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
      
        public void Evaluate(int Generations, int GenerationSize)
        {
            double[] text = RManager.rManager.Count();
            Console.ReadLine();
            //engine.Initialize()
            //Console.WriteLine("ALL OK");
            Evolve(Generations, GenerationSize, SelectionType.Rank, CrossingType.SinglePoint, true);
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
