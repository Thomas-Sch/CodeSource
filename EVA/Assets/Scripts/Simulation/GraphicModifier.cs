/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>

using System;
using UnityEngine;

namespace Simulation {

    /// <summary>
    /// Modifies the render of the organism depending its state.
    /// </summary>
    public class GraphicModifier
    {
        private Organism model;

        private float initialR;
        private float initialG;
        private float initialB;
    
        public void Init(Organism model) {
            this.model = model;

            initialR = model.renderer.material.color.r;
            initialG = model.renderer.material.color.g;
            initialB = model.renderer.material.color.b;
        }

        public void Update() {

            // Gradually shift the root color of the organism to black.
            float percentage = 1.0f - model.Age / (float) model.phenotypeData.LifeDuration;
            model.renderer.material.color = new Color(initialR * percentage,
                                                      initialG * percentage, 
                                                      initialB * percentage);
        }
    }
}