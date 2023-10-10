namespace Engine.Events;

public record ChangeContentEvent
{
    public string Type { get; init; }
    public ChangeContentEvent(string type) => (Type) = type;
}
