using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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


        /// <summary>
        /// Loads all values with given type separated with comma (,)
        /// </summary>
        /// <param name="textbox"> Textbox to read values from</param>
        /// <returns>Array with readed values </returns>
        public static T[] LoadValuesArrayFromTextbox<T>(TextBox textbox)
        {
            string[] words = textbox.Text.Split(',');
            List<T> wordsList = new List<T>();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] is T)
                {
                    wordsList.Add((T)Convert.ChangeType(words[i], typeof(T)));
                }
            }

            return wordsList.ToArray();
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
