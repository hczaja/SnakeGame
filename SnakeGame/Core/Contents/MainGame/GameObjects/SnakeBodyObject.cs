using Engine.Core;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class SnakeBodyObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly RectangleShape Rectangle;

        private static readonly float BODY_WIDTH = 32f;
        private static readonly float BODY_HEIGHT = 32f;

        public SnakeBodyObject(int x, int y)
        {
            this.X = x;
            this.Y = y;

            this.Rectangle = new RectangleShape()
            {
                Size = new(BODY_WIDTH, BODY_HEIGHT),
                Position = new(x * BODY_WIDTH, y * BODY_HEIGHT),
                Texture = new Texture("Assets/SnakeBody.png")
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {
            this.Rectangle.Position = new(
                BODY_WIDTH * this.X,
                BODY_HEIGHT * this.Y);
        }

        internal void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
