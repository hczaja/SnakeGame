using Engine.Actors;
using Engine.Graphics;
using Engine.Time;
using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Contents.MainGame.GameObjects.Other;
using SnakeGame.Core.Contents.MainGame.Levels;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Enemies;

internal abstract class EnemyObject : GameActor
{
    private readonly GameStopwatch _stopwatch;

    public override int X { get; protected set; }
    public override int Y { get; protected set; }

    protected HealthObject Health { get; set; }

    protected abstract Animation CurrentAnimation { get; set; }

    public EnemyObject(int x, int y, float actionTime)
    {
        X = x;
        Y = y;

        Health = new HealthObject(this);

        _stopwatch = new GameStopwatch(actionTime);
    }

    public override void DrawBy(RenderTarget render)
    {
        CurrentAnimation.DrawBy(render);
        Health.DrawBy(render);
    }

    public override void Update()
    {
        if (!_stopwatch.Update())
        {
            DoSomething();
            _stopwatch.Restart();
        }
    }

    protected abstract void DoSomething();

    protected virtual Vector2f GetPosition() => new(Cell.CELL_SIZE * X, Cell.CELL_SIZE * Y);
}
