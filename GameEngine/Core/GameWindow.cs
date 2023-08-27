using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    internal class GameWindow
    {
        private readonly RenderWindow _window;
        private readonly IGame _game;

        private readonly Color _theme;

        public GameWindow(IGame game, IGameSettings settings)
        {
            this._window = new RenderWindow(
                new VideoMode(
                    settings.WindowWidth,
                    settings.WindowHeight),
                settings.GameTitle);

            _theme = settings.Theme;

            this._window.SetKeyRepeatEnabled(enable: settings.EnableKeyRepeat);

            this._game = game;

            this._window.Closed += (_, _) => Close();
            this._window.KeyPressed += _game._window_KeyPressed;
            this._window.KeyReleased += _game._window_KeyReleased;
            this._window.MouseButtonPressed += _game._window_MouseButtonPressed;
            this._window.MouseButtonReleased += _game._window_MouseButtonReleased;
            this._window.MouseMoved += _game._window_MouseMoved;

            _game.Close += (_, _) => Close();
        }

        internal void Clear() => _window.Clear(_theme);

        internal void DispatchEvents() => _window.DispatchEvents();

        internal void Display() => _window.Display();

        internal void Draw() => _game.Draw(_window);

        internal bool IsOpen() => _window.IsOpen;

        internal void Update() => _game.Update();

        public void Close() => _window.Close();
    }
}
