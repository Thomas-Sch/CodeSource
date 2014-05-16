/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using System;
using UnityEngine;

namespace States
{
	public class Birth : State
	{
		private int Duration = Simulation.BirhtDuration;

		new private DUpdateState UpdateState;

		public Birth (Organism organism, DUpdateState updateState) : base(organism, null){
			Organism.Age = 0;
			UpdateState = updateState;
			Debug.Log(Organism + " is born.");
		}

		public override string Tag() {
			return "Birth";
		}

		#region implemented abstract members of State

		public override void FixedAction ()
		{
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

