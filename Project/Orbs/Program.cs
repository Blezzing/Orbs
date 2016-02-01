using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public static class Program
    {
        #region Fields
        private static RenderWindow window;
        private static AssetManager assetManager;
        private static StateManager stateManager;
        private static Color clearColor = Color.Black;
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
            window = new RenderWindow(new VideoMode(1920,1080), "Orbs", Styles.Fullscreen);

            //Set parameters
            window.SetVisible(true);
            window.SetMouseCursorVisible(false);

            //Bind events
            window.Closed += (sender, i) => window.Close();
            window.KeyPressed += (sender, i) => stateManager.CurrentState?.HandleKeyPressed(i);
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
                window.Clear(clearColor);

                //Draw frame
                stateManager.CurrentState?.Render();

                //Swap frame   
                window.Display();
            }
        }
        #endregion
    }
}
