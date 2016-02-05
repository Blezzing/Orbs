using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Linq;

namespace Orbs
{
    public class TileMap : Drawable
    {
        private Texture tileSet;
        private VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);

        private uint worldWidth;  //in tiles
        private uint worldHeight; //in tiles

        private static int tileDrawRangeWidth = 32;
        private static int tileDrawRangeHeight = 19;

        private Tile[,] tiles;
        private Vector2i drawingPoint = new Vector2i();

        //Should not be hardcoded!
        private string mapFile = "Assets/Maps/TestMapBig.csv";
        private string tileSheetFile = "Assets/Textures/mapSpriteSheet128px.png";

        #region Properties
        public Tile[,] Tiles
        {
            get
            {
                return tiles;
            }
        }

        public uint WorldWidth
        {
            get
            {
                return worldWidth;
            }
        }

        public uint WorldHeight
        {
            get
            {
                return worldHeight;
            }
        }
        #endregion

        public TileMap()
        {
            LoadTileSheet();
            LoadTileMap();
        }

        private void LoadTileSheet()
        {
            //load texture for use
            tileSet = new Texture(tileSheetFile);
        }

        private void LoadTileMap()
        {
            //load all lines
            string[] lines = File.ReadAllLines(mapFile);

            //define world dimensions
            worldHeight = (uint)lines.Count();
            worldWidth = (uint)lines[0].Split(',').ToArray().Count();

            //populate tiles
            tiles = new Tile[worldWidth, worldWidth];
            for (int i = 0; i < worldHeight; i++)
            {
                string[] values = lines[i].Split(',').ToArray();

                for (int j = 0; j < worldWidth; j++)
                {
                    tiles[j,i] = new Tile(int.Parse(values[j]), j, i);
                }
            }
        }

        public void SetDrawingPoint(Vector2i position)
        {
            this.drawingPoint = position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //set current texture to tileset
            states.Texture = tileSet;

            //Call draw for each tile in drawing range
            for (int x = drawingPoint.X - tileDrawRangeWidth; x < drawingPoint.X + tileDrawRangeWidth; x++)
            {
                for (int y = drawingPoint.Y - tileDrawRangeHeight; y < drawingPoint.Y + tileDrawRangeHeight; y++)
                {
                    if (x >= 0 && y >= 0 && x < worldWidth && y < worldHeight)
                    {
                        target.Draw(tiles[x, y], states);
                    }
                }
            }
        }
    }
}
