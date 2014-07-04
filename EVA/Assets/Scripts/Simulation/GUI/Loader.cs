/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>
/// 
using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Simulation.GUI
{
    /// <summary>
    /// Handles the loading of a parameter file.
    /// </summary>
    class Loader : IWindow
    {
        private ResizableRectangle size = new ResizableRectangle(0.3, 0.3);

        private Vector2 scrollPosition = new Vector2(0, 0);

        private String title = "Specify the parameter file (Parameters.json)";

        private String path = "";

        private bool error = false;
        private string errorMessage;

        GUIStyle errorStyle = new GUIStyle(UnityEngine.GUI.skin.label);

        public void Draw()
        {
            size.Update();
            GUILayout.BeginArea(new Rect(Screen.width / 2 - size.Width / 2, Screen.height / 2 - size.Height / 2, size.Width, size.Height), "", "box");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.Label(title);

            if (error)
            {
                GUILayout.Label("File is missing or format is corrupted:\n" + errorMessage + "\n\nTry again.", errorStyle);
            }

            GUILayout.FlexibleSpace();

            path = GUILayout.TextField(path, 255);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load"))
            {
                try
                {
                    // Get the configuration from the file at path.
                    string file = System.IO.File.ReadAllText(path);

                    // Convert the textfile to object.
                    Parameters parameters = JsonConvert.DeserializeObject<Parameters>(file);

                    // Open the simulation gui.
                    GUIHandler.Instance().ChangeWindow(new HUD(parameters));
                }
                catch (Exception e)
                {
                    error = true;
                    errorStyle.normal.textColor = Color.red;
                    errorMessage = e.Message;
                }
            }

            if (GUILayout.Button("Cancel"))
            {
                GUIHandler.Instance().ChangeWindow(new Title());
            }
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}

