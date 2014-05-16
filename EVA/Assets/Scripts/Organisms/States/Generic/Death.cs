/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using States;

namespace States {
	public class Death : State {

		private readonly float TimeBeforeRemove = Simulation.TimeBeforeRemove;
		private readonly float Smooth = Simulation.DeathSmooth;
		
		public Death (Organism organism, DUpdateState updateState) : base(organism, updateState){
			Debug.Log(Organism + " is dead");
			Organism.collider.enabled = false;
			Organism.Invoke("Kill", TimeBeforeRemove);
		}

		public override string Tag() {
			return "Death";
		}

		#region implemented abstract members of State

		public override void Action ()
		{
		}

		public override void FixedAction ()
		{
			// Nothing to do here.
		}

		#endregion
	}
}
