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
        private Vector2i mapPosition;
        
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

        public Tile(int ID, Vector2i position) : this(ID, position, isCollidableByDefault(ID))
        {
        }

        public Tile(int ID, Vector2i position, bool isCollidable)
        {
            this.ID = ID;
            this.isCollidable = isCollidable;
            this.mapPosition = position;
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
                                    new Vector2f(mapPosition.X * WorldSize, mapPosition.Y * WorldSize),
                                    new Vector2f(ID % tilesPerTileSetLine * TextureSize, (ID / tilesPerTileSetLine) * TextureSize)));

            // #!
            // ##
            vertexArray.Append(new Vertex(
                                    new Vector2f((mapPosition.X + 1) * WorldSize, mapPosition.Y * WorldSize),
                                    new Vector2f((ID % tilesPerTileSetLine + 1) * TextureSize, (ID / tilesPerTileSetLine) * TextureSize)));

            // ##
            // #!
            vertexArray.Append(new Vertex(
                                    new Vector2f((mapPosition.X + 1) * WorldSize, (mapPosition.Y + 1) * WorldSize),
                                    new Vector2f((ID % tilesPerTileSetLine + 1) * TextureSize, ((ID / tilesPerTileSetLine) + 1) * TextureSize)));

            // ##
            // !#
            vertexArray.Append(new Vertex(
                                    new Vector2f(mapPosition.X * WorldSize, (mapPosition.Y + 1) * WorldSize),
                                    new Vector2f(ID % tilesPerTileSetLine * TextureSize, ((ID / tilesPerTileSetLine) + 1) * TextureSize)));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(vertexArray, states);
        }
    }
}