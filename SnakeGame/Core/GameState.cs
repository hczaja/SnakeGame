using Engine.Actors;
using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using Engine.Settings;
using SFML.Graphics;
using SnakeGame.Core.Contents;
using SnakeGame.Core.Events;
using Snakeventures.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    //internal interface IGameState : Engine.GameState.IGameState, IEventHandler<ChangeContentEvent>
    //{
    //    IContent ActualContent { get; }
    //}

    internal class GameState : IGameState
    {
        public IGameContent ActualContent { get; private set; }

        private readonly IGameSettings _settings;
        private readonly IGameCore _game;

        public GameState(IGameCore game, IGameSettings settings)
        {
            this._game = game;
            this._settings = settings;

            this.ActualContent = new MenuContent(this);
        }

        public void DrawBy(RenderTarget render)
        {
            this.ActualContent.DrawBy(render);
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

        private CameraComponent Camera { get; set; }
        public IGameComponent GetCamera()
        {
            if (Camera is null)
                Camera = new CameraComponent(_settings);

            return Camera;
        }

        private PlayerControllerComponent PlayerController { get; set; }
        public IGameComponent GetPlayerController()
        {
            if (PlayerController is null)
                PlayerController = new PlayerControllerComponent(_settings);

            return PlayerController;
        }

        private ColliderComponent Collider { get; set; }
        public IGameComponent GetCollider()
        {
            if (Collider is null)
                Collider = new ColliderComponent();

            return Collider;
        }

        private List<GameActor> _actorsRegistry = new List<GameActor>();

        public GameActor GetActor(Guid id) => _actorsRegistry.FirstOrDefault(actor => actor.Id == id);
        public void AddActor(GameActor actor)
        {
            if (!_actorsRegistry.Any(e => e.Id == actor.Id))
                _actorsRegistry.Add(actor);
        }
        public void RemoveActor(Guid id)
        {
            var actor = _actorsRegistry.FirstOrDefault(e => e.Id == id); ;
            if (actor is not null)
                _actorsRegistry.Remove(actor);
        }
    }
}
