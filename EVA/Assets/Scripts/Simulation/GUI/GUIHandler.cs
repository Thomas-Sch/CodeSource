/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>

using UnityEngine;
using System.Collections;
using System;

namespace Simulation.GUI
{
    /// <summary>
    /// Display GUI at the screen.
    /// </summary>
    public class GUIHandler : MonoBehaviour
    {
        private IWindow current;
        private static GUIHandler instance;

        private GUIHandler()
        {
            // The declaration of the singleton is done here because Unity forbides us to use the new keyword on MonoBehaviour objects.
            instance = this;

            // We begin the application with the title.
            ChangeWindow(new Title());
        }

        /// <summary>
        /// Unity's refresh function for the GUI.
        /// </summary>
        void OnGUI()
        {
            if (current != null)
                current.Draw();
        }

        /// <summary>
        /// Changes the windows to be displayed.
        /// </summary>
        /// <param name="w">The new window</param>
        public void ChangeWindow(IWindow w)
        {
            current = w;
        }

        /// <summary>
        /// Returns the instance of the singleton.
        /// </summary>
        /// <returns>The singleton</returns>
        public static GUIHandler Instance()
        {
            if (instance == null)
                throw new Exception("The singleton was not found !");
            return instance;
        }
    }
}