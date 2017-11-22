using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    public class GeneticAlgorithm
    {
        public static float uniformRate = 0.5f;
        public static float mutationRate = 0.015f;
        public static int tournamentSize = 5;
        public static bool elitism = true;

        public static Population Evolve(Population population)
        {
            Population newPopulation = new Population(population.Size, false);
            int elitismOffset = 0;
            if(elitism)
            {
                elitismOffset = 1;
                CopyBestToNewPopultaion();
            }
            for(int i = elitismOffset; i < population.Size; i++)
            {
                Individual father = TournamentSelection(population);
                Individual mother = TournamentSelection(population);
                Individual newIndividual = new Individual(father, mother);
                newPopulation[i] = newIndividual;
            }
            MutateRandomIndividuals(newPopulation);

            return newPopulation;
        }

        private static Individual TournamentSelection(Population population)
        {
            // Create a tournament population
            Population tournamentPopulation = new Population(tournamentSize, false);
            // For each place in the tournament get a random individual
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId = Common.Instance.Rand.Next(0, population.Size - 1);
                tournamentPopulation[i] = population[randomId];
            }
            // Get the fittest
            Individual fittest = tournamentPopulation.GetFittests(1)[0];

            return fittest;
        }

        private static void CopyBestToNewPopultaion()
        {
            Console.WriteLine("TODO!");
        }

        private static void MutateRandomIndividuals(Population newPopulation)
        {
            for (int i = 0; i < newPopulation.Size; i++)
            {
                if(mutationRate >= Common.Instance.Rand.NextDouble())
                {
                    newPopulation[i].Mutate();
                }
            }

            return;
        }
    }
}
