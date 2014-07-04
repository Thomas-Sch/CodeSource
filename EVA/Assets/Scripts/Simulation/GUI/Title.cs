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
        private ResizableRectangle size = new ResizableRectangle(0.15, 0.2);

        private Vector2 scrollPosition = new Vector2(0, 0);

        public void Draw()
        {
            size.Update();
            GUILayout.BeginArea(new Rect(Screen.width / 2 - size.Width / 2, Screen.height / 2 - size.Height / 2, size.Width, size.Height), "", "box");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
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
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}
