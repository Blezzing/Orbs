using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Orbs
{
    class SplashScreenState : IState
    {
        private Sprite splashSprite;
        private Text titleText;
        private Color renderColor = new Color(200, 200, 255);
        
        public SplashScreenState()
        {
            //Create the orb
            splashSprite = new Sprite(new Texture("Assets/Textures/splashOrb.png") { Smooth = true });
            splashSprite.Color = renderColor;
            splashSprite.Origin = splashSprite.Texture.Center();
            splashSprite.Position = new SFML.System.Vector2f(Program.Window.Size.X / 2, (Program.Window.Size.Y / 5) * 3);

            //Create the title label
            titleText = new Text("ORBS", new Font("Assets/Fonts/Base.ttf"), 140);
            titleText.Color = renderColor;
            titleText.Position = new Vector2f(Program.Window.Size.X / 2 - titleText.GetLocalBounds().Width / 2, 100);
        }

        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Draw(splashSprite);
            Program.Window.Draw(titleText);
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            //On any key - enter main menu
            Program.StateManager.EnterState(new MainMenuState());
        }
    }
}
