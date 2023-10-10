using Engine.Actors;
using Engine.Components;
using Engine.Events;
using Engine.GameObjects;
using Engine.Graphics;
using Engine.Settings;

namespace Engine.GameState;

public interface IGameState :
    IDrawable,
    IUpdatable,
    IEventHandler<KeyboardEvent>,
    IEventHandler<ChangeContentEvent>
{
    IGameComponent GetCamera();
    IGameComponent GetCollider();
    IGameComponent GetPlayerController();
    IGameSettings GetSettings();

    GameActor GetActor(Guid id);
    void AddActor(GameActor id);
    void RemoveActor(Guid id);
}
