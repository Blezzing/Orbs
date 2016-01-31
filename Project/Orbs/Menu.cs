using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class Menu : Drawable
    {
        private List<MenuItem> menuItems;
        private int selectedItem = 0;
        private Sprite selectionPointer;

        public Menu(List<MenuItem> items,Vector2f topLeft, float height)
        {
            //get items
            menuItems = items;

            //define positions
            for (int i = 0; i<menuItems.Count;i++)
            {
                menuItems[i].Label.Position = new Vector2f(topLeft.X, topLeft.Y + (height / menuItems.Count) * i);
            }

            //prepare pointer
            uint pointerSize = menuItems[selectedItem].Label.CharacterSize;
            selectionPointer = new Sprite(new Texture("Assets/Textures/arrowRight.png") { Smooth=true });
            selectionPointer.Scale = new Vector2f(0.04f*pointerSize, 0.04f*pointerSize);
            selectionPointer.Color = Color.Red;

            //preset first selection
            menuItems[selectedItem].Selected = true;
            UpdatePointerPosition();
        }

        public void MoveUp()
        {
            if (selectedItem > 0)
            {
                menuItems[selectedItem--].Selected = false;
                menuItems[selectedItem].Selected = true;
                UpdatePointerPosition();
            }
        }

        public void MoveDown()
        {
            if (selectedItem < menuItems.Count - 1)
            {
                menuItems[selectedItem++].Selected = false;
                menuItems[selectedItem].Selected = true;
                UpdatePointerPosition();
            }
        }

        public void SelectItem()
        {
            menuItems[selectedItem].Pick();
        }

        private void UpdatePointerPosition()
        {
            selectionPointer.Position = menuItems[selectedItem].Label.Position - new Vector2f(20+menuItems[selectedItem].Label.CharacterSize, -menuItems[selectedItem].Label.CharacterSize/6);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (MenuItem item in menuItems)
            {
                target.Draw(item);
            }

            target.Draw(selectionPointer);
        }
    }
}
