/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using Tools;
using Simulation.Handling;

namespace States {
    public class Movement : State {

        private static float TurnRate = SimHandler.Instance().Parameters.MovementTurnRate;
        
        private static Probability Turn = new Probability(0.01);
        private Quaternion newRotation;

        public Movement(Organism organism, DUpdateState updateState) : base(organism, updateState) { 
            Organism.collider.enabled = true;
        }

        public override string Tag() {
            return "Movement";
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            // Will change with iteration III.
            // On déplace la forme seulement si elle n'est pas en l'air.
            if(Organism.transform.position.y < 1.0F){
                Vector3 newPos = new Vector3(Organism.phenotypeData.Speed * Organism.transform.forward.x, 0, Organism.phenotypeData.Speed * Organism.transform.forward.z);

                // Calculate the distance travelled.
                Organism.Distance += Vector3.Distance(Organism.transform.position, newPos);

                Organism.transform.Translate(newPos, Space.World);
            }
            
            //if(Turn.Test()) {
            //    newRotation = UnityEngine.Random.rotation;
            //    Vector3 v = newRotation.eulerAngles;
            //    v.x = 0;
            //    v.z = 0;
            //    newRotation.eulerAngles = v;
            //}
            //Organism.transform.rotation = Quaternion.Slerp(Organism.transform.rotation, newRotation, Time.deltaTime * TurnRate);
        } 
        #endregion
    }
}
