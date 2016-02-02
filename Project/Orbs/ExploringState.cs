using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Orbs
{
    public class ExploringState : IState
    {
        private TileMap map;
        private View exploringView;
        private View defaultView;

        public ExploringState()
        {
            //pretty loading here
            map = new TileMap();
            exploringView = new View(Program.Window.GetView());
            defaultView = Program.Window.DefaultView;
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            switch (i.Code)
            {
                case Keyboard.Key.Escape:
                    Program.Window.SetView(Program.Window.DefaultView);
                    Program.StateManager.LeaveCurrentState();
                    break;
                case Keyboard.Key.Right:
                    exploringView.Move(new Vector2f(4, 0));
                    break;
                case Keyboard.Key.Left:
                    exploringView.Move(new Vector2f(-4, 0));
                    break;
                case Keyboard.Key.Up:
                    exploringView.Move(new Vector2f(0, -4));
                    break;
                case Keyboard.Key.Down:
                    exploringView.Move(new Vector2f(0, 4));
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        public void Render()
        {
            Program.Window.SetView(exploringView);
            Program.Window.Draw(map);
            Program.Window.SetView(defaultView);
        }

        public void Update()
        {
        }
    }
}
