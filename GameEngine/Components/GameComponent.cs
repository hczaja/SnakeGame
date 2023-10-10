using Engine.GameObjects;

namespace Engine.Components;

public abstract class GameComponent : IGameComponent
{
    public Guid Id { get; }

    public GameComponent()
    {
        Id = Guid.NewGuid();
    }

    public IGameObject Deserialize(string data) => throw new NotImplementedException();
    public string Serialize() => throw new NotImplementedException();
}
