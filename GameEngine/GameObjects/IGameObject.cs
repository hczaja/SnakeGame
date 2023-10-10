namespace Engine.GameObjects;

public interface IGameObject 
{
    Guid Id { get; }

    string Serialize();
    IGameObject Deserialize(string data);
}
