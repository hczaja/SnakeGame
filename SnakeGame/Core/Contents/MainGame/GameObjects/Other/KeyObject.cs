using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Interactive
{
    internal enum KeyType
    {
        Bronze, Silver, Gold
    }

    internal class KeyObject : GameActor
    {
        public override int X { get; protected set; }
        public override int Y { get; protected set; }

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

        public override void DrawBy(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public override void Update() { }

        public override void CheckCollisions()
        {
            throw new NotImplementedException();
        }

        public override void Handle(KeyboardEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
