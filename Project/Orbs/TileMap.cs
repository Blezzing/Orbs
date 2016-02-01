using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbs
{
    class TileMap : Drawable
    {
        private Texture tileSet;
        private uint tilesPerTileSetLine = 20;

        private VertexArray sanity = new VertexArray(PrimitiveType.Quads, 4);

        private VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);
        private RenderStates vertexState = new RenderStates();

        private uint worldWidth;  //in tiles
        private uint worldHeight; //in tiles

        private float textureTileSize = 128; //Tile size on sheet
        private float worldTileSize = 64;   //Tile size on screen

        private int[,] tiles;

        //Should not be hardcoded!
        private string mapFile = "Assets/Maps/TestMap.csv";
        private string tileSheetFile = "Assets/Textures/mapSpriteSheet128px.png";

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
                    // !#
                    // ##
                    vertexArray.Append(new Vertex(
                                            new Vector2f(j * worldTileSize, i * worldTileSize),
                                            //new Vector2f(0,0)));
                                            new Vector2f(tiles[i, j] % tilesPerTileSetLine * textureTileSize, (tiles[i, j] / tilesPerTileSetLine) * textureTileSize)));

                    // #!
                    // ##
                    vertexArray.Append(new Vertex(
                                            new Vector2f((j + 1) * worldTileSize, i * worldTileSize),
                                            //new Vector2f(textureTileSize, 0)));
                                            new Vector2f((tiles[i, j] % tilesPerTileSetLine + 1) * textureTileSize, (tiles[i, j] / tilesPerTileSetLine) * textureTileSize)));

                    // ##
                    // #!
                    vertexArray.Append(new Vertex(
                                            new Vector2f((j + 1) * worldTileSize, (i + 1) * worldTileSize),
                                            //new Vector2f(textureTileSize, textureTileSize)));
                                            new Vector2f((tiles[i, j] % tilesPerTileSetLine + 1) * textureTileSize, ((tiles[i, j] / tilesPerTileSetLine) + 1) * textureTileSize)));

                    // ##
                    // !#
                    vertexArray.Append(new Vertex(
                                            new Vector2f(j * worldTileSize, (i + 1) * worldTileSize),
                                            //new Vector2f(0, textureTileSize)));
                                            new Vector2f(tiles[i, j] % tilesPerTileSetLine * textureTileSize, ((tiles[i, j] / tilesPerTileSetLine) + 1) * textureTileSize)));
                }
            }
                        
            Console.WriteLine("yay");
        }
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = tileSet;
            target.Draw(vertexArray,states);
        }
    }
}
