using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Orbs
{
    public class Character : Drawable
    {
        private Vector2f inputDirection = new Vector2f(0,0);
        public float Speed = 0.0001f;

        Sprite sprite;

        public Vector2f Position
        {
            get
            {
                return sprite.Position;
            }
        }


        public Character()
        {
            sprite = new Sprite(new Texture("Assets/Textures/playerTemp.png"));
            sprite.Origin = sprite.Texture.Center();
        }

        public void Move(Time deltaTime)
        {
            sprite.Position += inputDirection* Speed * deltaTime.AsMicroseconds();
        }

        public void Update(Time deltaTime)
        {
            HandleInput();
            Move(deltaTime);           
        }

        private void HandleInput()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                inputDirection.Y = -1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                inputDirection.Y = 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                inputDirection.X = 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                inputDirection.X = -1f;
            if (!Keyboard.IsKeyPressed(Keyboard.Key.Up) && !Keyboard.IsKeyPressed(Keyboard.Key.Down))
                inputDirection.Y = 0f;
            if (!Keyboard.IsKeyPressed(Keyboard.Key.Right) && !Keyboard.IsKeyPressed(Keyboard.Key.Left))
                inputDirection.X = 0f;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}