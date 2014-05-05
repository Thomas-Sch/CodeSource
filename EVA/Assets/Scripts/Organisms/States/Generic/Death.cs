using UnityEngine;
using States;

namespace States {
	public class Death : State {

		private readonly float TimeBeforeRemove = Simulation.TimeBeforeRemove;
		private readonly float Smooth = Simulation.DeathSmooth;

		private readonly float DeathSize = 0.01f;
		
		public Death (Organism organism, DUpdateState updateState) : base(organism, updateState){
			Debug.Log(Organism + " is dead");
			Organism.collider.enabled = false;
			Organism.Invoke("Kill", TimeBeforeRemove);
		}

		#region implemented abstract members of State

		public override void Action ()
		{
			// Temporaire.
			Organism.gameObject.transform.localScale = Vector3.Lerp(Organism.gameObject.transform.localScale, new Vector3(DeathSize, DeathSize, DeathSize), Smooth);
		}

		public override void FixedAction ()
		{
			// Nothing to do here.
		}

		#endregion
	}
}
