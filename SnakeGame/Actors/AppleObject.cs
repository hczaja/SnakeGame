using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using Snakeventures.Levels;

namespace Snakeventures.Actors;

internal enum AppleType
{
    Red, Green, Yellow
}

internal class AppleObject : GameActor
{
    public override int X { get; protected set; }
    public override int Y { get; protected set; }

    private AppleType Type { get; set; }

    private readonly RectangleShape Rectangle;

    private static readonly Texture RED_APPLE_TEXTURE = new Texture("Assets/Apples/apple_red.png");
    private static readonly Texture YELLOW_APPLE_TEXTURE = new Texture("Assets/Apples/apple_yellow.png");
    private static readonly Texture GREEN_APPLE_TEXTURE = new Texture("Assets/Apples/apple_green.png");

    public AppleObject(int x, int y, AppleType type = AppleType.Red)
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
        AppleType.Red => RED_APPLE_TEXTURE,
        AppleType.Yellow => YELLOW_APPLE_TEXTURE,
        AppleType.Green => GREEN_APPLE_TEXTURE,
        _ => throw new NotImplementedException(),
    };

    public override void DrawBy(RenderTarget render)
    {
        render.Draw(Rectangle);
    }

    public override void Update()
    {

    }

    internal bool IsRed() => Type == AppleType.Red;
    internal bool IsGreen() => Type == AppleType.Green;
    internal bool IsYellow() => Type == AppleType.Yellow;

    public override void CheckCollisions()
    {
        throw new NotImplementedException();
    }

    public override void Handle(KeyboardEvent @event)
    {
        throw new NotImplementedException();
    }
}
