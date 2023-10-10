using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using Snakeventures.Levels;

namespace Snakeventures.Actors;

internal class HeartObject : GameActor
{
    public override int X { get; protected set; }
    public override int Y { get; protected set; }

    public bool IsFull
    {
        get => IsFull;
        private set
        {
            IsFull = value;
            Rectangle.Texture = IsFull
                ? HEART_FULL_TEXTURE : HEART_EMPTY_TEXTURE;
        }
    }

    private readonly RectangleShape Rectangle;
    private static readonly Texture HEART_FULL_TEXTURE = new Texture("Assets/HeartFull.png");
    private static readonly Texture HEART_EMPTY_TEXTURE = new Texture("Assets/HeartEmpty.png");

    public HeartObject(GameActor parent)
    {
        X = parent.X;
        Y = parent.Y;

        Rectangle = new RectangleShape()
        {
            Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
            Position = new(X * Cell.CELL_SIZE, Y * Cell.CELL_SIZE - 12f),
            Texture = HEART_EMPTY_TEXTURE
        };
    }

    public override void DrawBy(RenderTarget drawer)
    {
        throw new NotImplementedException();
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
