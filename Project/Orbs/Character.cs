using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Orbs
{
    public class Character : Drawable
    {
        private bool isMoving = false;                      //Flag for knowing if it should take input
        private float speed = 2f;                           //Tiles/sec
        private Direction facingDirection = Direction.Down; //If moving, what direction? and if not, where are the character facing?
        private float alreadyMoved;                         //How far have the character already moved? unit is pixels, checked against tileworldsize                        
        private Clock updateTimer;                          //Timer to keep track of update deltatime
        private Sprite sprite;                              //Sprite of the character

        private TileMap map;                                //What map is the character currently on
        public Vector2i Position = new Vector2i(10,10);     //Where on the tilemap is the character currently? updated on end of movement from tile to tile
        public Vector2f DrawPosition                        //Where is the sprite of the character for camera following
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

            //Bind map
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
                //Check if there's an obstacle on the desired tile
                if (IsCollisionInFacingDirection())
                {
                    isMoving = false;
                    return;
                }

                //Distance according to speed(tiles/sec) and time.
                float distance = speed * (Tile.WorldSize * updateTimer.ElapsedTime.AsMicroseconds()) / 1000000;

                //Apply movement on sprite
                switch (facingDirection)
                {
                    case (Direction.Up):
                        sprite.Position += new Vector2f(0, -distance);
                        break;
                    case (Direction.Right):
                        sprite.Position += new Vector2f(distance, 0);
                        break;
                    case (Direction.Down):
                        sprite.Position += new Vector2f(0, distance);
                        break;
                    case (Direction.Left):
                        sprite.Position += new Vector2f(-distance, 0);
                        break;
                }

                //Moved an entire tile yet?
                alreadyMoved += distance;
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

                    //Hard set to avoid drifting
                    sprite.Position = new Vector2f(Position.X, Position.Y) * Tile.WorldSize + new Vector2f(Tile.WorldSize, Tile.WorldSize)/2;

                    //Prepare values for next movement
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

            //Qol while debugging
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                speed = 20;
            else
                speed = 2;
        }

        private bool IsCollisionInFacingDirection()
        {
            //Check for collidable tiles and map borders;
            if (facingDirection == Direction.Up && Position.Y >= 1)
            {
                return map.Tiles[Position.X, Position.Y - 1].isCollidable;
            }
            else if (facingDirection == Direction.Right && Position.X < map.WorldWidth - 1)
            {
                return map.Tiles[Position.X + 1, Position.Y].isCollidable;
            }
            else if (facingDirection == Direction.Down && Position.Y < map.WorldHeight - 1)
            {
                return map.Tiles[Position.X, Position.Y + 1].isCollidable;
            }
            else if (facingDirection == Direction.Left && Position.X >= 1)
            {
                return map.Tiles[Position.X - 1, Position.Y].isCollidable;
            }
            return true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}