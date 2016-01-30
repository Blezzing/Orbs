using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    class SplashScreenState : IState
    {
        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Clear(Color.Red);
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            Program.StateManager?.EnterState(new MainMenuState());
        }
    }
}
