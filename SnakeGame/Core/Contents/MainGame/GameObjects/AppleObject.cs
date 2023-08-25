using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal enum AppleType
    {
        Red, Green, Yellow
    }

    internal class AppleObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private AppleType Type { get; set; }

        private readonly RectangleShape Rectangle;

        public AppleObject(int x, int y, AppleType type = AppleType.Red)
        {
            this.Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture("Assets/Apple.png")
            };

            Type = type;
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {

        }

        internal bool IsRed() => Type == AppleType.Red;
        internal bool IsGreen() => Type == AppleType.Green;
        internal bool IsYellow() => Type == AppleType.Yellow;
    }
}
