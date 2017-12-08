using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.Alghorithm
{
    public class LocalSearchAlghorithm
    {
        public Individual CheckNeighbourhood(Individual[] individuals, int deep)
        {
            Individual best = individuals[0];
            for (int i = 0; i < individuals.Length; i++)
            {
                Individual localBest = SearchNeighbourhood(individuals[i], deep);
                best = Individual.GetBetterIndividual(best, localBest);
            }

            return best;
        }

        private Individual SearchNeighbourhood(Individual individual, int deep)
        {
            Individual best = individual;
            for(int chromosomes  = 0; chromosomes <  Enum.GetNames(typeof(ChromosomeType)).Length; chromosomes++)
            {
                Individual additiveOffsetNeighbour = SearchOneSideNeighbourhood(individual, (ChromosomeType)chromosomes, deep, +1);
                best = Individual.GetBetterIndividual(best, additiveOffsetNeighbour);
                Individual negativeOffsetNeighbour = SearchOneSideNeighbourhood(individual, (ChromosomeType)chromosomes, deep, -1);
                best = Individual.GetBetterIndividual(best, negativeOffsetNeighbour);
            }

            return best;
        }

        private Individual SearchOneSideNeighbourhood(Individual individual, ChromosomeType type, int deep, int step)
        {
            int counter = 0;
            int offset = 0;
            Individual lastBest = individual;

            while (counter <= deep)
            {
                offset += step;
                counter++;
                Individual newNeighbour = MakeNeighbour(individual, type, offset);
                if(CanEndLocalSearch(lastBest, newNeighbour))
                {
                    break;
                }
                lastBest = newNeighbour;
            }

            return lastBest;
        }

        private Individual MakeNeighbour(Individual origin, ChromosomeType type, int offset )
        {
            int testedValue = origin.GetChromosome(type).GetGeneDecimalValue() + offset;
            if(testedValue < 0)
            {
                return null;
            }
            Individual neighbour = new Individual(origin, type, testedValue);

            return neighbour;
        }

        private bool CanEndLocalSearch(Individual lastbest, Individual newIndividual)
        {
            if(newIndividual == null)
            {
                return true;
            }

            return lastbest.GetFitness() >= newIndividual.GetFitness();
        }
    }
}
