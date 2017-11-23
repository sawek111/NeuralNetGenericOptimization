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
                CopyBestToNewPopultaion();
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
                    //TODO DANIEL : Musi zwracać Individual metodą Rang
                    return null;
                }
                case SelectionType.Tournament:
                {
                    return TournamentSelection(population);
                }
                case SelectionType.Roulette:
                {
                    //TODO DANIEL: Musi zwracać Individual metodą Ruletki
                    return null;
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

        void Costa(ref int kdsak)
        {
            kdsak = 10;
        }

        private static void CopyBestToNewPopultaion()
        {
            //TODO DANIEL : dodaj elityzm
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
