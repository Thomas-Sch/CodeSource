/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;

namespace Organisms.States {
	public class Teen : AutoState {
		private State inner;

		public Teen(Organism organism, DUpdateState updateState) : base(organism, updateState) {
			inner = new Movement(Organism, null);
		}

		public override string Tag() {
			return "Teen";
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
