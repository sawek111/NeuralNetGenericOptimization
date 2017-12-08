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
        public void RandomSearch()
        {
            RandomSearchAlghorithm randomSearchAlghorithm = new RandomSearchAlghorithm(4);
            randomSearchAlghorithm.Evaluate();

            return;
        }

        public void MemeticSearch(int Generations, int GenerationSize, int neighbourhoodSize)
        {
            GeneticAlgorithm genetic = new GeneticAlgorithm();
            LocalSearchAlghorithm localSearch = new LocalSearchAlghorithm();

            Population population = new Population(GenerationSize);
            Individual best = new Individual();
            int generationNr = 0;

            while (generationNr < Generations)
            {
                generationNr++;
                population = genetic.Evolve(population, SelectionType.Rank, CrossingType.SinglePoint, elitism: true);
                Individual localBest =  localSearch.CheckNeighbourhood(population.GetRepresentation(5), 5);
                best = Individual.GetBetterIndividual(localBest, best);
            }
            best = Individual.GetBetterIndividual(population.GetMostAccurant(1)[0], best);

            return;
        }

    }
}
