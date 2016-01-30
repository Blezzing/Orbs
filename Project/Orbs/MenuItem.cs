using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class MenuItem
    {
        public Text Label;
        private bool selected;
        public Action Pick;
        
        private static Color idleColor = Color.White;
        private static Color activeColor = Color.Red;

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                if (value)
                    Label.Color = activeColor;
                else
                    Label.Color = idleColor;
            }
        }

        public MenuItem(Text label, Action pickAction)
        {
            Label = label;
            Pick = pickAction;
            Selected = false;
        }

        public void Draw()
        {
            if (selected)
            {

            }
            Label.Draw(Program.Window, RenderStates.Default);
        }
    }
}
