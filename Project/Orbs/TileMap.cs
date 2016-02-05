using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Linq;

namespace Orbs
{
    class TileMap : Drawable
    {
        private Texture tileSet;
        private uint tilesPerTileSetLine = 20;

        private VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);

        private uint worldWidth;  //in tiles
        private uint worldHeight; //in tiles

        private float textureTileSize = 64; //Tile size on sheet
        private float worldTileSize = 32;   //Tile size on screen

        private int[,] tiles;
        private VertexArray[,] chunks;

        //Should not be hardcoded!
        private string mapFile = "Assets/Maps/TestMap.csv";
        private string tileSheetFile = "Assets/Textures/mapSpriteSheet64px.png";

        public TileMap()
        {

            this.tileSet = new Texture(tileSheetFile);
            
            //Fill in tilearray from a csv file. no error handling at all MUST BE FIXED
            string[] lines = File.ReadAllLines(mapFile);
            worldHeight = (uint)lines.Count();
            worldWidth = (uint)lines[0].Split(',').ToArray().Count();

            tiles = new int[worldWidth, worldWidth];
            
            for (int i = 0; i < worldHeight; i++)
            {
                string[] values = lines[i].Split(',').ToArray();

                for (int j = 0; j< worldWidth; j++)
                {
                    tiles[i,j] = int.Parse(values[j]);
                }
            }

            //If we made it this far, hurray!.. that filehandling sucked.

            //time to hook this shit up.
            vertexArray.Resize(worldWidth * worldHeight * 4);
            for (uint i = 0; i < worldHeight; i++)
            {
                for (uint j = 0; j < worldWidth; j++)
                {
                    BindVerticesToTexture(j, i, tiles[i, j]);
                }
            }
                        
            Console.WriteLine("yay");
        }
        
        public void Update(Vector2f viewPosition)
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = tileSet;
            target.Draw(vertexArray,states);
        }

        private void BindVerticesToTexture(uint x, uint y, int tile)
        {
            // !#
            // ##
            vertexArray.Append(new Vertex(
                                    new Vector2f(x * worldTileSize, y * worldTileSize),
                                    new Vector2f(tile % tilesPerTileSetLine * textureTileSize, (tile / tilesPerTileSetLine) * textureTileSize)));

            // #!
            // ##
            vertexArray.Append(new Vertex(
                                    new Vector2f((x + 1) * worldTileSize, y * worldTileSize),
                                    new Vector2f((tile % tilesPerTileSetLine + 1) * textureTileSize, (tile / tilesPerTileSetLine) * textureTileSize)));

            // ##
            // #!
            vertexArray.Append(new Vertex(
                                    new Vector2f((x + 1) * worldTileSize, (y + 1) * worldTileSize),
                                    new Vector2f((tile % tilesPerTileSetLine + 1) * textureTileSize, ((tile / tilesPerTileSetLine) + 1) * textureTileSize)));

            // ##
            // !#
            vertexArray.Append(new Vertex(
                                    new Vector2f(x * worldTileSize, (y + 1) * worldTileSize),
                                    new Vector2f(tile % tilesPerTileSetLine * textureTileSize, ((tile / tilesPerTileSetLine) + 1) * textureTileSize)));
        }
    }
}
