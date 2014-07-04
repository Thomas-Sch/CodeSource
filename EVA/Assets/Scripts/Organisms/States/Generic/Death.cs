/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using States;
using Simulation.Handling;

namespace States {
    public class Death : State {
        
        public Death (Organism organism, DUpdateState updateState) : base(organism, updateState){
            Organism.Death = SimHandler.Instance().Step;
            Organism.LogSelf();
            SimHandler.PopulationHandler().Kill(Organism);
        }

        public override string Tag() {
            return "Death";
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            // Nothing to do here.
        }

        #endregion
    }
}
