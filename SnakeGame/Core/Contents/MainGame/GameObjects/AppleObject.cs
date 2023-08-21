using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class AppleObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly RectangleShape Rectangle;

        public AppleObject(int x, int y)
        {
            this.Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture("Assets/Apple.png")
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
