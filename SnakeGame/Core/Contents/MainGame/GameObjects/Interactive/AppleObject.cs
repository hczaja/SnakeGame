using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Interactive
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

        private static readonly Texture RED_APPLE_TEXTURE = new Texture("Assets/Apples/apple_red.png");
        private static readonly Texture YELLOW_APPLE_TEXTURE = new Texture("Assets/Apples/apple_yellow.png");
        private static readonly Texture GREEN_APPLE_TEXTURE = new Texture("Assets/Apples/apple_green.png");

        public AppleObject(int x, int y, AppleType type = AppleType.Red)
        {
            X = x;
            Y = y;

            Type = type;

            Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = GetTexture()
            };
        }

        private Texture GetTexture() => Type switch
        {
            AppleType.Red => RED_APPLE_TEXTURE,
            AppleType.Yellow => YELLOW_APPLE_TEXTURE,
            AppleType.Green => GREEN_APPLE_TEXTURE,
            _ => throw new NotImplementedException(),
        };

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
