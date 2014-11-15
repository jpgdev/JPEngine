using JPEngine.Entities;
using Microsoft.Xna.Framework;

namespace JPEngine.Components
{
    public class TransformComponent : BaseComponent
    {
        //public Vector2 Position = Vector2.Zero;
        public Vector3 Position = Vector3.Zero;
        public float Rotation = 0f;
        public Vector2 Scale = new Vector2(1f, 1f);

        public TransformComponent(Entity gameObject)
            : base(gameObject)
        {
        }
    }
}