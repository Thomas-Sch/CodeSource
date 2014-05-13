/// <summary>
/// Contains global variables used in the code.
/// </summary>
using System;

/// <summary>
/// Starting and general parameters for the simulation. Parents have 1 child per reproduction.
/// </summary>
public class Simulation
{
	// Simulation
	public static readonly string OrganismTag = "Organism";
	public static readonly int InitialPopulation = 20;

	// Organisms
	public static readonly int BirhtDuration = 100; // [Nombre d'updates]
	public static readonly float PreAdultDuration = 0.0f; // [%]
	public static readonly float OrganismSight = 10F; // [Longueur]

	// Death settings
	public static readonly float TimeBeforeRemove = 0; // [s]
	public static readonly float DeathSmooth = 0.01f; // [0.0 - 1.0]

	// Movement settings
	public static readonly float MovementTurnRate = 0.5f; // [0.0 - 1.0]

	// Reproduction settings
	public static readonly float ApproachRate = 0.5f; // [0.0 - 1.0]
	public static readonly int NoNewChildDuration = 100; // [Nombre d'updates]
}

