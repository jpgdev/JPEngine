using Microsoft.Xna.Framework;

namespace JPEngine.ECS
{
    public class TransformComponent : EntityComponent
    {
        public Vector2 Position = Vector2.Zero; //TODO: Use a Vector3 for the Z ? Helps with the Z-indexing
        public float Rotation = 0f;
        public float Scale = 1f;

        public TransformComponent(Entity gameObject)
            : base(gameObject)
        {
        }
    }
}