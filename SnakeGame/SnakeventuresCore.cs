using Engine.Core;
using Engine.Events;
using Engine.GameState;
using Engine.Settings;
using SFML.Graphics;
using SFML.Window;
using Snakeventures.GameStates;

namespace Snakeventures;

internal class SnakeventuresCore : IGameCore
{
    private readonly IGameState _gameState;

    public EventHandler<EventArgs> Close { get; set; }

    public SnakeventuresCore(IGameSettings settings)
    {
        Close = new EventHandler<EventArgs>((_, _) => { });
        _gameState = new GameState(this, settings);
    }

    public void DrawBy(RenderTarget render) => _gameState.DrawBy(render);

    public void Update() => _gameState.Update();

    public void _window_KeyPressed(object? sender, KeyEventArgs e)
        => _gameState.Handle(new KeyboardEvent(KeyboardEventType.Press, e.Code));
    public void _window_KeyReleased(object? sender, KeyEventArgs e)
        => _gameState.Handle(new KeyboardEvent(KeyboardEventType.Release, e.Code));

    public void _window_MouseButtonPressed(object? sender, MouseButtonEventArgs e) { }

    public void _window_MouseButtonReleased(object? sender, MouseButtonEventArgs e) { }

    public void _window_MouseMoved(object? sender, EventArgs e) { }
}
