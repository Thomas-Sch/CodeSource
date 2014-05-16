/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;

namespace States {

	/// <summary>
	/// Represent a state for a state machine based on the state design pattern.
	/// </summary>
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
		}

		/// <summary>
		/// Action to do when Unity's FixedUpdate function is triggered.
		/// </summary>
		public void FixedUpdate() {
			FixedAction();
			if(UpdateState != null) {
				UpdateState();
			}
		}

		public abstract string Tag();

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
