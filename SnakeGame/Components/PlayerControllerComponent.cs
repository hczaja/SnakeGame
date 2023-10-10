using Engine.Components;
using Engine.Events;
using Engine.Settings;
using SFML.Window;
using Snakeventures.Actors;

namespace Snakeventures.Components;

internal class PlayerControllerComponent 
    : GameComponent, IEventHandler<KeyboardEvent>
{
    private SnakeActor Snake { get; set; }
    private readonly IGameSettings _settings;

    public PlayerControllerComponent(IGameSettings settings) : base() 
    { 
        _settings = settings;
    }

    internal void AttachTo(SnakeActor snakeActor)
    {
        Snake = snakeActor;
    }

    public void Handle(KeyboardEvent @event)
    {
        if (@event.Type == KeyboardEventType.Press)
        {
            switch (@event.Key)
            {
                case Keyboard.Key.Left:
                    Snake.TurnHeadLeft();
                    break;
                case Keyboard.Key.Right:
                    Snake.TurnRight();
                    break;
                case Keyboard.Key.Up:
                    Snake.TurnHeadUp();
                    break;
                case Keyboard.Key.Down:
                    Snake.TurnHeadDown();
                    break;
                case Keyboard.Key.Space:
                    Snake.SpeedUp();
                    break;
            }
        }
        else if (@event.Type == KeyboardEventType.Release)
        {
            switch (@event.Key)
            {
                case Keyboard.Key.Space:
                    Snake.SlowDown();
                    break;
            }
        }
    }
}
