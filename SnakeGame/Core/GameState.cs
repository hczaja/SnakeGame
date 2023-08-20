using Engine.Core;
using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents;
using SnakeGame.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal interface IGameState : ICoreGameState, IEventHandler<ChangeContentEvent>
    {
        IContent ActualContent { get; }
    }

    internal class GameState : IGameState
    {
        public IContent ActualContent { get; private set; }

        private readonly IGameSettings _settings;
        private readonly IGame _game;

        public GameState(IGame game, IGameSettings settings)
        {
            this._game = game;
            this._settings = settings;

            this.ActualContent = new MenuContent(this);
        }

        public void Draw(RenderTarget render)
        {
            this.ActualContent.Draw(render);
        }

        public void Handle(KeyboardEvent @event)
        {
            this.ActualContent.Handle(@event);
        }

        public void Update()
        {
            this.ActualContent.Update();
        }

        public void Handle(ChangeContentEvent @event)
        {
            switch (@event.Type) 
            {
                case ChangeContentEventType.MainMenu:
                    this.ActualContent = new MenuContent(this);
                    break;
                case ChangeContentEventType.LevelsMenu:
                    this.ActualContent = new LevelsMenuContent(this);
                    break;
                case ChangeContentEventType.Game:
                    this.ActualContent = new GameContent(this, ActualContent.GetLevelId()); 
                    break;
                case ChangeContentEventType.LevelSummary:
                    this.ActualContent = new LevelSummaryContent(this, ActualContent.GetLevelId());
                    break;
                case ChangeContentEventType.Exit:
                    this._game.Close?.Invoke(null, new EventArgs());
                    break;
            }
        }

        public IGameSettings GetSettings() => _settings;
    }
}
