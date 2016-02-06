using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Orbs
{
    public static class Program
    {
        #region Fields
        //Key components
        private static RenderWindow window;
        private static AssetManager assetManager;
        private static StateManager stateManager;

        //Fps monitoring
        private static Clock fpsClock = new Clock();
        private static Text fpsLabel = new Text("fps", new Font("Assets/Fonts/Base.ttf"));
        private static int fpsSamples = 0;

        //Flags
        public static bool IsFpsRendering = false;
        public static bool IsVSyncEnabled = true;
        public static bool IsFullscreen = true;
        #endregion

        #region Properties
        public static RenderWindow Window
        {
            get
            {
                return window;
            }
        }
        public static AssetManager AssetManager
        {
            get
            {
                return assetManager;
            }
        }
        public static StateManager StateManager
        {
            get
            {
                return stateManager;
            }
        }
        #endregion

        private static void Main(string[] args)
        {
            InitializeWindow();         //Requires nothing
            InitializeAssetManager();   //Requires nothing
            InitializeStateManager();   //Requires window to exist, and assets to be loaded
            GameLoop();                 //Requires everything to be ready
        }
        
        #region Private methods
        private static void InitializeAssetManager()
        {
            //Construct
            assetManager = new AssetManager();
        }

        private static void InitializeStateManager()
        {
            //Construct
            stateManager = new StateManager();

            //Bind events
            stateManager.OutOfStates += (sender, i) => window?.Close();

            //Define starting state
            stateManager.EnterState(new SplashScreenState());
        }

        private static void InitializeWindow()
        {
            //Construct
            window = new RenderWindow(new VideoMode(1920, 1080), "Orbs", IsFullscreen ? Styles.Fullscreen : Styles.Default);

            //Set parameters
            window.SetVisible(true);
            window.SetMouseCursorVisible(false);
            window.SetVerticalSyncEnabled(IsVSyncEnabled);

            //Bind events
            window.Closed += (sender, i) => window.Close();
            window.KeyPressed += (sender, i) => stateManager.CurrentState?.HandleKeyPressed(i);
            window.KeyPressed += (sender, i) => { if (i.Code == Keyboard.Key.F11) { window.Close(); IsFullscreen = !IsFullscreen; InitializeWindow(); } };
        }

        private static void GameLoop()
        {
            //Terminate when window gets closed
            while (window.IsOpen)
            {
                //Walks through delegates for event handling
                window.DispatchEvents();

                //Perform logic
                stateManager.CurrentState?.Update();

                //Prepare frame
                window.Clear(Color.Black);

                //Draw frame
                stateManager.CurrentState?.Render();

                //Fps-counter
                FpsRenderer();

                //Swap frame   
                window.Display();
            }
        }

        private static void FpsRenderer()
        {
            if (IsFpsRendering)
            {
                if (++fpsSamples > 100)
                {
                    fpsLabel.DisplayedString = (1000000 / (fpsClock.ElapsedTime.AsMicroseconds() / fpsSamples)).ToString();
                    fpsSamples = 0;
                    fpsClock.Restart();
                }
                window.Draw(fpsLabel);
            }
        }
        #endregion
    }
}
