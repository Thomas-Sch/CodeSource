using UnityEngine;
using System.Collections;

namespace States {

	public abstract class State {

		/// <summary>
		/// Gets or sets the organism.
		/// </summary>
		/// <value>The organism.</value>
		public Organism Organism {get; set;}

		// Define the method signature for updating.
		public delegate void DUpdateState();

		// Automaticaly called at the end of an update.
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

		/// <summary>
		/// Action to do during the update.
		/// </summary>
		public abstract void Action();

		/// <summary>
		/// Action to do during the fixed update.
		/// </summary>
		public abstract void FixedAction();
	}
}
