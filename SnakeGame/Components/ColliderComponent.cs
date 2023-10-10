using Engine.Collisions;
using Engine.Components;

namespace Snakeventures.Components;

internal class ColliderComponent : GameComponent
{
    public ColliderComponent() : base()
    {
        DynamicColliders = new List<Func<CollisionBox>>();
        StaticColliders = new List<CollisionBox>();
    }

    private List<Func<CollisionBox>> DynamicColliders { get; }
    public void AddDynamicCollider(Func<CollisionBox> collision) 
        => DynamicColliders.Add(collision);

    private List<CollisionBox> StaticColliders { get; }
    public void AddStaticCollider(CollisionBox collision) 
        => StaticColliders.Add(collision);

    public (bool IsCollision, Guid ColliderId) CheckCollisionsFor(Guid id)
    {
        var getColliderData = DynamicColliders.FirstOrDefault(c => c().Id == id);
        if (getColliderData != null)
        {
            var collider = getColliderData();

            foreach (var other in StaticColliders.Concat(DynamicColliders.Select(c => c()).Except(new[] { collider })))
            {
                if (collider.IsInCollisionWith(other))
                    return (true, other.Id);
            }
        }

        return (false, id);
    }

}
