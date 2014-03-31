using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GeneticCode.Interfaces;

namespace GeneticCode
{
    public delegate void DiedEventhandler(object sender, EventArgs e);
    /// <summary>
    /// Entry class to interact with the organism.
    /// </summary>
    abstract class Organism
    {
        /// <summary>
        /// Used to differenciate organisms.
        /// </summary>
        private static int counter;
        public int myCounter { get; private set; }
        private int lifeExpectancy = 10;
        public int age { get; private set; }

        public Boolean canReproduce {get; set;}

        public Genotype genotype { get; protected set; }

        public event DiedEventhandler Died;

        public Thread thread { get; private set; }

        static Organism()
        {
            counter = 0;
        }

        protected Organism()
        {
            myCounter = ++counter;

            canReproduce = false;
            age = 0;
            thread = new Thread(new ThreadStart(this.loop));
        }

        /// <summary>
        /// Causes the organism to mutate accordingly to the settings in the mutation.
        /// </summary>
        /// <param name="m">The mutation</param>
        public void mutate(IMutation m)
        {
            genotype.mutate(m);
        }

        public abstract Organism createEmptyOrganism();

        override public string ToString()
        {
            return "I'm organism - " + myCounter + ":" + Environment.NewLine + genotype.ToString();
        }

        /// <summary>
        /// Launches the thread.
        /// </summary>
        public void start()
        {
            // Launches the thread.
            thread.Start();
        }

        /// <summary>
        /// Thread method to be called.
        /// </summary>
        private void loop()
        {
            Console.WriteLine("A new organisme is born ("+myCounter + ")");

            while (age < lifeExpectancy)
            {   
                if (age > 2)
                    canReproduce = true;

                if (age > lifeExpectancy - 2)
                    canReproduce = false;

                Console.WriteLine(myCounter + " - Moving");
                
                age++;
            }
            Console.WriteLine(myCounter + " - I'm dying.");
            OnDeath(EventArgs.Empty);
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnDeath(EventArgs e)
        {
            if (Died != null)
                Died(this, e);
        }
    }
}
