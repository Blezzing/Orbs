using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    public static class LibraryExtensions
    {
        public static SFML.System.Vector2f Center(this SFML.Graphics.Texture texture)
        {
            return new SFML.System.Vector2f(texture.Size.X / 2, texture.Size.Y / 2);
        }
    }
}
