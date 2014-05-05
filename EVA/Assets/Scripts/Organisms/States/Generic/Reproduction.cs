using System;
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

		new private DUpdateState UpdateState;

		public Reproduction(Organism organism, Organism other, DUpdateState updateState, bool isMother) : base(organism, null) {
			Other = other;
			IsMother = isMother;

			UpdateState = updateState;
			Organism.collider.enabled = false;
			Organism.rigidbody.collider.enabled = false;
			InnerState = InnerStates.Approach;

			Debug.Log(Organism + " is reproducing with " + Other);
		}

		#region implemented abstract members of State

		public override void Action ()
		{
			// Nothing to do here.
		}

		public override void FixedAction ()
		{
			switch(InnerState) {
			case InnerStates.Approach:
				if(Vector3.Distance(Organism.transform.position, Other.transform.position) > ApproachDistance)
				{
					Organism.transform.localRotation = Quaternion.Slerp(Organism.transform.localRotation, Quaternion.LookRotation(Other.transform.position - Organism.transform.position), 0.02F);
					Organism.transform.position = Vector3.Lerp(Organism.transform.position, Other.transform.position, Time.deltaTime * ApproachRate);
				} else {
					InnerState = InnerStates.Separation;
					OtherReproduction = (Reproduction)((Adult)Other.State).inner;
					((Adult)Organism.State).NoNewChild = NoNewChildDuration;
					Finished = true;

					Debug.Log(Organism + " finished to reproduce");
				}
				break;
				
			case InnerStates.Separation:
				if(OtherReproduction.Finished) {
					Vector3 newPos = Organism.transform.localPosition + -BackDistanceMultiplier * Organism.transform.forward;
					newPos.y = Organism.transform.localPosition.y;
					Organism.transform.localPosition = newPos;
					Organism.transform.localEulerAngles = Organism.transform.localEulerAngles + UnityEngine.Random.Range(MinAngle, MaxAngle) * Vector3.up;

					Organism.collider.enabled = true;
					Organism.rigidbody.collider.enabled = true;

					Debug.Log(Organism + " is ready to move");
                    ((Adult)Organism.State).ReproductionToMovement();

					if(IsMother) {
						Vector3 Position = new Vector3();
						Position.x = (Organism.transform.localPosition.x + Other.transform.localPosition.x) / 2;
						Position.y = SpawnHeight;
						Position.z = (Organism.transform.localPosition.z + Other.transform.localPosition.z) / 2;
						SpawnChildren(Position);
                    }
                }
                break;
			}
		}

		#endregion

		/// <summary>
		/// Spawns the children at the given position.
		/// </summary>
		/// <param name="position">Position.</param>
		private void SpawnChildren(Vector3 position) {
			Genotype[] childrenGenotype = SimpleReco.getInstance().Recombine(Organism.Genotype,Other.Genotype);
			foreach(Genotype childGenotype in childrenGenotype) {
				GameObject childInstance = Organism.Instantiate(Organism.Prefab(), position, Organism.transform.localRotation) as GameObject;
				Organism child = childInstance.GetComponent<Organism>();
				if(child == null) {
					Debug.LogError("No script is attached");
				} else {
                    child.Genotype = childGenotype;
                    child.ModifyPhenotype(childGenotype);
                }
			}
		}
	}
}
