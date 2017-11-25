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
            foreach (ChromosomeType chromosomeType in chromosomes.Keys)
            {
                chromosomes[chromosomeType] = Chromosome.CrossOverChromosomes(father.GetChromosome(chromosomeType), mother.GetChromosome(chromosomeType));
            }

            return;
        }

        public void CombineParentsChromosomes(Individual[] parents)
        {
            foreach (ChromosomeType chromosomeType in chromosomes.Keys)
            {
                chromosomes[chromosomeType] = Chromosome.CrossOverChromosomes(parents.Select(x => x.GetChromosome(chromosomeType)).ToArray());
            }
            return;
        }

        public void CombineParentsChromosomes(Individual father, Individual mother, int points)
        {
            foreach (ChromosomeType chromosomeType in chromosomes.Keys)
            {
                chromosomes[chromosomeType] = Chromosome.CrossOverChromosomes(father.GetChromosome(chromosomeType), mother.GetChromosome(chromosomeType), points);
            }
            return;
        }

        public Chromosome GetChromosome(ChromosomeType type)
        {
            return chromosomes[type];
        }

        /// <summary>
        /// Choose one random chromosome and mutate it
        /// </summary>
        public void Mutate()
        {
            ChromosomeType chosenChromosomeType = (ChromosomeType)Common.Instance.Rand.Next(0, Enum.GetNames(typeof(ChromosomeType)).Length - 1);
            Console.WriteLine("Muate " + chosenChromosomeType.ToString());
            chromosomes[chosenChromosomeType].Mutate(1);

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
            int chromosomeTypesCount = Enum.GetNames(typeof(ChromosomeType)).Length -1;
            for(int chromosomTypeNr = 0; chromosomTypeNr < chromosomeTypesCount; chromosomTypeNr++)
            {
                ChromosomeType type = (ChromosomeType)chromosomTypeNr;
                Chromosome newChromosome = new Chromosome(type);
                chromosomes.Add(type, newChromosome);
            }

            return;
        }
    }
}
