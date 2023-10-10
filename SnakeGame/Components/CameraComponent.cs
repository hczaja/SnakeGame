using Engine.Components;
using Engine.GameObjects;
using Engine.Settings;
using Snakeventures.Actors;

namespace Snakeventures.Components;

internal class CameraComponent : GameComponent
{
    private readonly IGameSettings _settings;

    public CameraComponent(IGameSettings settings) : base()
    {
        _settings = settings;
    }

    internal void Follow(SnakeActor snakeActor)
    {
        throw new NotImplementedException();
    }
}
