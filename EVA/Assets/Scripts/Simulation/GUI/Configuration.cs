/// <summary>
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
		private string birthDuration = "";
		private string organismSight = "";
		private string noNewChildDuration = "";

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
			birthDuration = parameters.BirthDuration.ToString();
			organismSight = parameters.OrganismSight.ToString();
			noNewChildDuration = parameters.NoNewChildDuration.ToString();

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

			GUILayout.Label("Birth parameters");
			birthDuration = LabelField("Bith duration", birthDuration, checkIntField(birthDuration));

			GUILayout.Label("Reproduction parameters");
			parameters.PreAdultDuration = LabelSlider("Pre-Adult duration", parameters.PreAdultDuration, 1.0f);
            noNewChildDuration = LabelField("No new child duration", noNewChildDuration, checkIntField(noNewChildDuration));
            organismSight = LabelField("Organism's sight range", organismSight, checkFloatField(organismSight));

			GUILayout.Label("Movement parameters");
			parameters.MovementTurnRate = LabelSlider("Mouvement turn rate", parameters.MovementTurnRate, 1.0f);
			parameters.ApproachRate = LabelSlider("Approach turn rate", parameters.ApproachRate, 1.0f);

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
                    parameters.BirthDuration = int.Parse(birthDuration);
                    parameters.NoNewChildDuration = int.Parse(noNewChildDuration);
                    parameters.OrganismSight = float.Parse(organismSight);
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
