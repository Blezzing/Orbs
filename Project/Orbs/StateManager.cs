using System;
using System.Collections.Generic;

namespace Orbs
{
    public class StateManager
    {
        private Stack<State> states = new Stack<State>();

        public event EventHandler OutOfStates;

        public State CurrentState
        {
            get
            {
                if (states.Count > 0)
                { 
                    return states.Peek();
                }
                else
                {
                    return null;
                }
            }
        }

        public StateManager()
        {
            //Nothing special happends.
        }

        public void LeaveCurrentState()
        {
            if (states.Count > 0)
            {
                states.Pop();

                if (states.Count <= 0 && OutOfStates != null)
                {
                    OutOfStates(this, new EventArgs());
                }
            }
        }

        public void ReplaceState(State newState)
        {
            if (states.Count > 0)
            {
                states.Pop();
            }

            states.Push(newState);
        }

        public void EnterState(State newState)
        {
            states.Push(newState);
        }
    }
}
