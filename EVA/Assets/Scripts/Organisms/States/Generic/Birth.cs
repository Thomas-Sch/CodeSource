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

		#region implemented abstract members of State
		public override void Action ()
		{
			if(Duration > 0)
				Duration--;
			else {
				UpdateState();
			}
		}

		public override void FixedAction ()
		{
			// Nothing to do here.
		}
		#endregion
	}
}
