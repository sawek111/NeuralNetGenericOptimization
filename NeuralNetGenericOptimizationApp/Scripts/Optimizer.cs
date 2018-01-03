using NeuralNetGenericOptimizationApp.Scripts.Alghorithm;
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

        public void Calculate()
        {

        }


        public void RandomSearch()
        {
            RandomSearchAlghorithm randomSearchAlghorithm = new RandomSearchAlghorithm(4);
            Individual best  = randomSearchAlghorithm.Evaluate();

            return;
        }

        public float MemeticSearch(int[] generationsArray, int[] generationSizeArray, int[] neighbourhoodSizeArray, float[] mutationRateArray)
        {
            float time = 0.0f;
            for(int crossing = 0; crossing < Enum.GetNames(typeof(CrossingType)).Length; crossing++)
            {
                for(int selection = 0; selection < Enum.GetNames(typeof(SelectionType)).Length; selection++)
                {
                    
                    CalculateMemeticSearchForAllParameters((SelectionType)selection,(CrossingType)crossing, generationsArray, generationSizeArray, mutationRateArray, neighbourhoodSizeArray);
                }
            }

            return time;
        }

        private void CalculateMemeticSearchForAllParameters(SelectionType selection, CrossingType crossing, int[] GenerationsArray, int[] GenerationSizeArray, float[] mutationRateArray, int[] neighbourhoodSizeArray)
        {
            for(int generations = 0; generations < GenerationsArray.Length; generations++)
            {
                for(int generationSize = 0; generationSize < GenerationSizeArray.Length; generationSize++)
                {
                    for(int mutationRate = 0; mutationRate < mutationRateArray.Length; mutationRate++)
                    {
                        for(int neighbourhoodSize = 0; neighbourhoodSize < neighbourhoodSizeArray.Length; neighbourhoodSize++)
                        {
                            CalculateMemetic(selection, crossing, generations, generationSize, mutationRate, neighbourhoodSize, true);
                            CalculateMemetic(selection, crossing, generations, generationSize, mutationRate, neighbourhoodSize, false);
                            //TODO SAVE TO file 
                        }
                    }
                }
            }
        }

        private float CalculateMemetic(SelectionType selection, CrossingType crossing, int generations, int generationSize , float mutationRate, int neighbourhoodSize, bool elitism )
        {

            GeneticAlgorithm genetic = new GeneticAlgorithm();
            LocalSearchAlghorithm localSearch = new LocalSearchAlghorithm();

            Population population = new Population(generationSize);
            Individual best = new Individual();
            int generationNr = 0;

            while (generationNr < generations)
            {
                generationNr++;
                population = genetic.Evolve(population, selection, crossing, mutationRate, elitism);
                Individual localBest = localSearch.CheckNeighbourhood(population.GetRepresentation(5), neighbourhoodSize);
                best = Individual.GetBetterIndividual(localBest, best);
            }
            best = Individual.GetBetterIndividual(population.GetMostAccurant(1)[0], best);

            return best.GetFitness();
        }

        // private void 

    }
}
