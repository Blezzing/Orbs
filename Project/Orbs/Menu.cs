using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public class Menu
    {
        List<MenuItem> menuItems;
        int selectedItem = 0;

        public Menu(List<MenuItem> items,Vector2f topLeft, float height)
        {
            //get items
            menuItems = items;

            //define positions
            for (int i = 0; i<menuItems.Count;i++)
            {
                menuItems[i].Label.Position = new Vector2f(topLeft.X, topLeft.Y + (height / menuItems.Count) * i);
            }

            menuItems[selectedItem].Selected = true;
        }

        public void MoveUp()
        {
            if (selectedItem > 0)
            {
                menuItems[selectedItem--].Selected = false;
                menuItems[selectedItem].Selected = true;
            }
        }

        public void MoveDown()
        {
            if (selectedItem < menuItems.Count - 1)
            {
                menuItems[selectedItem++].Selected = false;
                menuItems[selectedItem].Selected = true;
            }
        }

        public void SelectItem()
        {
            menuItems[selectedItem].Pick();
        }

        public void Draw()
        {
            foreach (MenuItem item in menuItems)
            {
                item.Draw();
            }
        }
    }
}
