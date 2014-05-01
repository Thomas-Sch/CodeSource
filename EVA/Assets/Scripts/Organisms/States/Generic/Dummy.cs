using UnityEngine;
using States;

public class Dummy : State {

	public Dummy(Organism organism, DUpdateState updateState) : base(organism, updateState){}

	#region implemented abstract members of State
	public override void Action ()
	{
		Organism.transform.Rotate(Random.rotation.eulerAngles);
	}
	public override void FixedAction ()
	{
		if(Organism.rigidbody.velocity.y == 0f) {
			Organism.rigidbody.AddForce(new Vector3(0,500,0), ForceMode.Impulse);
		}
	}
	#endregion
}
