using System;
using SFML.Graphics;
using SFML.System;

namespace Orbs
{
    public class Tile : Drawable
    {
        private static int[] collidables = { 10, 11, 12, 30, 31, 32, 50, 51, 52 };

        public static float TextureSize = 128; //tile size on sheet
        public static float WorldSize = 128;   //tile size on screen
        private static uint tilesPerTileSetLine = 20;

        private int iD;
        private int x;
        private int y;
        
        private VertexArray vertexArray = new VertexArray(PrimitiveType.Quads, 4);
        
        public bool isCollidable;

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
                BindTexture();
            }
        }
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                BindTexture();
            }
        }
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
                BindTexture();
            }
        }

        public Tile(int ID, int x, int y) : this(ID, x, y, isCollidableByDefault(ID))
        {
        }

        public Tile(int ID, int x, int y, bool isCollidable)
        {
            this.ID = ID;
            this.X = x;
            this.Y = y;
            this.isCollidable = isCollidable;
            this.BindTexture();
        }

        private static bool isCollidableByDefault(int queryID)
        {
            foreach (int id in collidables)
                if (id == queryID)
                    return true;
            return false;
        }


        private void BindTexture()
        {
            vertexArray.Clear();
            // !#
            // ##
            vertexArray.Append(new Vertex(
                                    new Vector2f(X * WorldSize, Y * WorldSize),
                                    new Vector2f(ID % tilesPerTileSetLine * TextureSize, (ID / tilesPerTileSetLine) * TextureSize)));

            // #!
            // ##
            vertexArray.Append(new Vertex(
                                    new Vector2f((X + 1) * WorldSize, Y * WorldSize),
                                    new Vector2f((ID % tilesPerTileSetLine + 1) * TextureSize, (ID / tilesPerTileSetLine) * TextureSize)));

            // ##
            // #!
            vertexArray.Append(new Vertex(
                                    new Vector2f((X + 1) * WorldSize, (Y + 1) * WorldSize),
                                    new Vector2f((ID % tilesPerTileSetLine + 1) * TextureSize, ((ID / tilesPerTileSetLine) + 1) * TextureSize)));

            // ##
            // !#
            vertexArray.Append(new Vertex(
                                    new Vector2f(X * WorldSize, (Y + 1) * WorldSize),
                                    new Vector2f(ID % tilesPerTileSetLine * TextureSize, ((ID / tilesPerTileSetLine) + 1) * TextureSize)));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(vertexArray, states);
        }
    }
}