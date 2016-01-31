using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class MenuItem : Drawable
    {
        public Text Label;
        public Action Pick;

        private static Color selectedColor = Color.Red;
        private static Color notSelectedColor = Color.White;

        public bool Selected
        {
            set
            {
                if (value)
                {
                    Label.Color = selectedColor;
                }
                else
                {
                    Label.Color = notSelectedColor;
                }
            }
        }

        public MenuItem(Text label, Action pickAction)
        {
            Label = label;
            Label.Color = notSelectedColor;
            Pick = pickAction;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Label);
        }
    }
}
