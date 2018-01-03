using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.Utils
{
    public enum ChromosomeType
    {
        HIDDEN_LAYER_SIZE,
        DECAY,
        MAX_ITERATIONS
    }

    public enum SelectionType
    {
        Roulette,
        Tournament,
        Rank
    }

    public enum CrossingType
    {
        SinglePoint,
        MultiPoints,
        MultiParenting
    }
}
