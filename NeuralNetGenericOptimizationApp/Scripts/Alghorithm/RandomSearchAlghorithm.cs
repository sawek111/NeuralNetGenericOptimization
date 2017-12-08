using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetGenericOptimizationApp.Scripts;
using NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm;

namespace NeuralNetGenericOptimizationApp
{
    class RandomSearchAlghorithm
    {
        private const int MAX_ITERATIONS = 1000;

        private int _iterationsLimit = 0;
        private float _satisfactoryAccurancy = 1.0f;

        public RandomSearchAlghorithm(int iterationsLimit)
        {
            _iterationsLimit = (iterationsLimit < MAX_ITERATIONS) ? iterationsLimit : MAX_ITERATIONS;
            _satisfactoryAccurancy = 1.0f;

            return;
        }

        public RandomSearchAlghorithm(float satisfactoryAccurancy)
        {
            _iterationsLimit = MAX_ITERATIONS;
            _satisfactoryAccurancy = satisfactoryAccurancy;
            return;
        }

        public Individual Evaluate()
        {
            Individual best = new Individual();
            for (int i = 0; i < _iterationsLimit; i++)
            {
                Individual newIndividual = new Individual();
                best = Individual.GetBetterIndividual(best, newIndividual);
                if (best.GetFitness() >= _satisfactoryAccurancy)
                {
                    break;
                }
            }

            return best;
        }
    }
}
