using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPEngine.ECS
{
    public class TransformComponent : EntityComponent
    {
        public Vector2 Position = Vector2.Zero;
        public float Scale = 1f;
        public float Rotation = 0f;
        
        public TransformComponent(Entity gameObject)
            :base(gameObject)
        {}
    }
}
