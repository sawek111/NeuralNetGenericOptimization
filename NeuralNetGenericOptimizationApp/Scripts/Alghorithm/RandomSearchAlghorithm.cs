using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetGenericOptimizationApp.Scripts;

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

        public void Evaluate()
        {

            for (int i = 0; i < _iterationsLimit; i++)
            {
                double[] results = RManager.rManager.Count();
                if (results[0] >= _satisfactoryAccurancy)
                {
                    break;
                    //TODO save to file : best params + time  
                }
            }
            SaveToFile();

            return;
        }

        private void SaveToFile()
        {

        }
    }
}
