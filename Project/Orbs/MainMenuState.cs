using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;

namespace Orbs
{
    class MainMenuState : IState
    {
        private Menu menu;
        private Text title;

        public MainMenuState()
        {
            Font menuFont = new Font("Assets/Fonts/Base.ttf");

            title = new Text("ORBS", menuFont, 140);
            title.Color = new Color(200, 200, 255);
            title.Position = new SFML.System.Vector2f(Program.Window.Size.X / 2 - title.GetLocalBounds().Width / 2, 100);

            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuItem(new Text("Play", menuFont, 60), () => { }));
            items.Add(new MenuItem(new Text("Options", menuFont, 60), () => { }));
            items.Add(new MenuItem(new Text("Help", menuFont, 60), () => { }));
            items.Add(new MenuItem(new Text("Credits", menuFont, 60), () => { Program.StateManager.LeaveCurrentState(); }));
            items.Add(new MenuItem(new Text("Exit", menuFont, 60), () => { Program.Window.Close(); }));

            menu = new Menu(items, new Vector2f(Program.Window.Size.X / 2, 350),700);
        }

        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Clear(Color.Black);
            title.Draw(Program.Window, RenderStates.Default);
            menu.Draw();
        }
        
        public void HandleKeyReleased(KeyEventArgs i)
        {
            switch(i.Code)
            {
                case (Keyboard.Key.Up):
                    menu?.MoveUp();
                    break;
                case (Keyboard.Key.Down):
                    menu?.MoveDown();
                    break;
                case (Keyboard.Key.Return):
                    menu?.SelectItem();
                    break;
            }
        }

        public void HandleMouseReleased(MouseButtonEventArgs i)
        {
        }
    }
}
