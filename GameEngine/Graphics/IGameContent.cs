using Engine.Events;
using Engine.GameObjects;

namespace Engine.Graphics;

public interface IGameContent :
    IDrawable,
    IUpdatable,
    IEventHandler<KeyboardEvent>
{
    string GetLevelId();
    object GetAdditionalData();
}
