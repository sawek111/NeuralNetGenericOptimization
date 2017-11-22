using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.Utils
{
    class Common
    {
        public const int SEED = 5;

        public static Common Instance = new Common();

        private Random _rand = null;

        public Random Rand
        {
            get { return _rand; }
        }

        private Common()
        {
            _rand = new Random(SEED);
            return;
        }

    }
}
