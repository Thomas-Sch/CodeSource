using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticLibrary.BodyParts;
using GeneticLibrary.Extensions.BodyParts;
using GeneticLibrary.Extensions.Members;
using GeneticLibrary.GeneticDataTemplates;
using GeneticLibrary.Interfaces;
using GeneticLibrary.Wrappers;

namespace GeneticLibrary.Organisms
{
    class Test : Organism
    {
        public int age { get; private set; }

        public Test()
        {
            CanReproduce = false;
            age = 0;
            ConstructGenotype();
        }

        public Test(IMutation mutation) : this()
        {
            Mutate(mutation);
        }

        private void ConstructGenotype()
        {
            DefaultSEVA defaultGenes = new DefaultSEVA();
            Genotype genotype = new Genotype();
            var root = new Cylinder(defaultGenes);
            genotype.RootElement = root;

            Sphere front = new Sphere(new DefaultSEVA("bob"));
            //front.setGeneticData("position",new Vector3(1, 0, 0));
            front.SetGeneticData("position", new WInt(25));
            front.AddExtension(new Square(defaultGenes));

            Sphere back = new Sphere(new DefaultSEVA("a"));
            back.SetGeneticData("Bienvenue à", new WString("GATTACA"));
            back.SetGeneticData("position",new WVector3(-1, 0, 0));


            Plate left = new Plate(new DefaultSEVA("a"));
            left.SetGeneticData("position", new WVector3(0, 0, 1));
            left.SetGeneticData("rotation", new WVector3(1, 1, 0));

            Plate right = new Plate(defaultGenes);
            right.SetGeneticData("position", new WVector3(0, 0, -1));
            right.SetGeneticData("rotation", new WVector3(-1, -1, 0));

            back.AddExtension(right);
            back.AddExtension(left);

            root.AddExtension(front);
            root.AddExtension(back);

            base.Genotype = genotype;
        }

        public override Organism CreateEmpty()
        {
            var result = new Test();
            result.Genotype = new Genotype();
            return result;
        }
    }
}
