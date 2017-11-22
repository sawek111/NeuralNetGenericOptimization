using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    public static class Settings
    {
        private const int HIDDEN_LAYER_SIZE_CHROMOSOME_BITS = 16;
        private const int HIDDEN_LAYERS_COUNT_CHROMOSOME_BITS = 6;
        private const int MAX_ITERATIONS_CHROMOSOME_BITS = 8;
        private const int DECAY_CHROMOSOME_BITS = 8;

        /// <summary>
        /// Get number of bits for chromosome's type based on settings
        /// </summary>
        public static int GetChromosomeSize(ChromosomeType type)
        {
            switch(type)
            {
                case ChromosomeType.DECAY:
                {
                    return DECAY_CHROMOSOME_BITS;
                }
                case ChromosomeType.HIDDEN_LAYER_SIZE:
                {
                    return HIDDEN_LAYER_SIZE_CHROMOSOME_BITS;
                }
                case ChromosomeType.HIDDEN_LAYERS_COUNT:
                {
                    return HIDDEN_LAYERS_COUNT_CHROMOSOME_BITS;
                }
                case ChromosomeType.MAX_ITERATIONS:
                {
                    return MAX_ITERATIONS_CHROMOSOME_BITS;
                }
            }

            return 0;
        }

    }
}
