using System;
using UnityEngine;
namespace States
{
	public class Birth : State
	{
		public Birth (Organism organism, DUpdateState updateState) : base(organism, updateState){
			Organism.Age = 0;
			Debug.Log(Organism + " is born.");
		}

		#region implemented abstract members of State
		public override void Action ()
		{

		}

		public override void FixedAction ()
		{

		}
		#endregion
	}
}

