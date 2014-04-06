using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.BodyParts;
using GeneticCode.Extensions.BodyParts;
using GeneticCode.Extensions.Members;
using GeneticCode.GeneticDataTemplates;
using GeneticCode.Interfaces;
using GeneticCode.Wrappers;

namespace GeneticCode.Organisms
{
    class Test : Organism
    {
        public int age { get; private set; }
        private int lifeExpectancy = 10;

        public Test()
        {
            canReproduce = false;
            age = 0;
            constructGenotype();
        }

        public Test(IMutation mutation) : this()
        {
            mutate(mutation);
        }

        private void constructGenotype()
        {
            DefaultSEVA defaultGenes = new DefaultSEVA();
            Genotype genotype = new Genotype();
            var root = new Cylinder(defaultGenes);
            genotype.setRootElement(root);

            Sphere front = new Sphere(new DefaultSEVA("bob"));
            //front.setGeneticData("position",new Vector3(1, 0, 0));
            front.setGeneticData("position", new WInt(25));
            front.addExtension(new Square(defaultGenes));

            Sphere back = new Sphere(new DefaultSEVA("a"));
            back.setGeneticData("Bienvenue à", new WString("GATTACA"));
            back.setGeneticData("position",new WVector3(-1, 0, 0));


            Plate left = new Plate(new DefaultSEVA("a"));
            left.setGeneticData("position", new WVector3(0, 0, 1));
            left.setGeneticData("rotation", new WVector3(1, 1, 0));

            Plate right = new Plate(defaultGenes);
            right.setGeneticData("position", new WVector3(0, 0, -1));
            right.setGeneticData("rotation", new WVector3(-1, -1, 0));

            back.addExtension(right);
            back.addExtension(left);

            root.addExtension(front);
            root.addExtension(back);

            base.genotype = genotype;
        }

        public override Organism createEmpty()
        {
            var result = new Test();
            result.genotype = new Genotype();
            return result;
        }

        protected override void loop()
        {
            Console.WriteLine("A new organisme is born (" + myCounter + ")");

            while (age < lifeExpectancy)
            {
                if (age > 2)
                    canReproduce = true;

                if (age > lifeExpectancy - 2)
                    canReproduce = false;

                Console.WriteLine(myCounter + " - Moving");

                age++;
            }
            Console.WriteLine(myCounter + " - I'm dying.");
            OnDeath(EventArgs.Empty);
        }
    }
}
