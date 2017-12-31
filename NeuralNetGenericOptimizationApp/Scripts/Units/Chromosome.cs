using NeuralNetGenericOptimizationApp.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetGenericOptimizationApp.Scripts.Units
{
    public struct Chromosome
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

        public void SetGenes(int decValue)
        {
            string binary = Convert.ToString(decValue, 2);
            int length = (binary.Length < _genes.Length) ? binary.Length : _genes.Length;
            for (int i = 1; i <= length; i++)
            {
                _genes[_genes.Length - i] = byte.Parse(binary[binary.Length - i].ToString()) ;
            }
            PreventZeroValueGene();

            return;
        }

        public int GetGeneDecimalValue()
        {
            string valueString = string.Join("", _genes);
            int value = Convert.ToInt32(valueString, 2);
            Console.WriteLine(_genes + " value: " + value);

            return value;
        }

        /// <summary>
        /// Get gene value in range 0,1
        /// </summary>
        /// <returns></returns>
        public float GetGeneFlotValue()
        {
            string valueString = string.Join("", _genes);
            int value = Convert.ToInt32(valueString, 2);
            float dividedValue = (float)value / (float)Math.Pow(2, _genes.Length);
            Console.WriteLine(_genes + " value: " + value);

            return dividedValue;
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
        /// Multi parent crossover
        /// </summary>
        /// <param name="parentsChromosomes"></param>
        /// <returns></returns>
        public static Chromosome CrossOverChromosomes(Chromosome[] parentsChromosomes)
        {
            ValidateParents(parentsChromosomes);
            Chromosome childChromosome = new Chromosome(parentsChromosomes[0]._type);
            int poolSize = (int)(childChromosome.Length / parentsChromosomes.Length); //each parent give the same amount of chromosomes
            int noOfParent = 0; //number of parent
            for (int i = 0; i < childChromosome.Length; i++)
            {
                if (i != 0)
                    if ((i % poolSize == 0) && (noOfParent < parentsChromosomes.Length - 1)) //change of parent
                    {
                        noOfParent++;
                    }
                childChromosome[i] = parentsChromosomes[noOfParent][i];               
            }

            return childChromosome;
        }


        /// <summary>
        /// Multi parent crossover (random amount of parent chromosomes)
        /// </summary>
        /// <param name="parentsChromosomes"></param>
        /// <returns></returns>
        public static Chromosome CrossOverChromosomesR(Chromosome[] parentsChromosomes)
        {
            ValidateParents(parentsChromosomes);
            Chromosome childChromosome = new Chromosome(parentsChromosomes[0]._type);
            int[] poolSizes = Common.Instance.Split(childChromosome.Length, parentsChromosomes.Length).ToArray(); //each parent give different amount of chromosomes
            int noOfParent = 0; //number of parent
            for (int i = 0; i < childChromosome.Length; i++)
            {
                if (i != 0)
                    if ((i % poolSizes[noOfParent] == 0) && (noOfParent < parentsChromosomes.Length - 1)) //change of parent
                    {
                        noOfParent++;
                    }
                childChromosome[i] = parentsChromosomes[noOfParent][i];
            }

            return childChromosome;
        }


        /// <summary>
        /// Multipoint crossover
        /// </summary>
        /// <param name="fatherChromosome"></param>
        /// <param name="motherChromosome"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Chromosome CrossOverChromosomes(Chromosome fatherChromosome, Chromosome motherChromosome, int points)
        {
            ValidateParents(fatherChromosome, motherChromosome);
            Chromosome childChromosome = new Chromosome(fatherChromosome._type);
            int[] poolSizes = Common.Instance.Split(childChromosome.Length, points).ToArray(); //each parent give different amount of chromosomes
            int chosenParent = 0;
            int counter = 0; //needed to change poolsize when eqauls to poolSize[index]
            int poolSizeNo = 0; //number of poolSize
            for (int i = 0; i < childChromosome.Length; i++)
            {
                if (counter == poolSizes[poolSizeNo])
                {             
                    if (chosenParent == 0) //father changed to mother
                        chosenParent = 1;
                    else
                        chosenParent = 0;

                    counter = 0;
                    poolSizeNo++;
                }
                //which parent chosed
                if (chosenParent == 0)
                    childChromosome[i] = fatherChromosome[i];
                else
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
                int value = Common.Instance.Rand.Next(0, 2);
                _genes[i] = (byte)value;
            }
            PreventZeroValueGene();

            return;
        }

        public void Mutate(int genesCount)
        {
            for(int i = 0; i < genesCount; i++)
            {
                int geneNumber = Common.Instance.Rand.Next(0, _genes.Length-1);
                MutateGene(ref _genes[geneNumber]);
            }
            PreventZeroValueGene();

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

        /// <summary>
        /// Check if all parents are properly chosen
        /// </summary>
        private static void ValidateParents(Chromosome[] parents)
        {         
            var type = parents.First()._type;
            if(!parents.All(x => x._type == type))
            {
                throw new Exception("You are crossing different chromosomes");
            }

            var length = parents.First().Length;
            if (!parents.All(x => x.Length == length))
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

        /// <summary>
        /// Set 1 value for zero value genes
        /// </summary>
        private void PreventZeroValueGene()
        {
            for(int i = 0; i < _genes.Length; i++)
            {
                if(_genes[i] != 0)
                {
                    return;
                }
            }
            _genes[_genes.Length - 1] = 1;

            return;
        }

        private void MutateGene(ref byte gene)
        {
            gene = (gene == 0) ? (byte)1 : (byte)0;
            return;
        }
    }
}
