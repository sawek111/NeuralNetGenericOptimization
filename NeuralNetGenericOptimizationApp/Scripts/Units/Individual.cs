using NeuralNetGenericOptimizationApp.Scripts.Units;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using NeuralNetGenericOptimizationApp.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm
{
    public class Individual
    {
        private float _fitness = 0.0f;
        private float _time = 0.0f;

        private Dictionary<ChromosomeType, Chromosome> _chromosomes = new Dictionary<ChromosomeType, Chromosome>();

        /// <summary>
        /// Create random individual
        /// </summary>
        public Individual()
        {
            CreateChromosomes();
            return;
        }

        public Individual(Individual copy, ChromosomeType type, int newValue) : base()
        {
            for(int i = 0; i < Enum.GetNames(typeof(ChromosomeType)).Length; i++)
            {
                _chromosomes[(ChromosomeType)i] = copy._chromosomes[(ChromosomeType)i];
            }
            _chromosomes[type].SetGenes(newValue);
        }

        public static Individual GetBetterIndividual(Individual individual1, Individual individual2)
        {
            return (individual1.GetFitness() > individual2.GetFitness()) ? individual1 : individual2;
        }

        public void CombineParentsChromosomes(Individual father, Individual mother)
        {
            for (int i = 0; i < Enum.GetNames(typeof(ChromosomeType)).Length; i++)
            {
                if (_chromosomes.ContainsKey((ChromosomeType)i))
                {
                    _chromosomes[(ChromosomeType)i] = Chromosome.CrossOverChromosomes(father.GetChromosome((ChromosomeType)i), mother.GetChromosome((ChromosomeType)i));
                    continue;
                }
                throw new Exception("Some chromosomes are not prepared properly");
            }

            return;
        }

        public void CombineParentsChromosomes(Individual[] parents)
        {
            for (int i = 0; i < Enum.GetNames(typeof(ChromosomeType)).Length; i++)
            {
                if (_chromosomes.ContainsKey((ChromosomeType)i))
                {
                    _chromosomes[(ChromosomeType)i] = Chromosome.CrossOverChromosomes(parents.Select(x => x.GetChromosome((ChromosomeType)i)).ToArray());
                    continue;
                }
                throw new Exception("Some chromosomes are not prepared properly");
            }

            return;
        }

        public void CombineParentsChromosomes(Individual father, Individual mother, int points)
        {
            for (int i = 0; i < Enum.GetNames(typeof(ChromosomeType)).Length; i++)
            {
                if (_chromosomes.ContainsKey((ChromosomeType)i))
                {
                    _chromosomes[(ChromosomeType)i] = Chromosome.CrossOverChromosomes(father.GetChromosome((ChromosomeType)i), mother.GetChromosome((ChromosomeType)i), points);
                    continue;
                }
                throw new Exception("Some chromosomes are not prepared properly");
            }
            return;
        }

        public Chromosome GetChromosome(ChromosomeType type)
        {
            return _chromosomes[type];
        }

        /// <summary>
        /// Choose one random chromosome and mutate it
        /// </summary>
        public void Mutate()
        {
            ChromosomeType chosenChromosomeType = (ChromosomeType)Common.Instance.Rand.Next(0, Enum.GetNames(typeof(ChromosomeType)).Length - 1);
            Console.WriteLine("Muate " + chosenChromosomeType.ToString());
            _chromosomes[chosenChromosomeType].Mutate(1);

            return;
        }

        public float GetFitness()
        {
            if(_fitness == 0.0f)
            {
                CountAtributes();
            }

            return _fitness;
        }

        /// <summary>
        /// Time and Fitness
        /// </summary>
        private void CountAtributes()
        {
            double[] result = RManager.rManager.Count();
            _fitness = (float)result[0];
            _time = (float)result[1];

            return;
        }

        /// <summary>
        /// Create chromosomes with random genes
        /// </summary>
        private void CreateChromosomes()
        {
            int chromosomeTypesCount = Enum.GetNames(typeof(ChromosomeType)).Length;
            for(int chromosomTypeNr = 0; chromosomTypeNr < chromosomeTypesCount; chromosomTypeNr++)
            {
                ChromosomeType type = (ChromosomeType)chromosomTypeNr;
                Chromosome newChromosome = new Chromosome(type);
                _chromosomes.Add(type, newChromosome);
            }

            return;
        }
    }
}
