using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Orbs
{
    public class Character : Drawable
    {
        private bool isMoving = false;                      //flag for knowing if it should take input
        private float speed = 2f;                           //tiles/sec
        private Direction facingDirection = Direction.Down; //if moving, what direction? and if not, where are the character facing?
        private float alreadyMoved;                         //how far have the character already moved? unit is pixels, checked against tileworldsize                        
        private Clock updateTimer;                          //timer to keep track of update deltatime
        private Sprite sprite;                              //sprite of the character

        private TileMap map;                                //what map is the character currently on
        public Vector2i Position = new Vector2i(10,10);     //where on the tilemap is the character currently? updated on end of movement from tile to tile

        public Vector2f DrawPosition                        //where is the sprite of the character for camera following
        {
            get
            {
                return sprite.Position;
            }
        }                     
        
        public Character(TileMap map)
        {
            //Prepare the sprite
            sprite = new Sprite();
            sprite.Texture = new Texture("Assets/Textures/playerTemp.png");
            sprite.Origin = sprite.Texture.Center();
            sprite.Position = new Vector2f(Position.X, Position.Y) * Tile.WorldSize + new Vector2f(Tile.WorldSize, Tile.WorldSize) / 2;

            //bind map
            this.map = map;

            //Start the updateTimer
            updateTimer = new Clock();
        }

        public void Update()
        {
            HandleInput();
            Act();
            updateTimer.Restart();
        }

        private void Act()
        {
            if (isMoving)
            {
                if (IsCollisionInFacingDirection())
                {
                    isMoving = false;
                    return;
                }

                float move = speed * (Tile.WorldSize * updateTimer.ElapsedTime.AsMicroseconds()) / 1000000;

                //move player sprite
                alreadyMoved += move;

                switch (facingDirection)
                {
                    case (Direction.Up):
                        sprite.Position += new Vector2f(0, -move);
                        break;
                    case (Direction.Right):
                        sprite.Position += new Vector2f(move, 0);
                        break;
                    case (Direction.Down):
                        sprite.Position += new Vector2f(0, move);
                        break;
                    case (Direction.Left):
                        sprite.Position += new Vector2f(-move, 0);
                        break;
                }
                //there? stop
                if (alreadyMoved >= Tile.WorldSize)
                {
                    switch (facingDirection)
                    {
                        case (Direction.Up):
                            Position.Y -= 1;
                            break;
                        case (Direction.Right):
                            Position.X += 1;
                            break;
                        case (Direction.Down):
                            Position.Y += 1;
                            break;
                        case (Direction.Left):
                            Position.X -= 1;
                            break;
                    }

                    sprite.Position = new Vector2f(Position.X, Position.Y) * Tile.WorldSize + new Vector2f(Tile.WorldSize, Tile.WorldSize)/2;
                    alreadyMoved = 0;
                    isMoving = false;
                }
            }
        }

        private void HandleInput()
        {
            if (!isMoving)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    facingDirection = Direction.Up;
                    isMoving = true;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                {
                    facingDirection = Direction.Right;
                    isMoving = true;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                    facingDirection = Direction.Down;
                    isMoving = true;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                {
                    facingDirection = Direction.Left;
                    isMoving = true;
                }
            }
        }

        private bool IsCollisionInFacingDirection()
        {
            try 
            {
                switch (facingDirection)
                {
                    case (Direction.Up):
                        return map.Tiles[Position.X, Position.Y - 1].isCollidable;
                    case (Direction.Right):
                        return map.Tiles[Position.X + 1, Position.Y].isCollidable;
                    case (Direction.Down):
                        return map.Tiles[Position.X, Position.Y + 1].isCollidable;
                    case (Direction.Left):
                        return map.Tiles[Position.X - 1, Position.Y].isCollidable;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }

            //Should never be reached
            return true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}