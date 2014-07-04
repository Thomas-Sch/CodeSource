/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using System;
using Simulation.Handling;
using UnityEngine;

namespace States
{
    public class Birth : State
    {
        private int Duration = SimHandler.Instance().Parameters.BirthDuration;

        new private DUpdateState UpdateState;

        public Birth (Organism organism, DUpdateState updateState) : base(organism, null){
            Organism.Name = (++Organism.NumberOfOrganisms).ToString();
            Organism.Birth = SimHandler.Instance().Step;

            UpdateState = updateState;
        }

        public override string Tag() {
            return "Birth";
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            Organism.Age++;
            // Nothing to do here.
            if(Duration > 0)
                Duration--;
            else {
                UpdateState();
            }
        }
        #endregion
    }
}

