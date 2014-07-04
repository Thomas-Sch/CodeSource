/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>
/// 
using System;

namespace Simulation.GUI
{
    /// <summary>
    /// Defines a GUI window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Draw the GUI on the screen.
        /// </summary>
        void Draw();
    }
}