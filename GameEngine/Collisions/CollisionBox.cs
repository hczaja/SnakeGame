using SFML.Graphics;
using SFML.System;

namespace Engine.Collisions;

public class CollisionBox : RectangleShape
{
    public Guid Id { get; }
    public CollisionBox(Vector2f size, Vector2f position, Guid id)
    {
        Id = id;
        Size = size;
        Position = position;
    }

    public bool IsInCollisionWith(CollisionBox other)
    {
        if (other is null || Id == other.Id) return false;

        var topLeft = new Vector2f(Position.X, Position.Y);
        var bottomRight = new Vector2f(Position.X + Size.X, Position.Y + Size.Y);

        var topLeftOther = new Vector2f(other.Position.X, other.Position.Y);
        var bottomRightOther = new Vector2f(other.Position.X + other.Size.X, other.Position.Y + other.Size.Y);

        if (topLeft.X == bottomRight.X 
            || topLeft.Y == bottomRight.Y 
            || bottomRightOther.X == topLeftOther.X 
            || topLeftOther.Y == bottomRightOther.Y)
                return false;

        if (topLeft.X > bottomRightOther.X 
            || topLeftOther.X > bottomRight.X)
                return false;

        if (bottomRight.Y > topLeftOther.Y 
            || bottomRightOther.Y > topLeft.Y)
                return false;

        return true;

    }
}
