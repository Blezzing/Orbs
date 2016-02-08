using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System;

namespace Orbs
{
    public class ExploringState : State
    {
        private TileMap map;
        private PlayerCharacter player;

        private View exploringView;
        private View defaultView;

        public ExploringState()
        {
            //Pretty loading here
            map = new TileMap();
            player = new PlayerCharacter(map, new Vector2i(15,15));
            exploringView = new View(Program.Window.GetView());
            defaultView = Program.Window.DefaultView;
        }

        public override void HandleKeyPressed(KeyEventArgs i)
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

        public override void Update()
        {
            player.Update();
            exploringView.Center = player.SpritePosition;
            map.SetDrawingPoint(player.Position);
        }

        public override void Render()
        {
            Program.Window.SetView(exploringView);
            Program.Window.Draw(map);
            Program.Window.Draw(player);
            Program.Window.SetView(defaultView);
        }
    }
}
