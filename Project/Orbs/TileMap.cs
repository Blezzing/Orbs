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

        private uint worldWidth;  //In tiles
        private uint worldHeight; //In tiles

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
            //Load texture for use
            tileSet = new Texture(tileSheetFile);
        }

        private void LoadTileMap()
        {
            //Load all lines
            string[] lines = File.ReadAllLines(mapFile);

            //Define world dimensions
            worldHeight = (uint)lines.Count();
            worldWidth = (uint)lines[0].Split(',').ToArray().Count();

            //Populate tiles
            tiles = new Tile[worldWidth, worldWidth];
            for (int i = 0; i < worldHeight; i++)
            {
                string[] values = lines[i].Split(',').ToArray();

                for (int j = 0; j < worldWidth; j++)
                {
                    tiles[j,i] = new Tile(int.Parse(values[j]), new Vector2i(j, i));
                }
            }
        }

        public void SetDrawingPoint(Vector2i position)
        {
            this.drawingPoint = position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //Set current texture to tileset
            states.Texture = tileSet;

            //Loop variable preparation to perform fewer operations while iterating
            int xLow    = drawingPoint.X - tileDrawRangeWidth > 0                   ? drawingPoint.X - tileDrawRangeWidth   : 0;
            int xHigh   = drawingPoint.X + tileDrawRangeWidth > (int)worldWidth     ? (int)worldWidth                       : drawingPoint.X + tileDrawRangeWidth;
            int yLow    = drawingPoint.Y - tileDrawRangeHeight > 0                  ? drawingPoint.Y - tileDrawRangeHeight  : 0;
            int yHigh   = drawingPoint.Y + tileDrawRangeHeight > (int)worldHeight   ? (int)worldHeight                      : drawingPoint.Y + tileDrawRangeHeight;
            
            //Draw relevant tiles
            for (int x = xLow;  x < xHigh; x++)
            {
                for (int y = yLow; y < yHigh; y++)
                {
                    target.Draw(tiles[x, y], states);
                }
            }
        }
    }
}
