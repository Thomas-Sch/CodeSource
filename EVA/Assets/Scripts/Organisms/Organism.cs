/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using System;
using System.Collections;
using GeneticLibrary;
using GeneticLibrary.BodyParts;
using GeneticLibrary.Mutations;
using GeneticLibrary.Interfaces;
using Wrappers;
using States;
using UnityEngine;
using Simulation.Handling;
using Simulation;

/// <summary>
/// Provide base support to handle Organisms.
/// It supports the concept of speed and aging.
/// </summary>
public abstract class Organism : MonoBehaviour {    
    public static String file = "Organism.csv";


    public GameObject motor;
    public static int NumberOfOrganisms = 0;

    // Duration of this state based on the age of the organism.
    private float PreAdultDuration;

    // Name of the organism.
    public String Name {get; set;}
    // Age of the organism.
    public int Age {get; set;}

    public float Distance {get; set;}

    public int NumberOfReproduction {get; set;}

    public String NameParent1 {get; set;}
    public String NameParent2 {get; set;}

    public long Birth {get; set;}
    public long Death {get; set;}

    public bool IsDead { get; set; }

    private GraphicModifier graphic;

    //--- Control of the organism. ---//
    public PhenotypeData phenotypeData {get; set;} // Need renommage maybe. Confusion entre le phenotype des extensions et des données.
    public State State {get; set;}
    public Genotype Genotype {get; set;}
    
    static Organism()
    {
         // Preparing the log file for the organisms.
         Logger.getInstance().Save("Name;Birth;Death;Age;Distance;NameParent1;NameParent2;NumberOfReproductions;Type;Genotype", file);
    }

    public Organism() {
        Distance = 0;
        NumberOfReproduction = 0;
        Age = 0;
        IsDead = false;
        graphic = new GraphicModifier();
    }

    /// <summary>
    /// Returns the prefab of the instance of the organism.
    /// </summary>
    public abstract GameObject Prefab();
    
    /// <summary>
    /// Returns the mutation which is applied before the organism is born.
    /// </summary>
    /// <returns>The spawn mutation.</returns>
    protected abstract IMutation PreSpawnMutation();
    
    /// <summary>
    /// Initialisation of an organism.
    /// </summary>
    private void Initialisation() {
        // Defining the genotype.
        Genotype = new Genotype(new Square(new GeneticData()));
        DeductGenotype(transform, Genotype.Root);
        
        // Adding extra parameters.		
        ExtendGenotype();
        
        // First time mutation to add noise and whatever.
        Genotype.Mutate(PreSpawnMutation());
        
        // Applying it to the phenotype.
        ChangePhenotype(Genotype);

        graphic.Init(this);
    }
    
    /// <summary>
    /// Enable or disable the collider on the organism.
    /// </summary>
    /// <param name="b">If set to <c>true</c> b.</param>
    public void SetCollider(bool b) {
        collider.enabled = b;
        foreach(var child in GetComponentsInChildren<Collider>()) {
            child.enabled = b;
        }
    }

    /// <summary>
    /// Save the statistics of the organism.
    /// </summary>
    public void LogSelf() {
        Logger.getInstance().Save(Name + ";" + 
                                  Birth + ";" +
                                  Death + ";" +
                                  Age + ";" + 
                                  Distance + ";" + 
                                  NameParent1 + ";" +
                                  NameParent2 + ";" +
                                  NumberOfReproduction + ";" +
                                  GetType() + ";" +
                                  Genotype.ToString(), file);
    }
    
    /// <summary>
    /// Kill this instance.
    /// </summary>
    public void Kill() {
        Destroy(gameObject);
    }

    #region Unity

    public void Start() {
        Initialisation();
        State = new Birth(this, BirthToPreAdult);
    }
    
    public void Awake()
    {
        PreAdultDuration = SimHandler.Instance().Parameters.PreAdultDuration;
    }

    // Fixed Update is called once by physic engine
    public void FixedUpdate() {
        try {
            State.FixedUpdate(); 
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public void Update() {
        graphic.Update();
    }
    
    #endregion

    #region State transition delegates

    protected void BirthToPreAdult() {
        State = new PreAdult(this, PreAdultToAdult);
    }

    protected void PreAdultToAdult() {
        if((float)Age/phenotypeData.LifeDuration > PreAdultDuration)
            State = new Adult(this, AdultToDeath);
    }

    protected void AdultToDeath() {
        if(Age > phenotypeData.LifeDuration)
            State = new Death(this, null);
    }

    #endregion

    #region Genetic handling
    /// <summary>
    /// Deducts the genotype.
    /// </summary>
    /// <param name="t">T. The current transform component examined.</param>
    /// <param name="e">E. The last extension treated (will serve as the parent for this call)</param>
    private void DeductGenotype(Transform t, Extension e) {
        e.GeneticData.Set("scale", new WVector3(t.localScale));
        e.GeneticData.Set("rotation", new WVector3(t.localRotation.eulerAngles));
        e.GeneticData.Set("position", new WVector3(t.localPosition));
        
        foreach(Transform child in t) {
            Extension eChild = new Square(new GeneticData());
            e.AddExtension(eChild);
            DeductGenotype(child, eChild);
        }
    }

    /// <summary>
    /// Applies the genotype to the phenotype.
    /// </summary>
    /// <param name="g">The green component.</param>
    public void ChangePhenotype(Genotype g) {
        ModifyElement(transform, g.Root);

        if(phenotypeData == null) {
            phenotypeData = new PhenotypeData();
        }
        phenotypeData.LifeDuration = (int)((WFloat)g.Root.GeneticData.Get("lifeduration")).Value;
        phenotypeData.Speed = ((WFloat)g.Root.GeneticData.Get("speed")).Value;
    }

    /// <summary>
    /// Modify the transform of one gameobject based on the given genetic data.
    /// </summary>
    /// <param name="t">The transform to modify.</param>
    /// <param name="e">The extension containing the genetic data.</param>
    private void ModifyElement(Transform t, Extension e) {
        t.localPosition = ((WVector3)e.GeneticData.Get("position")).Value;
        t.localRotation = Quaternion.Euler(((WVector3)e.GeneticData.Get("rotation")).Value);
        t.localScale = ((WVector3)e.GeneticData.Get("scale")).Value;
        
        IEnumerator transforms = t.GetEnumerator();
        IEnumerator extensions = e.GetEnumerator();
        
        while(transforms.MoveNext() && extensions.MoveNext()) {
            ModifyElement((Transform)transforms.Current, (Extension)extensions.Current);
        }
    }

    /// <summary>
    /// Adds informations to the genotype.
    /// </summary>
    protected void ExtendGenotype ()
    {
        Genotype.Root.Tag = "root";

        // Par défaut.
        Genotype.Root.SetGeneticData("lifeduration", new WFloat(500));
        Genotype.Root.SetGeneticData("speed", new WFloat(0.2F));
    }
    #endregion

    #region Overriding
    override public string ToString() {
        return GetType() + Name;
    }
    #endregion
}
