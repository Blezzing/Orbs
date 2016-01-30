using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class StateManager
    {
        private Stack<IState> states;

        public event EventHandler Empty;
        public event EventHandler CurrentStateChanged;

        public IState CurrentState
        {
            get
            {
                try
                {
                    return states?.Peek();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public StateManager()
        {
            states = new Stack<IState>(); 
        }

        public void LeaveCurrentState()
        {
            if (states?.Count > 0)
            {
                states.Pop();

                if (CurrentStateChanged != null)
                {
                    CurrentStateChanged(this, new EventArgs());
                }
            }

            if (states?.Count <= 0 && Empty != null)
            {
                Empty(this, new EventArgs());
            }
        }

        public void ReplaceState(IState newState)
        {
            if (states?.Count > 0)
            {
                states?.Pop();
            }

            states?.Push(newState);

            if (CurrentStateChanged != null)
            {
                CurrentStateChanged(this, new EventArgs());
            }
        }

        public void EnterState(IState newState)
        {
            states?.Push(newState);

            if (CurrentStateChanged != null)
            {
                CurrentStateChanged(this, new EventArgs());
            }
        }
    }
}
