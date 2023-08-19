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

        public GameWindow(IGame game, IGameSettings settings)
        {
            this._window = new RenderWindow(
                new VideoMode(
                    settings.WindowWidth,
                    settings.WindowHeight),
                settings.GameTitle);

            this._window.SetKeyRepeatEnabled(enable: false);

            this._window.Closed += _game._window_MouseMoved;
            this._window.KeyPressed += _game._window_KeyPressed;
            this._window.KeyReleased += _game._window_KeyReleased;
            this._window.MouseButtonPressed += _game._window_MouseButtonPressed;
            this._window.MouseButtonReleased += _game._window_MouseButtonReleased;
            this._window.MouseMoved += _game._window_MouseMoved;

            this._game = game;
        }

        internal void Clear() => _window.Clear(Color.Black);

        internal void DispatchEvents() => _window.DispatchEvents();

        internal void Display() => _window.Display();

        internal void Draw() => _game.Draw(_window);

        internal bool IsOpen() => _window.IsOpen;

        internal void Update() => _game.Update();
    }
}
