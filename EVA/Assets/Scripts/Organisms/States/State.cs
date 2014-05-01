using UnityEngine;
using System.Collections;

namespace States {

	public abstract class State {

		public Organism Organism {get; set;}
		public delegate void DUpdateState();

		protected DUpdateState UpdateState;

		public State(Organism organism, DUpdateState checkAndUpdateState) {
			Organism = organism;
			UpdateState = checkAndUpdateState;
		}

		/// <summary>
		/// Action to do when Unity's Update function is triggered.
		/// </summary>
		public void Update() {
			Action();
			if(UpdateState != null) {
				UpdateState();
			}
		}

		/// <summary>
		/// Action to do when Unity's FixedUpdate function is triggered.
		/// </summary>
		public void FixedUpdate() {
			FixedAction();
		}

		public abstract void Action();

		public abstract void FixedAction();
	}
}
