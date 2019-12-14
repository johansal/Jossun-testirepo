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
            //Console.Write("\n");
            bool hasOnlyOre = false;
            while (!hasOnlyOre)
            {
                hasOnlyOre = true;
                /*foreach (var fa in needed)
                {
                    Console.Write(fa.quantity + " " + fa.name + ", ");
                }
                Console.Write("\n");*/
                List<Molecule> newNeededMolecules = new List<Molecule>();
                foreach (Molecule n in needed)
                {
                    //Check if we have extra
                    int extraInd = extra.FindIndex(m => m.name.Equals(n.name));
                    if(extraInd >= 0) {
                        if (extra[extraInd].quantity >= n.quantity) {
                            extra[extraInd].quantity -= n.quantity;
                            continue;
                        }
                        else {
                            n.quantity -= extra[extraInd].quantity;
                            extra[extraInd].quantity = 0;

                        }
                    } 


                    if (!n.name.Equals("ORE"))
                    {
                        Reaction reactionForNeededMolecule = reactions.First(r => r.product.name.Equals(n.name));
                        long producedQty = reactionForNeededMolecule.product.quantity;
                        long productionRepeat = (long)Math.Ceiling((decimal)n.quantity / (decimal)producedQty);
                        foreach (var mol in reactionForNeededMolecule.factors)
                        {
                            //Console.Write(mol.quantity + "*(" + n.quantity + "/" + producedQty.ToString());
                            if (producedQty * productionRepeat > n.quantity)
                            {
                                int i = extra.FindIndex(m => m.name.Equals(n.name));
                                if (i >= 0)
                                {
                                    extra[i].quantity = producedQty * productionRepeat - n.quantity + extra[i].quantity; 
                                }
                                else
                                {
                                    Molecule extraM = new Molecule(n.name, producedQty * productionRepeat - n.quantity);
                                    extra.Add(extraM);
                                }

                            }
                            Molecule newMol = new Molecule(mol.name, productionRepeat * mol.quantity);
                            //Console.WriteLine(")=" + newMol.quantity);
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
                        hasOnlyOre = false;
                    }
                    else
                    {
                        newNeededMolecules.Add(n);
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
