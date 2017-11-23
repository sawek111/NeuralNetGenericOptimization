using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm
{
    public class Population
    {
        private Individual[] _population = null;

        /// <summary>
        /// Initialize population of random individuals with given size
        /// </summary>
        public Population(int size)
        {
            _population = new Individual[size];
            _population = GenerateRandomPopulation(size);

            return;
        }

        public int Size
        {
            get { return _population.Length; }
        }


        public Individual this[int i]
        {
            get { return _population[i]; }
            set { _population[i] = value; }
        }

        /// <summary>
        /// Get array with fittest individuals in given count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Individual[] GetMostAccurant(int count)
        {
            Individual[] best = new Individual[count];
            SortWithAccuracy();
            for(int i = 0; i < best.Length; i++)
            {
                best[i] = _population[i];
            }

            return best;
        }

        /// <summary>
        /// Prepare memory and (if fill) fill it with random individuals with given size
        /// </summary>
        public Population(int size, bool fill)
        {
            _population = new Individual[size];
            if (fill)
            {
                _population = GenerateRandomPopulation(size);
            }

            return;
        }

        private Individual[] GenerateRandomPopulation(int size)
        {
            Individual[] newPopulation = new Individual[size];
            for (int i = 0; i < newPopulation.Length; i++)
            {
                Individual individual = new Individual();
                newPopulation[i] = individual;
            }

            return newPopulation;
        }

        private void SortWithAccuracy()
        {
            int n = _population.Length;
            do
            {
                for (int i = 0; i < n - 1; i++)
                {
                    if (_population[i].GetFitness() < _population[i + 1].GetFitness())
                    {
                        Individual worstOne = _population[i];
                        _population[i] = _population[i + 1];
                        _population[i + 1] = worstOne;
                    }
                }
                n--;
            }
            while (n > 1);

            for(int i = 0; i < _population.Length; i++)
            {
                Console.WriteLine(_population[i].GetFitness());
            }

            return;
        }

    }
}
