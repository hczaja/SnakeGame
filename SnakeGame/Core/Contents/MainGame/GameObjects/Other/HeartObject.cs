using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Other
{
    internal class HeartObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool IsFull
        {
            get => IsFull;
            private set
            {
                IsFull = value;
                Rectangle.Texture = IsFull
                    ? HEART_FULL_TEXTURE : HEART_EMPTY_TEXTURE;
            }
        }

        private readonly RectangleShape Rectangle;
        private static readonly Texture HEART_FULL_TEXTURE = new Texture("Assets/HeartFull.png");
        private static readonly Texture HEART_EMPTY_TEXTURE = new Texture("Assets/HeartEmpty.png");

        public HeartObject(IGameObject parent)
        {
            this.X = parent.X;
            this.Y = parent.Y;

            this.Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(X * Cell.CELL_SIZE, Y * Cell.CELL_SIZE - 12f),
                Texture = HEART_EMPTY_TEXTURE
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {

        }
    }
}
