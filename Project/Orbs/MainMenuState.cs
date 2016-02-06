using SFML.Graphics;
using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.System;

namespace Orbs
{
    public class MainMenuState : IState
    {
        private readonly Menu menu;
        private readonly Text title;

        public MainMenuState()
        {
            Font menuFont = new Font("Assets/Fonts/Base.ttf");

            title = new Text("ORBS", menuFont, 140);
            title.Color = new Color(200, 200, 255);
            title.Position = new SFML.System.Vector2f(Program.Window.Size.X / 2 - title.GetLocalBounds().Width / 2, 100);

            List<Tuple<String,Action>> items = new List<Tuple<String, Action>>();
            items.Add(new Tuple<string, Action>("Play",     () => { Program.StateManager.EnterState(new ExploringState()); }    ));
            items.Add(new Tuple<string, Action>("Options",  () => { Program.IsFpsRendering = !Program.IsFpsRendering; }         ));
            items.Add(new Tuple<string, Action>("Help",     () => { }                                                           ));
            items.Add(new Tuple<string, Action>("Credits",  () => { Program.StateManager.LeaveCurrentState(); }                 ));
            items.Add(new Tuple<string, Action>("Exit",     () => { Program.Window.Close(); }                                   ));

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
                case Keyboard.Key.Up:
                    menu?.MoveUp();
                    break;
                case Keyboard.Key.Down:
                    menu?.MoveDown();
                    break;
                case Keyboard.Key.Space:
                case Keyboard.Key.Return:
                    menu?.SelectItem();
                    break;
            }
        }
    }
}
