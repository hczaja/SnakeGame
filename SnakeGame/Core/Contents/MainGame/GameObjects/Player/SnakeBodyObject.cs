using Engine.Core;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Player
{
    internal class SnakeBodyObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly RectangleShape Rectangle;

        public SnakeBodyObject(int x, int y)
        {
            X = x;
            Y = y;

            Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture("Assets/SnakeBody.png")
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {
            Rectangle.Position = new(
                Cell.CELL_SIZE * X,
                Cell.CELL_SIZE * Y);
        }

        internal void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
