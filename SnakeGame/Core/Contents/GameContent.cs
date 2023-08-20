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

        private readonly string _levelId;

        public GameContent(IGameState state, string levelId)
        {
            _state = state;
            _levelId = levelId;

            var loader = new LevelLoader();
            _level = loader.Load($"Assets/Levels/Level{levelId}.txt", _state);

            _score = new Score(
                _level.GetMaxApples(),
                _level.Name,
                _state.GetSettings());
        }

        public void Draw(RenderTarget render) 
        {
            _level.Draw(render);
            _score.Draw(render);
        }

        public string GetLevelId() => _levelId;

        public object GetAdditionalData()
        {
            _score.Update(_level);
            return _score.GetFinalScore();
        }

        public void Handle(KeyboardEvent @event) 
        {
            _level.Handle(@event);
        }

        public void Update() 
        {
            _level.Update();
            _score.Update(_level);
        }
    }
}
