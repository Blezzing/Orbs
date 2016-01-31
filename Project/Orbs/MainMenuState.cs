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

            List<Tuple<String,Action>> items = new List<Tuple<String, Action>>();
            items.Add(new Tuple<string, Action>("Play",     () => { }                                           ));
            items.Add(new Tuple<string, Action>("Options",  () => { }                                           ));
            items.Add(new Tuple<string, Action>("Help",     () => { }                                           ));
            items.Add(new Tuple<string, Action>("Credits",  () => { Program.StateManager.LeaveCurrentState(); } ));
            items.Add(new Tuple<string, Action>("Exit",     () => { Program.Window.Close(); }                   ));

            menu = new Menu(items, menuFont, 60, new Vector2f(Program.Window.Size.X / 2, 350),700);
        }

        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Draw(title);
            Program.Window.Draw(menu);
        }
        
        public void HandleKeyPressed(KeyEventArgs i)
        {
            switch(i.Code)
            {
                case (Keyboard.Key.Up):
                    menu?.MoveUp();
                    break;
                case (Keyboard.Key.Down):
                    menu?.MoveDown();
                    break;
                case (Keyboard.Key.Space):
                case (Keyboard.Key.Return):
                    menu?.SelectItem();
                    break;
            }
        }
    }
}
