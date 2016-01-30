using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Orbs
{
    class MainMenuState : IState
    {
        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Clear(Color.Black);
        }
        
        public void HandleKeyPressed(KeyEventArgs i)
        {
            if (i.Code == Keyboard.Key.Escape)
            {
                Program.StateManager?.LeaveCurrentState();
            }
        }
    }
}
