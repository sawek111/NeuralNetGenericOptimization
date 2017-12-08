using NeuralNetGenericOptimizationApp.Scripts.Utils;
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

        private bool _sorted = false;

        /// <summary>
        /// Initialize population of random individuals with given size
        /// </summary>
        public Population(int size)
        {
            _population = new Individual[size];
            _population = GenerateRandomPopulation(size);

            return;
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

        public Individual[] GetPopulation()
        {
            return _population;
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
            if(!_sorted)
            {
                SortWithAccuracy();
            }
            for(int i = 0; i < best.Length; i++)
            {
                best[i] = _population[i];
            }

            return best;
        }

        /// <summary>
        /// Get the best one and count more random representants
        /// </summary>
        /// <returns></returns>
        public Individual[] GetRepresentation(int count)
        {
            Individual[] representation = new Individual[count + 1];
            representation[0] = GetMostAccurant(1)[0];
            for(int i = 1; i <= count; i++)
            {
                int index = Common.Instance.Rand.Next(0, _population.Length - 1);
                representation[i] = _population[index];
            }

            return representation;
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
            _sorted = true;

            return;
        }

    }
}
