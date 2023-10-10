using Engine.Components;
using Engine.Events;
using Engine.GameObjects;
using Engine.Graphics;
using SFML.Graphics;

namespace Engine.Actors;

public abstract class GameActor
    : IGameObject, IDrawable, IUpdatable, ICollidable, IEventHandler<KeyboardEvent>
{
    public Guid Id { get; }
    public List<IGameComponent> Components { get; }

    public abstract int X { get; protected set; }
    public abstract int Y { get; protected set; }

    public GameActor()
    {
        Id = Guid.NewGuid();
        Components = Enumerable.Empty<IGameComponent>().ToList();
    }

    public abstract void DrawBy(RenderTarget drawer);
    public abstract void CheckCollisions();
    public abstract void Update();

    public abstract void Handle(KeyboardEvent @event);

    public virtual string Serialize() => string.Empty;
    public virtual IGameObject Deserialize(string data) => null;

    public void RegisterComponent(IGameComponent newComponent)
    {
        var component = Components.FirstOrDefault(c => c.Id == newComponent.Id);

        if (component is not null)
            Components.Add(newComponent);
    }

    public void UnregisterComponent(Guid id)
    {
        var component = Components.FirstOrDefault(c => c.Id == id);

        if (component is not null)
            Components.Remove(component);
    }
}
