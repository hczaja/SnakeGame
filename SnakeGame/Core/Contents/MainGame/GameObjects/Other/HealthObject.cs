using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using System.Reflection;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Other;

internal class HealthObject : GameActor
{
    private readonly List<HeartObject> _hearts;

    public override int X { get; protected set; }
    public override int Y { get; protected set; }

    public HealthObject(GameActor parent)
    {
        _hearts = new List<HeartObject>();
        _hearts.Add(new HeartObject(parent));
    }

    public override void DrawBy(RenderTarget drawer)
    {
        foreach (var heart in _hearts)
        {
            heart.DrawBy(drawer);
        }
    }

    public override void CheckCollisions()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

    public override void Handle(KeyboardEvent @event)
    {
        throw new NotImplementedException();
    }
}
