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
        private static StateManager stateManager;
        private static RenderWindow window;
        private static Color clearColor = new Color(100, 150, 240);
        #endregion

        #region Properties
        public static StateManager StateManager
        {
            get
            {
                return stateManager;
            }
        }
        public static RenderWindow Window
        {
            get
            {
                return window;
            }
        }
        #endregion

        private static void Main(string[] args)
        {
            InitializeWindow();
            InitializeStateManager();
            GameLoop();
        }

        #region Private methods
        private static void InitializeStateManager()
        {
            stateManager = new StateManager();
            stateManager.EnterState(new SplashScreenState());

            stateManager.Empty += (sender, i) => window?.Close();
        }

        private static void InitializeWindow()
        {
            //define the window
            window = new RenderWindow(new VideoMode(1440, 900), "Orbs", Styles.Default);
            window.SetVisible(true);

            //setup eventhandlers
            window.Closed += (sender, i) => window?.Close();
            window.KeyPressed += (sender, i) => stateManager?.CurrentState?.HandleKeyPressed(i);
        }

        private static void GameLoop()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                stateManager.CurrentState?.Update();
                stateManager.CurrentState?.Render();
                window.Display();
            }
        }
        #endregion
    }
}
