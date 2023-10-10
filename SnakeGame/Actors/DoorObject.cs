using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using Snakeventures.Levels;

namespace Snakeventures.Actors
{
    internal class DoorObject : GameActor
    {
        public override int X { get; protected set; }
        public override int Y { get; protected set; }

        private readonly RectangleShape Shape;

        private bool IsClosed { get; set; } = true;

        private static readonly Texture OPENED_TEXTURE = new Texture("Assets/Doors/door_opened.png");
        private static readonly Texture CLOSED_TEXTURE = new Texture("Assets/Doors/door_closed.png");

        public DoorObject(int x, int y)
        {
            Shape = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = CLOSED_TEXTURE
            };
        }

        public override void DrawBy(RenderTarget render)
        {
            render.Draw(Shape);
        }

        public override void Update()
        {

        }

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
