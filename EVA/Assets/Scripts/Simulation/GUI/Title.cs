/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>
/// 
using System;
using UnityEngine;
using Simulation.Handling;

namespace Simulation.GUI
{
    /// <summary>
    /// Displays the title of the application.
    /// </summary>
    public class Title : IWindow
    {
        private int width = 120;
        private int height = 120;

        public void Draw()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), "", "box");
            GUILayout.Label("Main menu", "box");
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("New simulation"))
            {
                GUIHandler.Instance().ChangeWindow(new Configuration(new DefaultParameters()));
            }

            if (GUILayout.Button("Load simulation"))
            {
                GUIHandler.Instance().ChangeWindow(new Loader());
            }

            if (GUILayout.Button("Quit"))
            {
                SimHandler.Control().Stop();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();
        }
    }
}
