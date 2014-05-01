using UnityEngine;
using System.Collections;
using States;
using Tools;

namespace States {
	public class Movement : State {

		private static float TurnRate = 0.5f;
		private Quaternion newRotation;

		public Movement(Organism organism, DUpdateState updateState) : base(organism, updateState) { }

		#region implemented abstract members of State
		public override void Action ()
		{
			// On déplace la forme seulement si elle n'est pas en l'air.
			if(Organism.transform.position.y < 1.0F){
				Vector3 newPos = new Vector3(Organism.phenotypeData.Speed * Organism.transform.forward.x, 0, Organism.phenotypeData.Speed * Organism.transform.forward.z);
				Organism.transform.Translate(newPos, Space.World);
			}
			
			if(Probability.Test(0.01)) {
				newRotation = UnityEngine.Random.rotation;
				Vector3 v = newRotation.eulerAngles;
				v.x = 0;
				v.z = 0;
				newRotation.eulerAngles = v;

	    	}
			Organism.transform.rotation = Quaternion.Slerp(Organism.transform.rotation, newRotation, Time.deltaTime * TurnRate);
		}

		public override void FixedAction ()
		{
			// Nothing
		}
		#endregion
	}
}
