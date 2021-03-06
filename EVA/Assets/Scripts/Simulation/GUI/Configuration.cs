﻿/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>

using System;
using Simulation;
using UnityEngine;

namespace Simulation.GUI
{
    /// <summary>
    /// Configuration windows for the simulation.
    /// </summary>
	class Configuration : IWindow
	{
		private Parameters parameters;
        private IWindow last;

		private string initialPopulation = "";
		private string organismSight = "";
		private string noNewChildDuration = "";
        private string blockLength = "";
        private string slidingWindowLength = "";
        private string worldSize = "";
        private string populationLimit = "";

        private ResizableRectangle size = new ResizableRectangle(0.25, 0.6);

        private Vector2 scrollPosition = new Vector2(0, 0);

        bool isConfigOk;

		GUIStyle originalStyle = new GUIStyle(UnityEngine.GUI.skin.label);
		GUIStyle errorStyle = new GUIStyle(UnityEngine.GUI.skin.label);

        /// <summary>
        /// Initialize the configuration windows with the given parameters.
        /// </summary>
        /// <param name="p"> The default parameters assigned to the interface.</param>
		public Configuration(Parameters p) {
			parameters = p;

			initialPopulation = parameters.InitialPopulation.ToString();
			organismSight = parameters.OrganismSight.ToString();
			noNewChildDuration = parameters.NoNewChildDuration.ToString();
            blockLength = parameters.BlockLength.ToString();
            slidingWindowLength = parameters.SlidingWindowLength.ToString();
            worldSize = parameters.WorldSize.ToString();
            populationLimit = parameters.PopulationLimit.ToString();

            // Sets the style in case of error.
			errorStyle.normal.textColor = Color.red;
		}

		public void Draw()
		{
			isConfigOk = true;
            size.Update();

			GUILayout.BeginArea(new Rect(Screen.width / 2 - size.Width / 2, Screen.height / 2 - size.Height / 2, size.Width, size.Height), "", "box");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
			GUILayout.Label("Configure the simulation", "box");


            GUILayout.Label("Simulation parameters");
            initialPopulation = LabelField("Initial population", initialPopulation, checkIntField(initialPopulation));
            worldSize = LabelField("World size", worldSize, checkIntField(worldSize));
            populationLimit = LabelField("PopulationLimit", populationLimit, checkIntField(populationLimit));

            GUILayout.Label("Organisms parameters");

            parameters.BabyDuration = LabelSlider("Baby duration", parameters.BabyDuration, 1.0f);
            parameters.TeenDuration = LabelSlider("Pre-Adult duration", parameters.TeenDuration, 1.0f);
            noNewChildDuration = LabelField("No new child duration", noNewChildDuration, checkIntField(noNewChildDuration));
            organismSight = LabelField("Organism's sight range", organismSight, checkFloatField(organismSight));

            GUILayout.Label("Statistics parameters");
            blockLength = LabelField("Block length", blockLength, checkIntField(blockLength));
            slidingWindowLength = LabelField("Sliding window length", slidingWindowLength, checkIntField(slidingWindowLength));

			GUILayout.FlexibleSpace();
            OKCancel();

            GUILayout.EndScrollView();

			GUILayout.EndArea();
		}

        /// <summary>
        /// Checks if the field is correct.
        /// </summary>
        /// <param name="value">The string value of the float parameter.</param>
        /// <returns>True is the field is valid.</returns>
        private bool checkFloatField(string value)
        {
            float temp;
            bool result = value == "" || !float.TryParse(value, out temp) || temp < 0;
            if (result)
                isConfigOk = false;
            return result;
        }

        /// <summary>
        /// Checks if the field is correct.
        /// </summary>
        /// <param name="value">The string value of the int parameter.</param>
        /// <returns>True is the field is valid.</returns>
        private bool checkIntField(string value)
        {
            int temp;
            bool result = value == "" || !int.TryParse(value, out temp) || temp < 0;
            if (result)
                isConfigOk = false;
            return result;
        }

        /// <summary>
        /// Draw a compound GUI element. It's a slider with a label.
        /// </summary>
        /// <param name="label">The content of the label.</param>
        /// <param name="value">The value of the slider</param>
        /// <param name="max">The maximum the slider can get. (min is 0.0)</param>
        /// <returns></returns>
		private float LabelSlider(string label, float value, float max)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(label);
			GUILayout.FlexibleSpace();

			GUILayout.BeginVertical();
			float result = GUILayout.HorizontalSlider(value, 0.0f, max, GUILayout.Width(70));

			GUILayout.Label(Math.Round(result, 2).ToString(), GUILayout.Width(30));
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			return result;
		}

        /// <summary>
        /// Draw a compound GUI element. It's a text field with a label.
        /// </summary>
        /// <param name="label">The content of the label.</param>
        /// <param name="value">The value of the field</param>
        /// <param name="error">If the field contains an error or not.</param>
        /// <returns></returns>
		private string LabelField(string label, string value, bool error)
		{
			GUIStyle current;
			if (error)
				current = errorStyle;
			else
				current = originalStyle;
			
			GUILayout.BeginHorizontal();
			GUILayout.Label(label, current);
			GUILayout.FlexibleSpace();
			string result = GUILayout.TextField(value, GUILayout.Width(70));
			GUILayout.EndHorizontal();

			return result;
		}

        /// <summary>
        /// Draw the OK and Cancel buttons of the windows with the respectives actions of these.
        /// </summary>
        private void OKCancel()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OK"))
            {
                if (isConfigOk)
                { 
                    // Apply text field parameters to parameter objects.
                    parameters.InitialPopulation = int.Parse(initialPopulation);
                    parameters.NoNewChildDuration = int.Parse(noNewChildDuration);
                    parameters.OrganismSight = float.Parse(organismSight);
                    parameters.BlockLength = int.Parse(blockLength);
                    parameters.SlidingWindowLength = int.Parse(slidingWindowLength);
                    parameters.WorldSize = int.Parse(worldSize);
                    parameters.PopulationLimit = int.Parse(populationLimit);
                    GUIHandler.Instance().ChangeWindow(new HUD(parameters));
                }
            }

            if (GUILayout.Button("Cancel"))
            {
                GUIHandler.Instance().ChangeWindow(new Title());
            }

            GUILayout.EndHorizontal();
        }
    }
}
