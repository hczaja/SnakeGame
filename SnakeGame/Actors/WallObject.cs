using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using Snakeventures.Levels;

namespace Snakeventures.Actors
{
    internal class WallObject : GameActor
    {
        public override int X { get; protected set; }
        public override int Y { get; protected set; }

        private readonly RectangleShape Shape;

        public WallObject(int x, int y)
        {
            Shape = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture("Assets/Wall.png")
            };
        }

        public override void DrawBy(RenderTarget render)
        {
            //render.Draw(Shape);
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
