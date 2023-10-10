using Engine.Settings;
using SFML.Graphics;
using SFML.Window;

namespace Engine.Core;

internal class GameWindow
{
    private readonly RenderWindow _window;
    private readonly IGameCore _game;

    private readonly Color _theme;

    public GameWindow(IGameCore game, IGameSettings settings)
    {
        _window = new RenderWindow(
            new VideoMode(
                settings.WindowWidth,
                settings.WindowHeight),
            settings.GameTitle);

        _theme = settings.Theme;

        _window.SetKeyRepeatEnabled(enable: settings.EnableKeyRepeat);
        _window.SetMouseCursorVisible(visible: settings.MouseCursorVisible);

        _game = game;

        _window.Closed += (_, _) => Close();
        _window.KeyPressed += _game._window_KeyPressed;
        _window.KeyReleased += _game._window_KeyReleased;
        _window.MouseButtonPressed += _game._window_MouseButtonPressed;
        _window.MouseButtonReleased += _game._window_MouseButtonReleased;
        _window.MouseMoved += _game._window_MouseMoved;

        _game.Close += (_, _) => Close();
    }

    internal void Clear() => _window.Clear(_theme);

    internal void DispatchEvents() => _window.DispatchEvents();

    internal void Display() => _window.Display();

    internal void Draw() => _game.DrawBy(_window);

    internal bool IsOpen() => _window.IsOpen;

    internal void Update() => _game.Update();

    public void Close() => _window.Close();
}
