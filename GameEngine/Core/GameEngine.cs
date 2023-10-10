using Engine.Settings;
using Engine.Time;

namespace Engine.Core;

public class GameEngine
{
    private readonly GameWindow _window;
    private readonly GameClock _clock;

    public GameEngine(IGameCore game, IGameSettings settings)
        => (_window, _clock) = (new GameWindow(game, settings), new GameClock(settings));

    public void Start()
    {
        while (_window.IsOpen())
        {
            _clock.Update();
            if (_clock.IsUpdated())
            {
                _window.DispatchEvents();

                _window.Update();

                _window.Clear();
                _window.Draw();
                _window.Display();

                _clock.Restart();
            }
        }
    }
}
