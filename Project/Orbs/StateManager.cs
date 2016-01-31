using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class StateManager
    {
        private Stack<IState> states = new Stack<IState>();

        public event EventHandler OutOfStates;

        public IState CurrentState
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

        public void ReplaceState(IState newState)
        {
            if (states.Count > 0)
            {
                states.Pop();
            }

            states.Push(newState);
        }

        public void EnterState(IState newState)
        {
            states.Push(newState);
        }
    }
}
