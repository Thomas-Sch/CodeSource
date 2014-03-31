using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticCode.BodyParts;
using GeneticCode.Extensions.BodyParts;
using GeneticCode.Extensions.Members;
using GeneticCode.GeneticDataTemplates;
using GeneticCode.Interfaces;

namespace GeneticCode.Organisms
{
    class Test : Organism
    {
        public Test()
        {
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
            genotype.rootElement = new Cylinder(defaultGenes);

            Sphere front = new Sphere(new DefaultSEVA("bob"));
            front.setGeneticData("position",new Vector3(1, 0, 0));
            front.addExtension(new Square(defaultGenes));

            Sphere back = new Sphere(new DefaultSEVA("a"));
            back.setGeneticData("position",new Vector3(-1, 0, 0));

            Plate left = new Plate(new DefaultSEVA("a"));
            left.setGeneticData("position", new Vector3(0, 0, 1));
            left.setGeneticData("rotation", new Vector3(1, 1, 0));

            Plate right = new Plate(defaultGenes);
            right.setGeneticData("position", new Vector3(0, 0, -1));
            right.setGeneticData("rotation", new Vector3(-1, -1, 0));

            back.addExtension(right);
            back.addExtension(left);

            genotype.rootElement.addExtension(front);
            genotype.rootElement.addExtension(back);

            base.genotype = genotype;
        }

        public override Organism createEmptyOrganism()
        {
            var result = new Test();
            result.genotype = new Genotype();
            return result;
        }
    }
}
