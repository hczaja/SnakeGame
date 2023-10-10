using Engine.Actors;
using Engine.Events;
using SFML.Graphics;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class EmptyObject : GameActor
    {
        public static EmptyObject Instance = new EmptyObject();

        public override int X { get; protected set; } = 1;
        public override int Y { get; protected set; } = 1;

        public override void CheckCollisions() { }
        public override void DrawBy(RenderTarget drawer) { }
        public override void Handle(KeyboardEvent @event) { }
        public override void Update() { }
    }
}
