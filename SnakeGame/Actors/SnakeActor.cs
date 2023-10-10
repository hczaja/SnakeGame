using Engine.Actors;
using Engine.Collisions;
using Engine.Components;
using Engine.Events;
using Engine.GameObjects;
using Engine.GameState;
using Engine.Graphics;
using Engine.Time;
using SFML.Graphics;
using SFML.System;
using SnakeGame;
using SnakeGame.Core.Contents.MainGame.Levels;
using Snakeventures.Components;
using Snakeventures.Exceptions;
using System.Linq;

namespace Snakeventures.Actors;

internal class SnakeBody 
    : IDrawable, IUpdatable
{
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public readonly RectangleShape Shape;

    protected Vector2f DirectionFromTheBack;
    protected Vector2f DirectionInFront;

    public SnakeBody(int x, int y)
    {
        X = x;
        Y = y;

        Shape = new RectangleShape()
        {
            Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
            Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
        };
    }

    public void DrawBy(RenderTarget target)
    {
        target.Draw(Shape);
    }

    public void ChangeDirection(SnakeBody next)
    {
        var front = DirectionInFront;
        DirectionInFront = next.DirectionFromTheBack;
        DirectionFromTheBack = front;
    }

    public void Update()
    {
        X += (int)DirectionInFront.X;
        Y += (int)DirectionInFront.Y;
    }

    public void Move()
    {
        Shape.Position = new(
            Cell.CELL_SIZE * X,
            Cell.CELL_SIZE * Y);
    }
}

internal class SnakeHead : SnakeBody
{
    public SnakeHead(int x, int y) 
        : base(x, y)
    { }

    internal void TurnRight() => DirectionInFront = new(1, 0);
    internal void TurnUp() => DirectionInFront = new(0, -1);
    internal void TurnDown() => DirectionInFront = new(0, 1);
    internal void TurnLeft() => DirectionInFront = new(-1, 0);
}

internal class SnakeActor : GameActor
{
    private readonly SnakeHead _head;
    private readonly List<SnakeBody> _body;

    private readonly GameStopwatch _stopwatch;

    public override int X { get => _head.X; protected set => throw new NotSupportedException(); }
    public override int Y { get => _head.Y; protected set => throw new NotSupportedException(); }

    private readonly IGameState _gameState;

    private static readonly float MAX_ENERGY = 1f;
    private static readonly float ENERGY_CONSUMPTION = 0.005f;
    public static readonly float ENERGY_CHARGE = 0.5f;
    public float CurrentEnergy { get; private set; } = MAX_ENERGY;

    private static readonly float REGULAR_SPEED = 1f;
    private static readonly float ACCELERATION = 0.05f;
    public static readonly float MAX_SPEED = 0.25f;

    private float BaseSpeed { get; set; } = REGULAR_SPEED;
    public float CurrentSpeed { get; private set; } = REGULAR_SPEED;
    private bool Accelerate { get; set; } = false;

    public void TurnRight() => _head.TurnRight();
    public void TurnHeadUp() => _head.TurnUp();
    public void TurnHeadDown() => _head.TurnDown();
    public void TurnHeadLeft() => _head.TurnLeft();
    public void SpeedUp() => Accelerate = true;
    public void SlowDown() => Accelerate = false;

    private readonly CameraComponent _camera;
    private readonly PlayerControllerComponent _controller;
    private readonly ColliderComponent _collider;

    public SnakeActor(int x, int y, IGameState gameState)
    {
        _gameState = gameState;

        _head = new SnakeHead(x, y);

        _body = new List<SnakeBody>();
        var bodyPart = new SnakeBody(x, y + 1);
        _body.Add(bodyPart);

        _stopwatch = new GameStopwatch(BaseSpeed);

        _camera = GetCameraComponent();
        _controller = AddPlayerControllerComponent();
        _collider = AddColliderComponent();
    }

    public override void DrawBy(RenderTarget drawer)
    {
        _head.DrawBy(drawer);
        foreach (var part in _body) { part.DrawBy(drawer); }
    }

    public override void CheckCollisions()
    {
        var collider = _gameState.GetCollider() as ColliderComponent;
        if (collider is null)
            throw new UnableToLoadComponentException(nameof(ColliderComponent));

        var result = collider.CheckCollisionsFor(Id);
        if (result.IsCollision)
        {
            _gameState.GetActor(result.ColliderId);
        }
    }

    private PlayerControllerComponent AddPlayerControllerComponent()
    {
        var controller = _gameState.GetPlayerController() as PlayerControllerComponent;
        if (controller is null)
            throw new UnableToLoadComponentException(nameof(PlayerControllerComponent));

        controller.AttachTo(this);
        RegisterComponent(controller);

        return controller;
    }

    private CameraComponent GetCameraComponent()
    {
        var camera = _gameState.GetCamera() as CameraComponent;
        if (camera is null)
            throw new UnableToLoadComponentException(nameof(CameraComponent));

        camera.Follow(this);
        RegisterComponent(camera);

        return camera;
    }

    private ColliderComponent AddColliderComponent()
    {
        var collider = _gameState.GetCollider() as ColliderComponent;
        if (collider is null)
            throw new UnableToLoadComponentException(nameof(ColliderComponent));

        collider.AddDynamicCollider(GetCollisionBox());
        RegisterComponent(collider);

        return collider;
    }

    private Func<CollisionBox> GetCollisionBox()
        => () => new CollisionBox(_head.Shape.Size, _head.Shape.Position, Id);

    public override void Update() 
    {
        UpdateSpeed();
        if (!_stopwatch.Update())
        {
            UpdatePositions();
            _stopwatch.Restart();
        }
    }

    private void UpdatePositions()
    {
        for (int i = 0; i < _body.Count - 2; i++)
        {
            _body[i].ChangeDirection(_body[i + 1]);
        }
        _body[_body.Count - 1].ChangeDirection(_head);

        for (int i = _body.Count - 1; i > 0; i--)
        {
            _body[i].Update();
            _body[i].Move();
        }

        _body[0].Update();
        _body[0].Move();

        _head.Update();
    }

    private void UpdateSpeed()
    {
        if (Accelerate)
        {
            if (CurrentSpeed > MAX_SPEED)
                CurrentSpeed -= ACCELERATION;

            if (CurrentEnergy > 0)
            {
                CurrentEnergy -= ENERGY_CONSUMPTION;
                _stopwatch.ChangeTimer(CurrentSpeed);
            }
            else
            {
                SlowDown();
            }
        }
        else
        {
            if (CurrentSpeed != BaseSpeed)
            {
                CurrentSpeed = BaseSpeed;
                _stopwatch.ChangeTimer(CurrentSpeed);
            }
        }
    }

    public override void Handle(KeyboardEvent @event) => _controller.Handle(@event);
}
