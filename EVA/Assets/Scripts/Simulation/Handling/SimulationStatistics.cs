/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : July 2014
/// </summary>
/// 

using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Simulation.Handling
{
    class SimulationStatistics : ISimStats
    {
        private static string file = "Statistics.csv";

        private int blockNumber;

        public int interval {get; set;}

        static SimulationStatistics() {
            Logger.getInstance().Save(
                "Step;Time;Cumulative_Average_Distance;Block_Average_Distance;Sliding_Window_Average_Distance;"+
                "Cumulative_Average_Age;Block_Average_Age;Sliding_Window_Average_Age;Nb_Organism_Alive;Nb_Organism_Dead", file);
        }

        public SimulationStatistics(int interval)
        {
            this.interval = interval;
        }

        public void Log()
        {
            blockNumber = (int)SimHandler.Instance().Step / SimHandler.Instance().Parameters.BlockLength;

            // Log the statistics.
            Logger.getInstance().Save(SimHandler.Instance().Step + ";" + 
                                      SimHandler.Control().TimeElapsed() + ";" + 
                                      AverageDistanceCumulative() + ";" + 
                                      AverageDistanceBlock() + ";" +
                                      AverageDistanceSlidingWindow() + ";" +
                                      AverageAgeCumulative() + ";" + 
                                      AverageAgeBlock() + ";" +
                                      AverageAgeSlidingWindow() + ";" +
                                      NbOrganismAlive() + ";" +
                                      NbOrganismDead(),file);
        }

        public void SaveParameters(Parameters parameter, string file)
        {
            Logger logger = Logger.getInstance();
            logger.File = file;
            logger.Save(JsonConvert.SerializeObject(parameter));
        }

        public float AverageDistanceCumulative()
        {
            float result = 0;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if(o.IsDead)
                    result += o.Distance;
            }
            result = result / NbOrganismDead();

            return result;
        }

        public float AverageDistanceBlock()
        {
            float result = 0;
            int nbr = 0;

            int min = blockNumber * SimHandler.Instance().Parameters.BlockLength;
            int max = (blockNumber + 1) * SimHandler.Instance().Parameters.BlockLength;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if (o.IsDead && o.Death >= min && o.Death < max)
                {
                    result += o.Distance;
                    nbr++;
                }
            }
            result = result / nbr;

            return result;
        }
        
        public float AverageDistanceSlidingWindow() {
            float result = 0;
            int nbr = 0; 

            long min = SimHandler.Instance().Step - SimHandler.Instance().Parameters.SlidingWindowLength / 2;
            long max = SimHandler.Instance().Step + SimHandler.Instance().Parameters.SlidingWindowLength / 2;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if (o.IsDead && o.Death >= min && o.Death < max)
                {
                    result += o.Distance;
                    nbr++;
                }
            }
            result = result / nbr;

            return result;
        }

        public float AverageAgeCumulative()
        {
            float result = 0;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if (o.IsDead)
                    result += o.Age;
            }
            result = result / NbOrganismDead();

            return result;
        }

        public float AverageAgeBlock() {
            float result = 0;
            int nbr = 0;

            int min = blockNumber * SimHandler.Instance().Parameters.BlockLength;
            int max = (blockNumber + 1) * SimHandler.Instance().Parameters.BlockLength;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if (o.IsDead && o.Death >= min && o.Death < max)
                {
                    result += o.Age;
                    nbr++;
                }
            }
            result = result / nbr;

            return result;
        }

        public float AverageAgeSlidingWindow() {
            float result = 0;
            int nbr = 0; 

            long min = SimHandler.Instance().Step - SimHandler.Instance().Parameters.SlidingWindowLength / 2;
            long max = SimHandler.Instance().Step + SimHandler.Instance().Parameters.SlidingWindowLength / 2;

            foreach (Organism o in SimHandler.PopulationHandler().Organisms)
            {
                if (o.IsDead && o.Death >= min && o.Death < max)
                {
                    result += o.Age;
                    nbr++;
                }
            }
            result = result / nbr;

            return result;
        }

        public int NbOrganismAlive()
        {
            return SimHandler.PopulationHandler().LivingOrganisms;
        }

        public int NbOrganismDead()
        {
            return SimHandler.PopulationHandler().DeadOrganisms;
        }
    }
}
