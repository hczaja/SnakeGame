using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core;
using Engine.Events;
using SFML.Graphics;
using SFML.Window;

namespace SnakeGame
{
    internal class Snake : IGame
    {
        private readonly ICoreGameState _gameState;

        public EventHandler<EventArgs> Close { get; set; }

        public Snake(IGameSettings settings)
        {
            Close = new EventHandler<EventArgs>((_, _) => { });
            _gameState = new GameState(this, settings);
        }

        public void Draw(RenderTarget render) => _gameState.Draw(render);

        public void Update() => _gameState.Update();
            
        public void _window_KeyPressed(object? sender, KeyEventArgs e)
            => _gameState.Handle(new KeyboardEvent(KeyboardEventType.Press, e.Code));
        public void _window_KeyReleased(object? sender, KeyEventArgs e)
            => _gameState.Handle(new KeyboardEvent(KeyboardEventType.Release, e.Code));

        public void _window_MouseButtonPressed(object? sender, MouseButtonEventArgs e) { }

        public void _window_MouseButtonReleased(object? sender, MouseButtonEventArgs e) { }

        public void _window_MouseMoved(object? sender, EventArgs e) { }
    }
}
