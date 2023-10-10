using Engine.Actors;
using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using Engine.Settings;
using SFML.Graphics;
using Snakeventures.Components;
using Snakeventures.Contents;

namespace Snakeventures.GameStates
{
    internal class GameState : IGameState
    {
        public IGameContent ActualContent { get; private set; }

        private readonly IGameSettings _settings;
        private readonly IGameCore _game;

        public GameState(IGameCore game, IGameSettings settings)
        {
            _game = game;
            _settings = settings;

            ActualContent = new MenuContent(this);
        }

        public void DrawBy(RenderTarget render)
        {
            ActualContent.DrawBy(render);
        }

        public void Handle(KeyboardEvent @event)
        {
            ActualContent.Handle(@event);
        }

        public void Update()
        {
            ActualContent.Update();
        }

        public void Handle(ChangeContentEvent @event)
        {
            switch (@event.Type)
            {
                case nameof(MenuContent):
                    ActualContent = new MenuContent(this);
                    break;
                case nameof(LevelsMenuContent):
                    ActualContent = new LevelsMenuContent(this);
                    break;
                case nameof(GameContent):
                    ActualContent = new GameContent(this, ActualContent.GetLevelId());
                    break;
                case nameof(LevelSummaryContent):
                    ActualContent = new LevelSummaryContent(this, ActualContent.GetLevelId());
                    break;
                default:
                    _game.Close?.Invoke(null, new EventArgs());
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
