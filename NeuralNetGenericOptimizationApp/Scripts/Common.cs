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

        public IEnumerable<int> Split(Int32 val, int parts, int minimumValue = 5)
        {
            int left = val;
            for (int i = 0; i < parts - 1; i++)
            {
                var curr = _rand.Next(minimumValue, left / parts);
                yield return curr;
                left -= curr;
            }
            yield return left;
        }

        public static string ConvertPathToR(string CSharpPath)
        {
            string rPath = CSharpPath.Replace('\\', '/');
            return rPath;
        }
    }
}
