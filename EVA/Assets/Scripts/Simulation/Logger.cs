/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : June 2014
/// </summary>

using System;
using System.IO;

namespace Simulation
{
    /// <summary>
    /// Handles all the file writing for the simulation.
    /// </summary>
    public class Logger
    {
        private static Logger Instance { get; set; }
        
        private static string ContainerDirectory = "Logs";
        
        // The name of the folder for this simulation.
        private static String SimulationDirectory;

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file currently written.</value>
        public String File { get; set; }
        
        private Logger()
        {
            DateTime now = DateTime.Now;
            SimulationDirectory = now.Day + "_" + now.Month + "_" + now.Year + "-"
            + now.Hour + "h" + now.Minute + "m" + now.Second + "s";
            }
            
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        public static Logger getInstance()
        {
            if (Instance == null)
            {
                Instance = new Logger();
                CreateDirectoryIfNotExist(ContainerDirectory + "/" + SimulationDirectory);
            }
            return Instance;
        }
        
        /// <summary>
        /// Creates the directory if he doesn't exist
        /// </summary>
        /// <param name="directory">The directory.</param>
        private static void CreateDirectoryIfNotExist(String directory)
        {
             if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        
        /// <summary>
        /// Write o in a file on one line.
        /// </summary>
        /// <param name="o">The object to write on the file.</param>
        public void Save(Object o)
        {
            if (Instance == null)
            {
                  throw new Exception("File is not set !");
            }
            using (StreamWriter file = new StreamWriter(@"./" + ContainerDirectory + "/" + SimulationDirectory + "/" + File, true))
            {
               file.WriteLine(o);
            }
         }
         
        /// <summary>
        /// Write o in a file on one line.
        /// </summary>
        /// <param name="o">The object to write on the file.</param>
        public void Save(Object o, String file)
        {
            File = file;
            Save(o);
        }
      }
   }

