using NeuralNetGenericOptimizationApp.Scripts.Units;
using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.GeneticAlghoritm
{
    public class Individual
    {
        private float fitness = 0.0f;
        private Dictionary<ChromosomeType, Chromosome> chromosomes = new Dictionary<ChromosomeType, Chromosome>();

        /// <summary>
        /// Create random individual
        /// </summary>
        public Individual()
        {
            CreateChromosomes();
            return;
        }

        /// <summary>
        /// Create from parents with chromosomes crossing
        /// </summary>
        public Individual(Individual father, Individual mother)
        {
            CreateChromosomes();

            return;
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
            Console.WriteLine("ERROR TODO");
            if(fitness == 0.0f)
            {
            //    fitness =  Math
            }

            return 0.0f;
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
