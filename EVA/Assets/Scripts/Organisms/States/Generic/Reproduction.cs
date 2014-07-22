/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using GeneticLibrary.Recombination;
using GeneticLibrary;
using Simulation.Handling;

namespace Organisms.States
{
    public class Reproduction : State
    {
        private readonly static int NoNewChildDuration = SimHandler.Instance().Parameters.NoNewChildDuration;

        private Organism Other;

        // Determine who has to create childs.
        private bool IsMother;

        public Reproduction(Organism organism, Organism other, DUpdateState updateState, bool isMother) : base(organism, null) {
            Other = other;
            IsMother = isMother;
        }

        public override string Tag() {
            return "Reproduction";
        }

        /// <summary>
        /// Spawns the children at the given position.
        /// </summary>
        /// <param name="position">Position.</param>
        private void SpawnChildren(Vector3 position, string nameParent1, string nameParent2) {
            RecombinationOutput childrenGenotypes = SimpleReco.getInstance().Recombine(Organism.Genotype,Other.Genotype);
            foreach(Genotype childGenotype in childrenGenotypes) {
                GameObject childInstance = SimHandler.PopulationHandler().SpawnWithRandomRotation(Organism.Prefab(), position);

                if (childInstance != null)
                {
                    Organism child = childInstance.GetComponent<Organism>();
                    if (child == null)
                    {
                        Debug.LogError("No script is attached");
                    }
                    else
                    {
                        child.Genotype = childGenotype;
                        child.ChangePhenotype(childGenotype);
                    }
                    child.transform.position = position;
                    child.NameParent1 = nameParent1;
                    child.NameParent2 = nameParent2;
                }
            }
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            if (IsMother)
            {
                SpawnChildren(SimHandler.Instance().GetRandomPosition(), Organism.Name, Other.Name);
            }

            Organism.NumberOfReproduction++;
            ((Adult)Organism.State).NoNewChild = NoNewChildDuration;
            ((Adult)Organism.State).ReproductionToMovement();
        }
        #endregion
    }
}

