/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>

using System;
using UnityEngine;
using Simulation.Handling;

namespace Simulation.GUI
{
    /// <summary>
    /// GUI controlling the simulation.
    /// </summary>
    class HUD : IWindow
    {
        private Parameters parameters;

        private Rect control;
        private Rect statistics;
        private Rect help;

        // Size settings
        private int screenMargin = 20;
        private int windowMargin = 5;

        private int wControlWidth = 200;
        private int wControlHeight = 150;

        private int wStatsWidth = 250;
        private int WStatsHeight = 190;

        private int wHelpWidth = 100;
        private int wHelpHeight = 32;

        private int infoPaneWidth = 300;
        private int infoPaneHeight = 200;

        // Global logic
        private bool about = false;
        private bool keys = false;

        // Control's logic
        private bool play = false;
        private bool isStarted = false;
        private bool maximumSpeed = false;
        private bool isQuitting = false;

        // Help data
        private string aboutText = 
            "HEIG-VD 2014\n" + 
            "Realised by Thomas Schweizer for his bachelor project 'Evolution et vies artificielles'\n" + 
            "Professor: Carlos Pena";
        private string keysText = 
            "Controls:\n" +
            "Alt + mouse - Move the camera around\n"+
            "W,A,S,D - Move the camera (Front, Left, Right, Back)\n" +
            "Q - Rise the camera\n" +
            "E - Lower the camera\n" +
            "Shift - Accelerate the camera\n" +
            "Ctrl - Slow the camera";


        /// <summary>
        /// Initialize the HUD for the simulation and gives the parameters to the simulation.
        /// </summary>
        /// <param name="p">parameters of the simulation</param>
        public HUD(Parameters p)
        {
            control = new Rect(screenMargin, screenMargin, wControlWidth, wControlHeight);
            statistics = new Rect(Screen.width - wStatsWidth - screenMargin, screenMargin, wStatsWidth, WStatsHeight);
            help = new Rect(screenMargin, Screen.height - wHelpHeight, wHelpWidth, wHelpHeight);

            parameters = p;
            SimHandler.Control().Init(parameters);
        }

        public void Draw()
        {
            // Draw the main windows.
            control = UnityEngine.GUI.Window(0, control, WindowControl, "Controls");
            statistics = UnityEngine.GUI.Window(1, statistics, WindowStatistics, "Statistics");
            help = UnityEngine.GUI.Window(2, help, WindowHelp, "");

            // The drawing of the panes related to the help window is externalized here because
            // we can't draw outside of a window.
            if (about)
                about = InfoPane(aboutText);
            if (keys)
                keys = InfoPane(keysText);

        }

        /// <summary>
        /// Draws the control window.
        /// </summary>
        /// <param name="id">The id of the window (Unity's GUI system requirement)</param>
        private void WindowControl(int id)
        {
            GUILayout.BeginArea(new Rect(windowMargin, windowMargin + 15, wControlWidth - 2 * windowMargin, wControlHeight - 5 * windowMargin));
            
            if (!isStarted && GUILayout.Button("Start"))
            {
                SimHandler.Control().Play();
                play = true;
                isStarted = true;
            }

            if (isStarted && GUILayout.Button((!play ? "Resume" : "Pause")))
            {
                if (play)
                {
                    play = false;
                    SimHandler.Control().Pause(); 
                }
                else
                {
                    SimHandler.Control().Resume();
                    play = true;
                }
            }

            // Simulation speed slider.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Speed (" + Math.Round(SimHandler.Control().Speed, 1).ToString() + ")");
            GUILayout.FlexibleSpace();
            SimHandler.Control().Speed = GUILayout.HorizontalSlider(SimHandler.Control().Speed, 0.0f, 10f, GUILayout.Width(70));
            GUILayout.EndHorizontal();

            // Maximum speed toggle.
            if (GUILayout.Button((maximumSpeed ? "Normal speed": "Maximum speed")))
            {
                if (!maximumSpeed)
                {
                    SimHandler.Control().MaximumSpeed();
                    maximumSpeed = true;
                }
                else
                {
                    SimHandler.Control().Resume();
                    maximumSpeed = false;
                }
            }

            GUILayout.FlexibleSpace();

            if (!isQuitting && GUILayout.Button("Exit"))
            {
                isQuitting = true;
            }

            if (isQuitting)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Save & Quit"))
                {
                    SimHandler.Control().Stop();
                }

                if (GUILayout.Button("Cancel"))
                {
                    isQuitting = false;
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        /// <summary>
        /// Draws the statistics window.
        /// </summary>
        /// <param name="id">The id of the window (Unity's GUI system requirement)</param>
        private void WindowStatistics(int id)
        {
            GUILayout.BeginArea(new Rect(windowMargin, windowMargin + 15, wStatsWidth - 2 * windowMargin, WStatsHeight - 5 * windowMargin));
            LabelLabel("Elapsed time", SimHandler.Control().TimeElapsed().ToString());
            LabelLabel("Sim. step", SimHandler.Instance().Step.ToString());
            GUILayout.FlexibleSpace();
            LabelLabel("Average distance", SimHandler.Statistics().AverageDistance().ToString());
            LabelLabel("Average age", SimHandler.Statistics().AverageAge().ToString());
            LabelLabel("Number of alive organisms", SimHandler.Statistics().NbOrganismAlive().ToString());
            LabelLabel("Number of dead organisms", SimHandler.Statistics().NbOrganismDead().ToString());
            GUILayout.EndArea();
        }

        /// <summary>
        /// Draw two labels to be able to print a label and a value together.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="content"></param>
        private void LabelLabel(String label, String content)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            GUILayout.FlexibleSpace();
            GUILayout.Label(content);
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the help window.
        /// </summary>
        /// <param name="id">The id of the window (Unity's GUI system requirement)</param>
        private void WindowHelp(int id)
        {
            GUILayout.BeginArea(new Rect(windowMargin, windowMargin, wHelpWidth - 2 * windowMargin, wHelpHeight - windowMargin));
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("?"))
            {
                about =! about;
            }

            if (GUILayout.Button("Keys"))
            {
                keys =! keys;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        /// <summary>
        /// Draw an pane about the program.
        /// </summary>
        /// <param name="text">The text contained in the info pane.</param>
        /// <returns>False if the pane must be closed.</returns>
        private bool InfoPane(string text)
        {
            bool temp = true;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - infoPaneWidth / 2, Screen.height / 2 - infoPaneHeight / 2, infoPaneWidth, infoPaneHeight), "", "box");
            GUILayout.Label(text);

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Cancel"))
            {
                temp = false;
            }

            GUILayout.EndArea();
            return temp;
        }
    }
}

