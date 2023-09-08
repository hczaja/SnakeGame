using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Interactive
{
    internal enum KeyType
    {
        Bronze, Silver, Gold
    }

    internal class KeyObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private KeyType Type { get; }

        private readonly RectangleShape Rectangle;

        private static readonly Texture BRONZE_KEY_TEXTURE = new Texture("Assets/Keys/Bronze_Key.png");
        private static readonly Texture SILVER_KEY_TEXTURE = new Texture("Assets/Keys/Silver_Key.png");
        private static readonly Texture GOLD_KEY_TEXTURE = new Texture("Assets/Keys/Gold_Key.png");

        public KeyObject(int x, int y, KeyType type = KeyType.Bronze)
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
            KeyType.Bronze => BRONZE_KEY_TEXTURE,
            KeyType.Silver => SILVER_KEY_TEXTURE,
            KeyType.Gold => GOLD_KEY_TEXTURE,
            _ => throw new NotImplementedException(),
        };

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update() { }
    }
}
