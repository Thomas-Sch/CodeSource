/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using Tools;
using Simulation.Handling;

namespace Organisms.States
{
    public class Movement : AutoState {

        private float strength = 100.0f;
        public GameObject motor;
        private Vector3 last;
        private Quaternion newRotation;

        public Movement(Organism organism, DUpdateState updateState) : base(organism, updateState) { 
            Organism.collider.enabled = true;
            motor = organism.motor;
            last = Organism.transform.position;
        }

        public override string Tag() {
            return "Movement";
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            motor.rigidbody.AddTorque(-Organism.gameObject.transform.forward * strength * Time.deltaTime, ForceMode.Impulse);
            float distance = Vector3.Distance(last, Organism.transform.position);

            // We are not counting the organisms that are flying by beeing ejected by the physic engine.
            if (distance < 10)
            {
                Organism.Distance += distance;
            }
            last = Organism.transform.position;
        } 
        #endregion
    }
}
