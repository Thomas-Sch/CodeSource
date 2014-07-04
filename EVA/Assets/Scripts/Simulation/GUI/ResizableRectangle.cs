using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Simulation.GUI
{
    /// <summary>
    /// Handy class to take care of resizable dimensions without having 4 variables to handle it in a loop...
    /// </summary>
    class ResizableRectangle
    {
        private double widthPercentage;
        private double heightPercentage;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ResizableRectangle(double widthPercentage, double heightPercentage)
        {
            this.widthPercentage = widthPercentage;
            this.heightPercentage = heightPercentage;
            Update();
        }

        /// <summary>
        /// Update the size of the rectangle
        /// </summary>
        public void Update()
        {
            Width = (int)(widthPercentage * Screen.width);
            Height = (int)(heightPercentage * Screen.height);
        }
    }
}
