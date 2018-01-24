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
        private bool _timeDependent = false;

        public RandomSearchAlghorithm(int iterationsLimit)
        {
            _iterationsLimit = (iterationsLimit < MAX_ITERATIONS) ? iterationsLimit : MAX_ITERATIONS;
            _satisfactoryAccurancy = 1.0f;

            return;
        }

        /// <summary>
        /// TO calculate for given time 
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="isIterationsLimit"></param>
        public RandomSearchAlghorithm()
        {
            _iterationsLimit = int.MaxValue;
            _satisfactoryAccurancy = 1.0f;
            _timeDependent = true;

            return;
        }

        public RandomSearchAlghorithm(float satisfactoryAccurancy)
        {
            _iterationsLimit = MAX_ITERATIONS;
            _satisfactoryAccurancy = satisfactoryAccurancy;
            _timeDependent = false;

            return;
        }

        public Individual Evaluate()
        {
            if(_timeDependent)
            {
                throw new Exception("Can only call Evaluate(int seconds) for this object, try to make randomSearch with parameter in constructor to call that function");
            }
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

        public Individual Evaluate(double seconds)
        {
            if (!_timeDependent)
            {
                throw new Exception("Can only call Evaluate() for this object, try to make randomSearch with parameterless i constructor to call that function");
            }

            Individual best = new Individual();
            DateTime startCount = DateTime.Now;

           while((DateTime.Now - startCount).TotalSeconds < seconds)
            {
                Individual newIndividual = new Individual();
                best = Individual.GetBetterIndividual(best, newIndividual);
            }

            return best;
        }
    }
}
