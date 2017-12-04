using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    public class GeneticAlgorithm
    {
        private const float MUTATTION_RATE = 0.015f;
        private const int TOURNAMENT_SIZE = 5;
        private const int MAX_PARENTS = 4;
        private const int ELITISM_SIZE = 2;
        private const int MULTI_POINTS_CROSSING = 4;


        public static Population Evolve(Population population, SelectionType selectionType, CrossingType crossingType, bool elitism)
        {
            Population newPopulation = new Population(population.Size, false);
            int elitismOffset = 0;
            if(elitism)
            {
                elitismOffset = ELITISM_SIZE;
                CopyBestToNewPopultaion(population, newPopulation);
            }
            for(int i = elitismOffset; i < population.Size; i++)
            {
                Individual[] chosenParents = ChooseParents(selectionType, population);
                Individual child = CombineParents(chosenParents, crossingType);
                newPopulation[i] = child;
            }
            MutateRandomIndividuals(newPopulation);

            return newPopulation;
        }

        private static Individual CombineParents(Individual[] parents, CrossingType crossingType)
        {
            Individual child = new Individual();
            switch(crossingType)
            {
                case CrossingType.MultiParenting:
                {
                    child.CombineParentsChromosomes(parents);
                    break;
                }
                case CrossingType.SinglePoint:
                {
                    child.CombineParentsChromosomes(parents[0],parents[1]);
                    break;
                }
                case CrossingType.MultiPoints:
                {
                    child.CombineParentsChromosomes(parents[0], parents[1], MULTI_POINTS_CROSSING);
                    break;
                }
            }

            return child;
        }

        private static Individual[] ChooseParents(SelectionType selectionType, Population population)
        {
            Individual[] newChosenParents = new Individual[MAX_PARENTS]; 
            for (int i = 0; i < MAX_PARENTS; i++)
            {
                newChosenParents[i] = DoSelection(selectionType, population);
            }

            return newChosenParents;
        }

        private static Individual DoSelection(SelectionType selectionType, Population population)
        {
            switch(selectionType)
            {
                case SelectionType.Rank:
                {
                        return RankSelection(population);
                }
                case SelectionType.Tournament:
                {
                    return TournamentSelection(population);
                }
                case SelectionType.Roulette:
                {
                   return RouletteSelection(population);
                }
                default:
                {
                    return null;
                }
            }

            return null;
        }


        private static Individual TournamentSelection(Population population)
        {
            // Create a tournament population
            Population tournamentPopulation = new Population(TOURNAMENT_SIZE, false);
            // For each place in the tournament get a random individual
            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                int randomId = Common.Instance.Rand.Next(0, population.Size - 1);
                tournamentPopulation[i] = population[randomId];
            }
            // Get the fittest
            Individual fittest = tournamentPopulation.GetMostAccurant(1)[0];

            return fittest;
        }

        /// <summary>
        /// Selection by roulette wheel
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        private static Individual RouletteSelection(Population population)
        {
            //fitness of whole population
            float totalFitness = 0;
            //calculating sum of fitness
            for (int i = 0; i < population.Size; i++)
            {
                totalFitness += population[i].GetFitness();
            }
            //array of probabilities
            float[] probabilityOfWholePopulation = new float[population.Size];

            //calculating probability of each individual
            for (int i = 0; i < population.Size; i++)
            {
                probabilityOfWholePopulation[i] = population[i].GetFitness() / totalFitness;
            }

            //draw
            float random = Common.Instance.Rand.Next(0, 1) * totalFitness;

            for (int i = 0; i < population.Size; i++)
            {
                random -= probabilityOfWholePopulation[i];
                if (random <= 0) return population[i];
            }
            //return last individual while error
            return population[population.Size - 1];
        }

        /// <summary>
        /// Selection by linear ranking
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        private static Individual RankSelection(Population population)
        {
            //sorting by fitness
            Array.Sort(population.GetPopulation(),
                delegate (Individual x, Individual y) { return x.GetFitness().CompareTo(y.GetFitness()); });

            //rank sum of whole population
            float rankSum = 0;
            //calculating sum of ranks (worst individual on first pos and it has rank equals 1)
            for (int i = 0; i < population.Size; i++)
            {
                rankSum += i;
            }

            //array of probabilities
            float[] probabilityOfWholePopulation = new float[population.Size];

            //calculating probability of each individual
            for (int i = 0; i < population.Size; i++)
            {
                probabilityOfWholePopulation[i] = population[i].GetFitness() / rankSum;
            }

            //draw
            float random = Common.Instance.Rand.Next(0, 1) * rankSum;

            for (int i = 0; i < population.Size; i++)
            {
                random -= probabilityOfWholePopulation[i];
                if (random <= 0) return population[i];
            }

            //return last individual while error
            return population[population.Size - 1];
        }

        /// <summary>
        /// Coping few best individuals to new population
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        private static void CopyBestToNewPopultaion(Population population, Population newPopulation)
        {
            Individual[] bestIndividuals = population.GetMostAccurant(ELITISM_SIZE);
            for(int i =0; i< ELITISM_SIZE; i++)
            {
                newPopulation[i] = bestIndividuals[i];
            }

            return;
        }

        private static void MutateRandomIndividuals(Population newPopulation)
        {
            for (int i = 0; i < newPopulation.Size; i++)
            {
                if(MUTATTION_RATE >= Common.Instance.Rand.NextDouble())
                {
                    newPopulation[i].Mutate();
                }
            }

            return;
        }
    }
}
