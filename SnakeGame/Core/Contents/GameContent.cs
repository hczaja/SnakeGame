using Engine.Events;
using Engine.Graphics;
using Engine.Time;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame;
using SnakeGame.Core.Contents.MainGame.GameObjects;
using SnakeGame.Core.Contents.MainGame.Levels;
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

        private readonly Level _level;
        private readonly Score _score;

        public GameContent(IGameState state, string levelId)
        {
            _state = state;

            var loader = new LevelLoader();
            _level = loader.Load($"Assets/Levels/Level{levelId}.txt", _state);
        }

        public void Draw(RenderTarget render) 
        {
            _level.Draw(render);
        }

        public void Handle(KeyboardEvent @event) 
        {
            _level.Handle(@event);
        }

        public void Update() 
        {
            _level.Update();
        }
    }
}
