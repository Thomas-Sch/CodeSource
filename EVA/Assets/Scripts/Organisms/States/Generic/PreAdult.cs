using UnityEngine;

namespace States {
	public class PreAdult : State {
		private State inner;

		public PreAdult(Organism organism, DUpdateState updateState) : base(organism, updateState) {
			inner = new Movement(Organism, UpdateState);
			Debug.Log(Organism + " is pre adult");
		}

		#region implemented abstract members of State
		public override void Action ()
		{
			inner.Action();
			Organism.Age++;
		}

		public override void FixedAction ()
		{
			inner.FixedAction();
		}

		#endregion
	}
}
