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
        private Sprite splashSprite;
        private Text titleText;
        
        public SplashScreenState()
        {
            Color renderColor = new Color(200, 200, 255);

            //Create the orb
            Texture splashTexture = new Texture("Assets/Textures/splashOrb.png");
            splashTexture.Smooth = true;
            splashSprite = new Sprite(splashTexture);
            splashSprite.Color = renderColor;
            splashSprite.Origin = splashTexture.Center();
            splashSprite.Position = new SFML.System.Vector2f(Program.Window.Size.X/2, (Program.Window.Size.Y/5)*3);

            //Create the title label
            Font titleFont = new Font("Assets/Fonts/Base.ttf");
            titleText = new Text("ORBS", titleFont, 140);
            titleText.Color = renderColor;
            titleText.Position = new SFML.System.Vector2f(Program.Window.Size.X/2-titleText.GetLocalBounds().Width/2, 100);
        }

        public void Update()
        {
        }

        public void Render()
        {
            Program.Window.Clear(Color.Black);
            splashSprite.Draw(Program.Window, RenderStates.Default);
            titleText.Draw(Program.Window, RenderStates.Default);
        }

        public void HandleKeyPressed(KeyEventArgs i)
        {
            EnterMainMenu();
        }

        public void HandleMouseReleased(MouseButtonEventArgs i)
        {
            EnterMainMenu();
        }

        private void EnterMainMenu()
        {
            Program.StateManager?.EnterState(new MainMenuState());
        }
    }
}
