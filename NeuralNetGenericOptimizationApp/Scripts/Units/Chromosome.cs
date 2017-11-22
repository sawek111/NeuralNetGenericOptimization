using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.Units
{
    public class Chromosome
    {
        private byte[] _genes;
        private ChromosomeType _type;

        /// <summary>
        /// Initiate chromosome with random genes' values
        /// </summary>
        public Chromosome(ChromosomeType type)
        {
            _genes = new byte[Settings.GetChromosomeSize(type)];
            _type = type;
            Random();

            return;
        }

        /// <summary>
        /// Genes amount
        /// </summary>
        public int Length
        {
            get { return _genes.Length; }
        }

        /// <summary>
        /// Indexer (to use Chromosome[i] instead  Chromosome._genes[i])
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public byte this[int i]
        {
            get { return _genes[i]; }
            set { _genes[i] = value; }
        }

        public static Chromosome CrossOverChromosomes(Chromosome fatherChromosome, Chromosome motherChromosome)
        {
            int firstMotherGeneNr = 0;
            ValidateParents(fatherChromosome, motherChromosome);
            Chromosome childChromosome = new Chromosome(fatherChromosome._type);
            int fatherPoolSize = (int)(childChromosome.Length * Common.Instance.Rand.NextDouble());
            for (int i = 0;  i < fatherPoolSize; i++)
            {
                childChromosome[i] = fatherChromosome[i];
            }
            firstMotherGeneNr = (fatherPoolSize < childChromosome.Length - 1) ? fatherPoolSize : fatherPoolSize - 1;
            for ( int i = firstMotherGeneNr; i < childChromosome.Length; i++ )
            {
                childChromosome[i] = motherChromosome[i];
            }

            return childChromosome;
        }

        /// <summary>
        /// Assign random values to genes
        /// </summary>
        public void Random()
        {
            for(int i = 0; i < _genes.Length; i++)
            {
                int value = Common.Instance.Rand.Next(0, 1);
                _genes[i] = (byte)value;
                Console.WriteLine(value + " " + _genes[i]);
            }

            return;
        }

        public void Mutate(int genesCount)
        {
            for(int i = 0; i < genesCount; i++)
            {
                int geneNumber = Common.Instance.Rand.Next(0, _genes.Length-1);
                MutateGene(ref _genes[geneNumber]);
            }

            return;
        }

        /// <summary>
        /// Check if parents are properly chosen
        /// </summary>
        private static void ValidateParents(Chromosome fatherChromosome, Chromosome motherChromosome)
        {
            if (fatherChromosome._type != motherChromosome._type)
            {
                throw new Exception("You are crossing different chromosomes");
            }
            if (fatherChromosome.Length != motherChromosome.Length)
            {
                throw new Exception("Chromosomes have different length, sth went wrong, assure You have assigned chromosome const values (based on type");
            }

            return;
        }

        private static byte CrossGene(byte fatherGene, byte motherGene)
        {
            if((fatherGene + motherGene) == 1)
            {
                return 1;
            }

            return 0;
        }

        private void MutateGene(ref byte gene)
        {
            gene = (gene == 0) ? (byte)1 : (byte)0;
            return;
        }
    }
}
