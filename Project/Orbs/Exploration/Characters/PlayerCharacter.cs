using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Orbs
{
    public class PlayerCharacter : Character, Drawable
    {        
        public PlayerCharacter(TileMap map, Vector2i initialPosition) : base(map,initialPosition)
        {
        }

        protected override void Decide()
        {
            //If character is not already moving, begin movement
            if (!isMoving)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    facingDirection = Direction.Up;
                    isMoving = CanEnterFacingTile();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                {
                    facingDirection = Direction.Right;
                    isMoving = CanEnterFacingTile();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                    facingDirection = Direction.Down;
                    isMoving = CanEnterFacingTile();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                {
                    facingDirection = Direction.Left;
                    isMoving = CanEnterFacingTile();
                }
            }

            //Qol while debugging
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                speed = 40;
            else
                speed = 2;
        }
    }
}