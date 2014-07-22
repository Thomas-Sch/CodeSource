using System;
using System.Text;
using UnityEngine;

namespace Organisms.States
{
    public abstract class AutoState : State
    {
        public AutoState(Organism organism, DUpdateState updateState) : base(organism, updateState)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (UpdateState != null)
            {
                UpdateState();
            }
        }
    }
}
