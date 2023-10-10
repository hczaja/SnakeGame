namespace Engine.Events;

public interface IEventHandler<T>
{
    public void Handle(T @event);
}
