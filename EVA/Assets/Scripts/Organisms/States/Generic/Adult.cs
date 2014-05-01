using UnityEngine;
using System.Collections;

namespace States {
	public class Adult : State {
		private static float SeeingDistance = 10F;
		State inner;

		public Adult(Organism organism, DUpdateState updateState) : base(organism, updateState) {
			Debug.Log(Organism + " is adult");
			inner = new Movement(Organism, MovementToReproduction);
		}

		public void MovementToReproduction() {
			RaycastHit hit;
			var rayDirection = Organism.gameObject.transform.forward;
			Debug.DrawRay(Organism.gameObject.transform.position, rayDirection * SeeingDistance);
			Physics.Raycast(Organism.gameObject.transform.position, rayDirection, out hit, SeeingDistance);
			
			if(hit.collider != null && hit.collider.CompareTag("Organism")) {

				// Récupération de l'instance de script.s
				Organism other = hit.collider.gameObject.GetComponent<Organism>();

				// TODO : Try to don't use IsInstanceOfType()...
				if(other != null && GetType().IsInstanceOfType(other.State)) {
					((Adult) Organism.State).inner = new Reproduction(Organism, null);
					((Adult) other.GetComponent<Organism>().State).inner = new Reproduction(other.GetComponent<Organism>(), null);
					//					other.Reproduce(gameObject);
					//					Reproduce(other.gameObject);
					//
					//					Genotype[] childrenGenotype = SimpleReco.getInstance().Recombine(Genotype,other.Genotype);
					//					foreach(Genotype child in childrenGenotype) {
					//						Vector3 position = new Vector3(transform.forward.x * -0.5f, transform.position.y, transform.forward.z * -0.5f);
					//
					//						GameObject childInstance = Instantiate(Prefab, position, transform.localRotation) as GameObject;
					//						childInstance.SetActive(false);
					//						A phenotype = childInstance.GetComponent<A>();
                    //						if(phenotype == null) {
                    //							Debug.LogError("No script is attached");
                    //						} else {
                    //							phenotype.Genotype = child;
                    //							phenotype.ModifyPhenotype(phenotype.Genotype);
					//						}
					//						childInstance.SetActive(true);
					//					}
				}
			}
		}

		#region implemented abstract members of State
		public override void Action ()
		{
			inner.Update();
			Organism.Age++;
		}

		public override void FixedAction ()
		{
			inner.FixedUpdate();
		}
		#endregion
	}
}