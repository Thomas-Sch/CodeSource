/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using Tools;
using Simulation.Handling;

namespace States {
    public class Adult : State {
    private static float OrganismSight = SimHandler.Instance().Parameters.OrganismSight;
        public State inner {get; set;}

        public int NoNewChild {get; set;}

        public Adult(Organism organism, DUpdateState updateState) : base(organism, updateState) {
            inner = new Movement(Organism, MovementToReproduction);
            NoNewChild = 0;
        }

        public override string Tag() {
            return "Adult";
        }

        #region State transition delegates

        /// <summary>
        /// Called directly inside the state.
        /// </summary>
        public void ReproductionToMovement() {
            inner = new Movement(Organism, MovementToReproduction);
        }

        public void MovementToReproduction() {
            Collider[] results = Physics.OverlapSphere(Organism.transform.position, OrganismSight);

            if (results.Length > 1)
            {
                foreach (Collider collider in results)
                {
                    if (collider.CompareTag(SimHandler.Instance().Parameters.OrganismTag))
                    {
                        // Récupération de l'instance de script.s
                        Organism other = collider.gameObject.GetComponent<Organism>();

                        if (other != null
                            && other != Organism 
                            && NoNewChild <= 0 
                            && other.State.Tag() == Organism.State.Tag())
                        {
                            Adult a = (Adult)Organism.State;
                            Adult b = (Adult)other.GetComponent<Organism>().State;

                            a.inner = new Reproduction(Organism, other, null, true);
                            b.inner = new Reproduction(other, Organism, null, false);
                            break;
                        }
                    }
                }
            }
            
        }
        #endregion

        #region implemented abstract members of State
        public override void FixedAction ()
        {
            inner.FixedUpdate();
            Organism.Age++;
            if(NoNewChild > 0) {
                NoNewChild--;
            }
        }
        #endregion
    }
}