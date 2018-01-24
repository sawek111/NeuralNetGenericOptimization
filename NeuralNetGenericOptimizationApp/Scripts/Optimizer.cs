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
        private readonly static int[] basicGenerations = new int[] { 30 };
        private static int[] basicGenerationSize = new int[] { 20 };
        private static double[] basicMutationRate = new double[] { 0.01 };
        private static int[] basicNeighbours = new int[] { 3 };

        private ExcelManager _excelManager;
        private string _pathToSave;

        private double _memeticWorstTime = 0;

        public void Calculate(int[] generationsArray, int[] generationSizeArray, int[] neighbourhoodSizeArray, double[] mutationRateArray)
        {
            _excelManager = new ExcelManager(_pathToSave);
            _memeticWorstTime = 0;

            CalculateForChosenParameter<int>(generationsArray, generationsArray, basicGenerationSize, basicNeighbours, basicMutationRate);

            CalculateForChosenParameter<int>(generationsArray, generationsArray, basicGenerationSize, basicNeighbours, basicMutationRate);
            CalculateForChosenParameter<int>(generationSizeArray, basicGenerations, generationSizeArray, basicNeighbours, basicMutationRate);
            CalculateForChosenParameter<int>(neighbourhoodSizeArray, basicGenerations, basicGenerationSize, neighbourhoodSizeArray, basicMutationRate);
            CalculateForChosenParameter<double>(mutationRateArray, basicGenerations, basicGenerationSize, basicNeighbours, mutationRateArray);

            return;
        }

        private void CalculateForChosenParameter<T>(T[] mainArray, int[] generationsArray, int[] generationSizeArray, int[] neighbourhoodSizeArray, double[] mutationRateArray)
        {
            for (int i = 0; i < mainArray.Length; i++)
            {
                _memeticWorstTime = 0;
                MemeticSearch(generationsArray, generationSizeArray, neighbourhoodSizeArray, mutationRateArray);
                RandomSearch(_memeticWorstTime);
            }

            return;
        }

        public void SetSavePath(string path)
        {
            _pathToSave = path;
            return;
        }

        private void RandomSearch(double time)
        {
            RManager.rManager.ClearHistory();
            RandomSearchAlghorithm randomSearchAlghorithm = new RandomSearchAlghorithm();
            Individual best  = randomSearchAlghorithm.Evaluate(time);
            _excelManager.WriteRow("RandomSearch", best.GetFitness().ToString(), time.ToString());
            
            return;
        }

        private void MemeticSearch(int[] generationsArray, int[] generationSizeArray, int[] neighbourhoodSizeArray, double[] mutationRateArray)
        {
            for(int crossing = 0; crossing < Enum.GetNames(typeof(CrossingType)).Length; crossing++)
            {
                for(int selection = 0; selection < Enum.GetNames(typeof(SelectionType)).Length; selection++)
                {
                   CalculateMemeticSearchForAllParameters((SelectionType)selection,(CrossingType)crossing, generationsArray, generationSizeArray, mutationRateArray, neighbourhoodSizeArray);
                }
            }

            return;
        }

        private void CalculateMemeticSearchForAllParameters(SelectionType selection, CrossingType crossing, int[] GenerationsArray, int[] GenerationSizeArray, double[] mutationRateArray, int[] neighbourhoodSizeArray)
        {
            for(int generations = 0; generations < GenerationsArray.Length; generations++)
            {
                for(int generationSize = 0; generationSize < GenerationSizeArray.Length; generationSize++)
                {
                    for(int mutationRate = 0; mutationRate < mutationRateArray.Length; mutationRate++)
                    {
                        for(int neighbourhoodSize = 0; neighbourhoodSize < neighbourhoodSizeArray.Length; neighbourhoodSize++)
                        {
                            Console.WriteLine(selection.ToString() + "  " + crossing.ToString());
                            CalculateMemetic(selection, crossing, GenerationsArray[generations], GenerationSizeArray[generationSize], (float)mutationRateArray[mutationRate], neighbourhoodSizeArray[neighbourhoodSize], true);
                            CalculateMemetic(selection, crossing, GenerationsArray[generations], GenerationSizeArray[generationSize], (float)mutationRateArray[mutationRate], neighbourhoodSizeArray[neighbourhoodSize], false);
                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Calculates memetic, save to excel and updates _memeticWorstTime
        /// </summary>
        private void CalculateMemetic(SelectionType selection, CrossingType crossing, int generations, int generationSize , float mutationRate, int neighbourhoodSize, bool elitism )
        {
            RManager.rManager.ClearHistory();
            GeneticAlgorithm genetic = new GeneticAlgorithm();
            LocalSearchAlghorithm localSearch = new LocalSearchAlghorithm();
            DateTime startTime = DateTime.Now;
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
            string netParameters = "Decay:" + best.GetChromosome(ChromosomeType.DECAY).GetGeneFlotValue().ToString()
                + " Size:" + best.GetChromosome(ChromosomeType.HIDDEN_LAYER_SIZE).GetGeneDecimalValue().ToString()
                + " MaxIterations" + best.GetChromosome(ChromosomeType.MAX_ITERATIONS).GetGeneDecimalValue().ToString();
                
            double seconds = (DateTime.Now - startTime).TotalSeconds;
            _memeticWorstTime = (seconds > _memeticWorstTime) ? seconds : _memeticWorstTime;
            _excelManager.WriteRow(selection.ToString(), crossing.ToString(), generations.ToString(), generationSize.ToString(), mutationRate.ToString(), neighbourhoodSize.ToString(), elitism.ToString() , best.GetFitness().ToString(), ((int)seconds).ToString(), netParameters);

            return;
        }
    }
}
