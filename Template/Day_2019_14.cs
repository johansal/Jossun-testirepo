using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_14
    {
        public static string firstPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            List<Reaction> reactions = new List<Reaction>();
            foreach (var line in lines)
            {
                var splittedL = line.Split(" => ");
                Molecule product = new Molecule(splittedL[1]);
                List<Molecule> f = new List<Molecule>();
                foreach (var ssL in splittedL[0].Split(", "))
                {
                    f.Add(new Molecule(ssL));
                }
                reactions.Add(new Reaction(f, product));
            }

            Reaction fuel = reactions.First(r => r.product.name.Equals("FUEL"));
            List<Molecule> needed = fuel.factors;
            List<Molecule> extra = new List<Molecule>();
            bool allFactorsAreOre = false;
            Console.Write("\n");
            while (!allFactorsAreOre)
            {
                allFactorsAreOre = true;
                foreach (var fa in needed)
                {
                    Console.Write(fa.quantity + " " + fa.name + ", ");
                }
                Console.Write("\n");
                List<Molecule> newNeededMolecules = new List<Molecule>();
                foreach (Molecule f in needed)
                {
                    if (!f.name.Equals("ORE"))
                    {
                        Reaction newFactor = reactions.First(r => r.product.name.Equals(f.name));
                        long tempQty = newFactor.product.quantity;
                        foreach (var mol in newFactor.factors)
                        {
                            Console.Write(mol.quantity + "*(" + f.quantity + "/" + tempQty.ToString());
                            if (tempQty > f.quantity)
                            {
                                tempQty = f.quantity; //extra not used
                            }
                            Molecule newMol = new Molecule(mol.name, (long)Math.Ceiling((decimal)f.quantity / (decimal)tempQty) * mol.quantity);
                            Console.WriteLine(")=" + newMol.quantity);
                            bool added = false;
                            foreach (var m in newNeededMolecules)
                            {
                                if (m.name.Equals(newMol.name))
                                {
                                    m.quantity += newMol.quantity;
                                    added = true;
                                }
                            }
                            if (!added)
                                newNeededMolecules.Add(newMol);
                        }
                        allFactorsAreOre = false;
                    }
                    else
                    {
                        newNeededMolecules.Add(f);
                    }
                }

                needed = newNeededMolecules;

            }
            return needed.Sum(molecule => molecule.quantity).ToString();
        }

        public static string secondPuzzle(string location)
        {
            return location;
        }
    }

    public class Molecule
    {
        public string name;
        public long quantity;
        public Molecule()
        {
            this.name = "";
            this.quantity = 0;
        }
        public Molecule(string n, long q)
        {
            this.name = n;
            this.quantity = q;
        }
        public Molecule(string s)
        {
            this.name = s.Split(" ")[1];
            this.quantity = long.Parse(s.Split(" ")[0]);
        }
    }
    public class Reaction
    {
        public List<Molecule> factors;
        public Molecule product;
        public Reaction()
        {
            this.factors = new List<Molecule>();
            this.product = new Molecule();
        }
        public Reaction(List<Molecule> f, Molecule p)
        {
            this.factors = f;
            this.product = p;
        }
    }
}
