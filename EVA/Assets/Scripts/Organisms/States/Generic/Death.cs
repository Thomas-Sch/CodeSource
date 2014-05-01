using UnityEngine;
using States;

namespace States {
	public class Death : State {
		
		public Death (Organism organism, DUpdateState updateState) : base(organism, updateState){
			Debug.Log(Organism + " is dead");
			Organism.Invoke("Kill", 4f);
		}

		#region implemented abstract members of State

		public override void Action ()
		{
			// Temporaire.
			Organism.gameObject.transform.localScale = Vector3.Lerp(Organism.gameObject.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), 0.01f);
		}

		public override void FixedAction ()
		{

		}

		#endregion
	}
}
