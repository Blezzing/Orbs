using SFML.Graphics;
using SFML.System;

namespace Orbs
{
    public abstract class Character : Drawable
    {
        protected bool isMoving = false;                        //Flag for knowing if it should take input
        protected float speed = 2f;                             //Tiles/sec
        protected Direction facingDirection = Direction.Down;   //If moving, what direction? and if not, where are the character facing?
        private float distanceMoved;                            //How far have the character already moved? unit is pixels, checked against tileworldsize                        
        protected Clock updateTimer;                            //Timer to keep track of update deltatime
        protected Sprite sprite;                                  //Sprite of the character

        protected TileMap map;                                  //What map is the character currently on
        public Vector2i Position;                               //Where on the tilemap is the character currently? updated on end of movement from tile to tile
        public Vector2f SpritePosition                          //Where is the sprite of the character for camera following
        {
            get
            {
                return sprite.Position;
            }
        }

        public Character(TileMap map, Vector2i initialPosition)
        {
            //Bind map
            this.map = map;
            this.map.Tiles[initialPosition.X, initialPosition.Y].Enter(this);

            //Prepare the sprite
            sprite = new Sprite();
            sprite.Texture = new Texture("Assets/Textures/playerTemp.png");
            sprite.Origin = sprite.Texture.Center();
            sprite.Position = new Vector2f(Position.X, Position.Y) * Tile.WorldSize + new Vector2f(Tile.WorldSize, Tile.WorldSize) / 2;

            //Start the updateTimer
            updateTimer = new Clock();
        }

        public virtual void Update()
        {
            Decide();
            UpdateMovement();
            updateTimer.Restart();
        }

        protected void UpdateMovement()
        {
            if (isMoving)
            {
                //Distance according to speed(tiles/sec) and time.
                float distance = speed * (Tile.WorldSize * updateTimer.ElapsedTime.AsMicroseconds()) / 1000000;

                //Apply movement on sprite
                if (facingDirection == Direction.Up)
                {
                    sprite.Position += new Vector2f(0, -distance);
                }
                else if (facingDirection == Direction.Right)
                {
                    sprite.Position += new Vector2f(distance, 0);
                }
                else if (facingDirection == Direction.Down)
                {
                    sprite.Position += new Vector2f(0, distance);
                }
                else if (facingDirection == Direction.Left)
                {
                    sprite.Position += new Vector2f(-distance, 0);
                }

                //Moved an entire tile yet?
                distanceMoved += distance;
                if (distanceMoved >= Tile.WorldSize)
                {
                    //Prepare values for next movement
                    distanceMoved = 0;
                    isMoving = false;

                    if (facingDirection == Direction.Up)
                    {
                        map.Tiles[Position.X, Position.Y].Leave();
                        map.Tiles[Position.X, Position.Y - 1].Enter(this);
                    }
                    else if (facingDirection == Direction.Right)
                    {
                        map.Tiles[Position.X, Position.Y].Leave();
                        map.Tiles[Position.X + 1, Position.Y].Enter(this);
                    }
                    else if (facingDirection == Direction.Down)
                    {
                        map.Tiles[Position.X, Position.Y].Leave();
                        map.Tiles[Position.X, Position.Y + 1].Enter(this);
                    }
                    else if (facingDirection == Direction.Left)
                    {
                        map.Tiles[Position.X, Position.Y].Leave();
                        map.Tiles[Position.X - 1, Position.Y].Enter(this);
                    }

                    //Hard set to avoid drifting
                    sprite.Position = new Vector2f(Position.X, Position.Y) * Tile.WorldSize + new Vector2f(Tile.WorldSize, Tile.WorldSize) / 2;
                }
            }
        }

        protected virtual void Decide()
        {
        }

        protected bool CanEnterFacingTile()
        {
            //Check for collidable tiles and map borders;
            if (facingDirection == Direction.Up && Position.Y >= 1)
            {
                return map.Tiles[Position.X, Position.Y - 1].CanEnter;
            }
            else if (facingDirection == Direction.Right && Position.X < map.WorldWidth - 1)
            {
                return map.Tiles[Position.X + 1, Position.Y].CanEnter;
            }
            else if (facingDirection == Direction.Down && Position.Y < map.WorldHeight - 1)
            {
                return map.Tiles[Position.X, Position.Y + 1].CanEnter;
            }
            else if (facingDirection == Direction.Left && Position.X >= 1)
            {
                return map.Tiles[Position.X - 1, Position.Y].CanEnter;
            }
            return false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}