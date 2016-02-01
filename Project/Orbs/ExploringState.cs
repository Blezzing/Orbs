using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Orbs
{
    class ExploringState : IState
    {
        private TileMap map;
        private View view = Program.Window.GetView();

        public ExploringState()
        {
            map = new TileMap();
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            switch (i.Code)
            {
                case (Keyboard.Key.Escape):
                    Program.Window.SetView(Program.Window.DefaultView);
                    Program.StateManager.LeaveCurrentState();
                    break;
                case (Keyboard.Key.Right):
                    view.Move(new Vector2f(4, 0));
                    Program.Window.SetView(view);
                    break;
                case (Keyboard.Key.Left):
                    view.Move(new Vector2f(-4, 0));
                    Program.Window.SetView(view);
                    break;
                case (Keyboard.Key.Up):
                    view.Move(new Vector2f(0, -4));
                    Program.Window.SetView(view);
                    break;
                case (Keyboard.Key.Down):
                    view.Move(new Vector2f(0, 4));
                    Program.Window.SetView(view);
                    break;
            }
        }

        public void Render()
        {
            Program.Window.Draw(map);
        }

        public void Update()
        {
        }
    }
}
