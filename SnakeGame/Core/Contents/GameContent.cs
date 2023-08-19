using Engine.Core;
using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents
{
    internal class GameContent : IContent
    {
        private readonly IGameState _state;

        public GameContent(IGameState state)
        {
            _state = state;
        }

        public void Draw(RenderTarget render) { }

        public void Handle(KeyboardEvent @event) { }

        public void Update() { }
    }
}
