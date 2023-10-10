using Engine.Core;
using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core.Events;

namespace SnakeGame.Core.Contents;

internal record LevelPointer : IDrawable
{
    public RectangleShape Shape { get; }
    public int LevelId { get; private set; } = 0;

    private readonly Vector2f[] _positions = new[]
    {
        new Vector2f(134f, 386f),
        new Vector2f(142f, 337f),
        new Vector2f(188f, 316f),
        new Vector2f(198f, 264f)
    };

    public LevelPointer()
    {
        Shape = new RectangleShape()
        {
            Position = _positions[LevelId],
            Size = new Vector2f(4f, 4f),
            FillColor = Color.Red
        };
    }

    public void MoveForward()
    {
        LevelId++;

        if (LevelId > _positions.Length - 1)
            LevelId = _positions.Length - 1;

        Shape.Position = _positions[LevelId];
    }

    public void MoveBackward()
    {
        LevelId--;

        if (LevelId < 0)
            LevelId = 0;

        Shape.Position = _positions[LevelId];
    }


    public void DrawBy(RenderTarget render)
    {
        render.Draw(Shape);
    }
}

internal class LevelsMenuContent : IGameContent
{
    private readonly IGameState _state;

    private RectangleShape _background;
    private LevelPointer Pointer { get; }

    public LevelsMenuContent(IGameState state)
    {
        _state = state;

        var settings = _state.GetSettings();
        _background = new RectangleShape()
        { 
            Position = new(0f, 0f),
            Size = new(settings.WindowWidth, settings.WindowHeight),
            Texture = new Texture("Assets/levels_background.png")
        };

        Pointer = new LevelPointer();
    }

    public void DrawBy(RenderTarget render)
    {
        render.Draw(_background);
        Pointer.DrawBy(render);
    }

    public void Handle(KeyboardEvent @event)
    {
        if (@event.Type == KeyboardEventType.Press)
        {
            if (@event.Key == Keyboard.Key.Right)
            {
                this.Pointer.MoveForward();
            }
            else if (@event.Key == Keyboard.Key.Left)
            {
                this.Pointer.MoveBackward();
            }
            else if (@event.Key == Keyboard.Key.Enter)
            {
                //_state.Handle(
                //    new ChangeContentEvent(
                //        ChangeContentEventType.Game));
            }
        }
    }

    public void Update() { }

    public string GetLevelId() => (this.Pointer.LevelId + 1).ToString();

    public object GetAdditionalData() => throw new NotImplementedException();
}
