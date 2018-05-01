using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World_Generations
{
    class Driver
    {
        // alph is the array that random letters are pulled from
        public static char[] alph = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'q', 'y', 'z', ' ' };

        static void Main()
        {
            Mutant[] test = Sorting(Creator());
            Console.WriteLine("Sorted");
            Printer(test);
            int c = 0;
            for (int i = 0; i < 10000; i++)
            {
                Mutant[] culled = Sorting(Culling(test));
                Console.WriteLine("\nCulled {0}", i);
                test = culled;
                Printer(culled);
                if (test[test.Length - 1].Word == "hello world")
                    break;
                c++;
            }
            Console.WriteLine("\nIn {0} evolutions", c);
            Console.WriteLine(test[test.Length - 1].Family + "\n" + test[test.Length-1].Word);
            Console.ReadLine();
        }
        // The Creator method makes the first array of random strings, 500 each 11 characters long. The word generation is done by the WordGen method
        public static Mutant[] Creator()
        {
            Mutant[] curGen = new Mutant[500];
            for (int i = 0; i < curGen.Length; i++)
            {
                curGen[i] = new Mutant(WordGen());
                System.Threading.Thread.Sleep(10);
            }
            return curGen;
        }
        // The Evolver method creates a new 500 long array of strings and fills it full of strings modified off of the 5 most fit from the previous generation
        public static Mutant[] Evolver(Mutant[] ml)
        {
            Mutant[] nextGen = new Mutant[500];
            Random rnd = new Random();
            for (int i = 0; i < ml.Length; i++) // Loops for each of the most fit strings
            {
                for (int j = 0; j < 100; j++) // Makes 100 new strings from each
                {
                    var chars = ml[i].Word.ToCharArray();
                    for (int l = 0; l < chars.Length; l++) // Goes through each letter and has a random chance of changing one letter to another
                    {
                        int change = rnd.Next(5);
                        if (change == 0)
                        {
                            chars[l] = alph[rnd.Next(27)];
                        }
                    }
                    String b = new string(chars);
                    int k = j + (100 * (i + 1)) - 100;
                    nextGen[k] = new Mutant(b);
                    nextGen[k].Family = ml[i].Family;
                }
                nextGen[(i + 1) * 100 - 100] = ml[i];
            }
            return nextGen;
        }
        //The WordGen method makes a char array, 11 long, and fills it full of random characters, returns it as a string
        public static string WordGen()
        {
            char[] s = new char[11];
            Random rnd = new Random();
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = alph[rnd.Next(27)];
            }
            String b = new string(s);
            return b;
        }
        // The Printer method takes a Mutant array and prints it to the console. Could be done with constructors
        public static void Printer(Mutant[] curGen)
        {
            for (int i = 0; i < curGen.Length; i++)
            {
                Console.WriteLine("{0} - {1}", curGen[i].Word, curGen[i].Fitness);
            }
        }
        // The Culling method takes the five most fit strings and sends it to the Evolver method
        public static Mutant[] Culling(Mutant[] curGen)
        {
            Mutant[] nextGen = new Mutant[5];

            for (int i = 0; i < nextGen.Length; i++)
            {
                nextGen[i] = curGen[curGen.Length - 1 - i];
                nextGen[i].Family += "\n" + curGen[curGen.Length - 1 - i].Word;
            }
            return Evolver(Sorting(nextGen));
        }
        // The Sorting method finds the fitness of every element and sorts it according to that
        public static Mutant[] Sorting(Mutant[] curGen)
        {
            for (int i = 0; i < curGen.Length; i++)
            {
                curGen[i].FindFitness();
            }
            var s = new List<Mutant>();
            s.AddRange(curGen);
            s.Sort();
            Mutant[] sortedGen = s.ToArray();
            return sortedGen;
        }
    }
}
