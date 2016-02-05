using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System;

namespace Orbs
{
    public class ExploringState : IState
    {
        private TileMap map;
        private Character player;

        private View exploringView;
        private View defaultView;

        private Clock time;

        public ExploringState()
        {
            //pretty loading here
            map = new TileMap();
            player = new Character();
            exploringView = new View(Program.Window.GetView());
            defaultView = Program.Window.DefaultView;
            time = new Clock();
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            switch (i.Code)
            {
                case Keyboard.Key.Escape:
                    Program.Window.SetView(Program.Window.DefaultView);
                    Program.StateManager.LeaveCurrentState();
                    break;
                default:
                    break;
            }
        }

        public void Render()
        {
            Program.Window.SetView(exploringView);
            Program.Window.Draw(map);
            Program.Window.Draw(player);
            Program.Window.SetView(defaultView);
        }

        public void Update()
        {
            player.Update(time.ElapsedTime);
            exploringView.Center = player.Position;
            time.Restart();
        }
    }
}
