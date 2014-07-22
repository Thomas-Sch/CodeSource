/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;

namespace States {
	public class PreAdult : State {
		private State inner;

		public PreAdult(Organism organism, DUpdateState updateState) : base(organism, updateState) {
			inner = new Movement(Organism, null);
		}

		public override string Tag() {
			return "PreAdult";
		}

		#region implemented abstract members of State

		public override void FixedAction ()
		{
			inner.FixedAction();
			Organism.Age++;
		}

		#endregion
	}
}
