using System;
using System.Linq;
using System.Collections;

public class Mutant : IComparable
{
    private string word;
    private string family;
    private int fitness;
    // No argument. I don't know if this is ever used, except during my debugging
    public Mutant()
    {
        word = "Goodbye Earth";
        family = "";
        fitness = 0;
    }
    // Single string argument, assigns it to the word
    public Mutant(string w)
    {
        word = w;
        family = "";
        fitness = 0;
    }
    // I don't know if this is used either
    public Mutant(string w, string f)
    {
        word = w;
        family = f;
        fitness = 0;
    }

    public string Word
    {
        set
        { word = value; }
        get
        { return word; }
    }

    public string Family
    {
        set
        { family = value; }
        get
        { return family; }
    }

    public int Fitness
    {
        set
        { fitness = value; }
        get
        { return fitness; }
    }
    // Finds the fitness based off of the number of letters that are in the correct spot
    public void FindFitness()
    {
        fitness = 0;
        var chars = word.ToCharArray();
        char[] hi = new char[] { 'h', 'e', 'l', 'l', 'o', ' ', 'w', 'o', 'r', 'l', 'd' };
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == hi[i])
                fitness += 1;
        }
    }
    // This allows me to sort the Mutant lists by telling it to compare the fitness scores
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Mutant otherMutant)
            return this.fitness.CompareTo(otherMutant.fitness);
        else
            throw new ArgumentException("Object is not a Mutant");
    }
}
