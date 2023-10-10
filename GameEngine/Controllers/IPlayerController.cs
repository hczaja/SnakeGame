using Engine.Events;

namespace Engine.Controllers;

internal interface IPlayerController
    : IEventHandler<KeyboardEvent>, IEventHandler<MouseEvent>
{ }
