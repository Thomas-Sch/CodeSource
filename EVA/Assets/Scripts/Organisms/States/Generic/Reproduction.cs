/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using GeneticLibrary.Recombination;
using GeneticLibrary;

namespace States
{
	public class Reproduction : State
	{
		private readonly static  float ApproachRate = Simulation.ApproachRate;
		private readonly static int NoNewChildDuration = Simulation.NoNewChildDuration;

		private readonly static float ApproachDistance = 2f;
        private readonly static float BackDistanceMultiplier = 1.5f;
		private readonly static int MinAngle = 150;
		private readonly static int MaxAngle = 190;
		private readonly static float SpawnHeight = 3f;

		public enum InnerStates {Approach, Separation};
		public InnerStates InnerState {get; set;}

		private Organism Other;
		private Reproduction OtherReproduction;

		// Determine if the approach is finished.
		public bool Finished;

		// Determine who has to create childs.
		private bool IsMother;

		public Reproduction(Organism organism, Organism other, DUpdateState updateState, bool isMother) : base(organism, null) {
			Other = other;
			IsMother = isMother;
			Organism.collider.enabled = false;
			Organism.rigidbody.collider.enabled = false;
			InnerState = InnerStates.Approach;

			Debug.Log(Organism + " is reproducing with " + Other);
		}

		public override string Tag() {
			return "Reproduction";
		}

		/// <summary>
		/// Spawns the children at the given position.
		/// </summary>
		/// <param name="position">Position.</param>
		private void SpawnChildren(Vector3 position) {
			RecombinationOutput childrenGenotypes = SimpleReco.getInstance().Recombine(Organism.Genotype,Other.Genotype);
			foreach(Genotype childGenotype in childrenGenotypes) {
				GameObject childInstance = Organism.Instantiate(Organism.Prefab(), position, Organism.transform.localRotation) as GameObject;
				Organism child = childInstance.GetComponent<Organism>();
				if(child == null) {
					Debug.LogError("No script is attached");
				} else {
					child.Genotype = childGenotype;
					child.ChangePhenotype(childGenotype);
				}
				child.transform.position = position;
			}
		}

		#region implemented abstract members of State

		public override void FixedAction ()
		{
			switch(InnerState) {
			case InnerStates.Approach:
				if(Other != null && Vector3.Distance(Organism.transform.position, Other.transform.position) > ApproachDistance)
				{
					Organism.transform.localRotation = Quaternion.Slerp(Organism.transform.localRotation, Quaternion.LookRotation(Other.transform.position - Organism.transform.position), 0.02F);
					Organism.transform.position = Vector3.Lerp(Organism.transform.position, Other.transform.position, Time.deltaTime * ApproachRate);
				} else if(Other.State.Tag() == Organism.State.Tag() && ((Adult)Other.State).inner.Tag() == Tag()){
					InnerState = InnerStates.Separation;
					OtherReproduction = (Reproduction)((Adult)Other.State).inner;
					((Adult)Organism.State).NoNewChild = NoNewChildDuration;
					Finished = true;

					Debug.Log(Organism + " finished to reproduce");
				}
				break;
				
			case InnerStates.Separation:

				// If the reproduction partner is null it means he's not available anymore. (Dead probably).
				if(OtherReproduction != null && OtherReproduction.Finished) {

					// Calculate the position of child.
					Vector3 ChildPosition = new Vector3();
					if(IsMother) {
						ChildPosition.x = (Organism.transform.position.x + Other.transform.position.x) / 2;
						ChildPosition.y = SpawnHeight;
						ChildPosition.z = (Organism.transform.position.z + Other.transform.position.z) / 2;
					}

					// New position of the parent.
					Vector3 SeparationPosition = Organism.transform.localPosition + -BackDistanceMultiplier * Organism.transform.forward;
					SeparationPosition.y = Organism.transform.localPosition.y;
					Organism.transform.localPosition = SeparationPosition;
					Organism.transform.localEulerAngles = Organism.transform.localEulerAngles + UnityEngine.Random.Range(MinAngle, MaxAngle) * Vector3.up;
	
					Debug.Log(Organism + " is ready to move");

                    ((Adult)Organism.State).ReproductionToMovement();

					if(IsMother) {
						SpawnChildren(ChildPosition);
                    }
                } else {
					((Adult)Organism.State).ReproductionToMovement();
				}
                break;
			}
		}
		#endregion
	}
}

