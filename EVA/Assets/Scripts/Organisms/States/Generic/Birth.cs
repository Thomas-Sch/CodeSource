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
        public Birth (Organism organism, DUpdateState updateState) : base(organism, updateState){
            Organism.Name = (++Organism.NumberOfOrganisms).ToString();
            Organism.Birth = SimHandler.Instance().Step;
        }

        public override string Tag() {
            return "Birth";
        }

        #region implemented abstract members of State

        public override void FixedAction ()
        {
            Organism.Age++;
        }
        #endregion
    }
}

