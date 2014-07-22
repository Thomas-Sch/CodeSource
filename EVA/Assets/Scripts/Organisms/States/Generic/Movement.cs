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
            Organism.Distance += Vector3.Distance(last, Organism.transform.position);
            last = Organism.transform.position;
        } 
        #endregion
    }
}
