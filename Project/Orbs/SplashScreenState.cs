using SFML.Graphics;
using SFML.Window;

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
            Texture splashTexture = new Texture("Assets/Textures/splashOrb.png");
            splashTexture.Smooth = true;
            splashSprite = new Sprite(splashTexture);
            splashSprite.Color = renderColor;
            splashSprite.Origin = splashTexture.Center();
            splashSprite.Position = new SFML.System.Vector2f(Program.Window.Size.X / 2, (Program.Window.Size.Y / 5) * 3);

            //Create the title label
            Font titleFont = new Font("Assets/Fonts/Base.ttf");
            titleText = new Text("ORBS", titleFont, 140);
            titleText.Color = renderColor;
            titleText.Position = new SFML.System.Vector2f(Program.Window.Size.X / 2 - titleText.GetLocalBounds().Width / 2, 100);
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
