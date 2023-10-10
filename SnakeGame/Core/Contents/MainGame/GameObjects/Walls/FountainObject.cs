using Engine.Actors;
using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Walls
{
    internal class FountainObject : GameActor
    {
        public override int X { get; protected set; }
        public override int Y { get; protected set; }

        private readonly Animation Iddle;

        public FountainObject(int x, int y)
        {
            X = x;
            Y = y;

            this.Iddle = new Animation(
                new Texture("Assets/Spritesheets/spritesheet_fountain.png"),
                (int)Cell.CELL_SIZE,
                () => new(X * Cell.CELL_SIZE, Y * Cell.CELL_SIZE),
                3, 0.5f, isLooped: true);

            this.Iddle.Start();
        }

        public override void DrawBy(RenderTarget render)
        {
            Iddle.DrawBy(render);
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
